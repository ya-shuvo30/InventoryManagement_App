@description('Primary location for all resources')
param location string

@description('Name of the environment')
param environmentName string

@description('Unique token for resource naming')
param resourceToken string

@description('Resource prefix for naming')
param resourcePrefix string

@description('Environment for the application')
param aspnetcoreEnvironment string

@secure()
@description('Administrator password for PostgreSQL server')
param postgresAdminPassword string

// Log Analytics Workspace (Student Pack Optimized)
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: 'log-${resourcePrefix}-${resourceToken}'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018' // Pay-as-you-go (cost-effective for students)
    }
    retentionInDays: 30 // Minimal retention for cost savings
    features: {
      enableLogAccessUsingOnlyResourcePermissions: true
    }
  }
  tags: {
    'azd-env-name': environmentName
  }
}

// Application Insights (Free tier included)
resource applicationInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: 'ai-${resourcePrefix}-${resourceToken}'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logAnalytics.id
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
  }
  tags: {
    'azd-env-name': environmentName
  }
}

// User-assigned managed identity
resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' = {
  name: 'id-${resourcePrefix}-${resourceToken}'
  location: location
  tags: {
    'azd-env-name': environmentName
  }
}

// Key Vault for storing secrets
resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: 'kv-${resourcePrefix}-${resourceToken}'
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard' // Standard tier for student projects
    }
    tenantId: subscription().tenantId
    enableRbacAuthorization: true
    enabledForDeployment: true
    enabledForTemplateDeployment: true
    enabledForDiskEncryption: true
    publicNetworkAccess: 'Enabled'
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
  }
  tags: {
    'azd-env-name': environmentName
  }
}

// Key Vault role assignment for managed identity
resource keyVaultSecretsOfficerRole 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(keyVault.id, managedIdentity.id, 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7')
  scope: keyVault
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'b86a8fe4-44ce-4948-aee5-eccb2c155cd7') // Key Vault Secrets Officer
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal'
  }
}

// PostgreSQL Flexible Server (Student Pack Optimized)
resource postgresServer 'Microsoft.DBforPostgreSQL/flexibleServers@2023-06-01-preview' = {
  name: 'psql-${resourcePrefix}-${resourceToken}'
  location: location
  sku: {
    name: 'Standard_B1ms' // Burstable tier - most cost-effective for students
    tier: 'Burstable'
  }
  properties: {
    administratorLogin: 'brightlifeadmin'
    administratorLoginPassword: postgresAdminPassword
    version: '14'
    storage: {
      storageSizeGB: 32 // Minimum size for cost optimization
      autoGrow: 'Enabled'
    }
    backup: {
      backupRetentionDays: 7 // Minimum retention for cost savings
      geoRedundantBackup: 'Disabled' // Disabled for cost optimization
    }
    highAvailability: {
      mode: 'Disabled' // Disabled for cost optimization
    }
    network: {
      publicNetworkAccess: 'Enabled'
    }
    authConfig: {
      activeDirectoryAuth: 'Disabled'
      passwordAuth: 'Enabled'
    }
  }
  tags: {
    'azd-env-name': environmentName
  }
}

// PostgreSQL Database
resource postgresDatabase 'Microsoft.DBforPostgreSQL/flexibleServers/databases@2023-06-01-preview' = {
  parent: postgresServer
  name: 'brightlife_ims'
  properties: {
    charset: 'UTF8'
    collation: 'en_US.utf8'
  }
}

// PostgreSQL Firewall Rules
resource postgresFirewallAzure 'Microsoft.DBforPostgreSQL/flexibleServers/firewallRules@2023-06-01-preview' = {
  parent: postgresServer
  name: 'AllowAzureServices'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

// App Service Plan (Free F1 tier for students)
resource appServicePlan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: 'plan-${resourcePrefix}-${resourceToken}'
  location: location
  sku: {
    name: 'F1' // Free tier - perfect for student projects
    tier: 'Free'
    size: 'F1'
    family: 'F'
    capacity: 1
  }
  properties: {
    reserved: false // Windows
  }
  tags: {
    'azd-env-name': environmentName
  }
}

// App Service (Web App)
resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: 'app-${resourcePrefix}-${resourceToken}'
  location: location
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}': {}
    }
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v9.0'
      metadata: [
        {
          name: 'CURRENT_STACK'
          value: 'dotnet'
        }
      ]
      cors: {
        allowedOrigins: ['*']
        supportCredentials: false
      }
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: aspnetcoreEnvironment
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: applicationInsights.properties.ConnectionString
        }
        {
          name: 'Authentication__Google__ClientId'
          value: '@Microsoft.KeyVault(VaultName=${keyVault.name};SecretName=google-client-id)'
        }
        {
          name: 'Authentication__Google__ClientSecret'
          value: '@Microsoft.KeyVault(VaultName=${keyVault.name};SecretName=google-client-secret)'
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: 'Host=${postgresServer.properties.fullyQualifiedDomainName};Database=${postgresDatabase.name};Username=${postgresServer.properties.administratorLogin};Password=${postgresAdminPassword};SslMode=Require'
          type: 'PostgreSQL'
        }
      ]
    }
  }
  tags: {
    'azd-env-name': environmentName
    'azd-service-name': 'brightlife-web'
  }
}

// Web App Diagnostic Settings
resource webAppDiagnostics 'Microsoft.Insights/diagnosticSettings@2021-05-01-preview' = {
  name: 'webAppDiagnostics'
  scope: webApp
  properties: {
    workspaceId: logAnalytics.id
    logs: [
      {
        category: 'AppServiceHTTPLogs'
        enabled: true
      }
      {
        category: 'AppServiceConsoleLogs'
        enabled: true
      }
      {
        category: 'AppServiceAppLogs'
        enabled: true
      }
    ]
    metrics: [
      {
        category: 'AllMetrics'
        enabled: true
      }
    ]
  }
}

// Outputs
output BRIGHTLIFE_WEB_URI string = 'https://${webApp.properties.defaultHostName}'
output BRIGHTLIFE_WEB_NAME string = webApp.name
output DATABASE_CONNECTION_STRING string = 'Host=${postgresServer.properties.fullyQualifiedDomainName};Database=${postgresDatabase.name};Username=${postgresServer.properties.administratorLogin};Password=***;SslMode=Require'
output KEY_VAULT_NAME string = keyVault.name
output APPLICATION_INSIGHTS_CONNECTION_STRING string = applicationInsights.properties.ConnectionString
output POSTGRES_SERVER_NAME string = postgresServer.name
output MANAGED_IDENTITY_CLIENT_ID string = managedIdentity.properties.clientId
