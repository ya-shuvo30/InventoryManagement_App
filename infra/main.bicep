targetScope = 'subscription'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention')
param environmentName string

@minLength(1)
@description('Primary location for all resources')
param location string

@description('Name of the resource group')
param resourceGroupName string

@description('Environment for the application')
param aspnetcoreEnvironment string = 'Production'

@secure()
@description('Administrator password for PostgreSQL server')
param postgresAdminPassword string

// Generate a unique token for resource naming
var resourceToken = uniqueString(subscription().id, location, environmentName)
var resourcePrefix = 'bl' // brightlife prefix (3 chars max)

// Create the resource group
resource rg 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: resourceGroupName
  location: location
  tags: {
    'azd-env-name': environmentName
  }
}

// Deploy the main resources to the resource group
module resources 'resources.bicep' = {
  name: 'resources-${resourceToken}'
  scope: rg
  params: {
    location: location
    environmentName: environmentName
    resourceToken: resourceToken
    resourcePrefix: resourcePrefix
    aspnetcoreEnvironment: aspnetcoreEnvironment
    postgresAdminPassword: postgresAdminPassword
  }
}

// Required outputs
output RESOURCE_GROUP_ID string = rg.id
output AZURE_LOCATION string = location
output AZURE_SUBSCRIPTION_ID string = subscription().subscriptionId
output AZURE_RESOURCE_GROUP string = rg.name

// Service outputs
output BRIGHTLIFE_WEB_URI string = resources.outputs.BRIGHTLIFE_WEB_URI
output BRIGHTLIFE_WEB_NAME string = resources.outputs.BRIGHTLIFE_WEB_NAME
output DATABASE_CONNECTION_STRING string = resources.outputs.DATABASE_CONNECTION_STRING
output KEY_VAULT_NAME string = resources.outputs.KEY_VAULT_NAME
output APPLICATION_INSIGHTS_CONNECTION_STRING string = resources.outputs.APPLICATION_INSIGHTS_CONNECTION_STRING
