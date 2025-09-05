# Azure Account Reference - BrightLife IMS
**Generated on**: September 5, 2025  
**Account**: Azure for Students subscription  
**Project**: BrightLife Inventory Management System

---

## 📋 Account Overview

### Basic Information
- **Account Type**: Azure for Students
- **Status**: Active & Enabled
- **Primary Region**: Southeast Asia
- **Project URL**: https://app-bl-pstn3hojje7gu.azurewebsites.net

### Credit Information
- **Initial Credit**: $100 USD (Azure for Students)
- **Estimated Monthly Cost**: $16-24 USD (after optimizations)
- **Expected Duration**: 4-6 months
- **Optimization Savings**: $17-19 USD/month (40-45% reduction)

---

## 🏗️ Infrastructure Overview

### Resource Groups (Primary: rg-imsapp)
- **rg-imsapp** - BrightLife IMS (Primary application)
- **ItransitionGroup** - Course projects
- **appsvc_windows_southeastasia** - Legacy applications
- **MyLandingPage-RG** - Static sites
- Additional system resource groups

---

## 💻 Active Services & Costs

### 🆓 FREE Tier Services (No Charges)

#### Web Applications
| Service Name | Resource Group | Plan | Status |
|-------------|----------------|------|--------|
| `app-bl-pstn3hojje7gu` | rg-imsapp | Free F1 | ✅ Production |
| `FakeBookGeneratorLinux` | appsvc_windows_southeastasia | Free F1 | ✅ Active |
| `USERMANAGEMENTAPPbyYEASINARAFAT` | ItransitionGroup | Free F1 | ✅ Optimized |
| `PresentationManagement-frontend` | ItransitionGroup | Free F1 | ✅ Optimized |

#### Storage & Monitoring
- **Storage Accounts**: Standard_LRS (minimal usage)
- **Application Insights**: Pay-as-you-go (minimal)
- **Log Analytics**: Pay-as-you-go (minimal)

### 💳 PAID Services (Credit Consuming)

#### Databases
| Service | Type | Tier | Monthly Cost | Status |
|---------|------|------|-------------|--------|
| `psql-bl-pstn3hojje7gu` | PostgreSQL | Standard_B1ms | $12-15 USD | 🔴 Main Cost |
| `CollaborativePresentationDB-Prod` | SQL Database | Standard S0 | $3-5 USD | 🟡 Optimized |

#### Security
- **Key Vault**: Standard tier ($0-1 USD/month)

---

## 📊 Cost Optimization History

### Optimizations Performed (September 5, 2025)

#### ✅ Completed Actions
1. **App Service Plan Downgrade**
   - From: Basic B1 → To: Free F1
   - Monthly Savings: ~$13 USD

2. **SQL Database Downgrade**  
   - From: GeneralPurpose GP_S_Gen5_1 → To: Standard S0
   - Size: Reduced from 32GB to 10GB
   - Monthly Savings: ~$5-8 USD

#### 💰 Cost Impact
- **Before Optimization**: $33-43 USD/month
- **After Optimization**: $16-24 USD/month
- **Total Monthly Savings**: $17-19 USD (40-45% reduction)

---

## 🔧 Service Details

### BrightLife IMS (Primary Application)
**Resource Group**: rg-imsapp  
**URL**: https://app-bl-pstn3hojje7gu.azurewebsites.net

#### Components
- **Web App**: Free F1 tier
- **Database**: PostgreSQL Flexible Server (B1ms)
- **Security**: Azure Key Vault (Standard)
- **Monitoring**: Application Insights + Log Analytics

#### Authentication
- **Provider**: Google OAuth 2.0
- **Configuration**: Stored securely in Azure Key Vault
- **Redirect URIs**: Configured for both local development and production

---

## 🚀 Deployment Information

### Azure Developer CLI (azd)
- **Environment**: Production
- **Template**: ASP.NET Core with PostgreSQL
- **Infrastructure**: Bicep templates
- **Location**: Southeast Asia

### GitHub Integration
- **Repository**: ya-shuvo30/InventoryManagement_App
- **Branch**: main
- **CI/CD**: GitHub Actions (Build & Test)
- **Deployment**: Manual via Azure Developer CLI

---

## 💡 Cost Management Recommendations

### Immediate Actions Available
1. **PostgreSQL Management**: Stop during non-testing periods
2. **Resource Cleanup**: Remove unused legacy resources
3. **Monitoring**: Set up budget alerts at $20, $30, $40

### Regular Maintenance
- **Monthly Review**: Check consumption reports
- **Quarterly Optimization**: Review service tiers
- **Annual Assessment**: Evaluate overall architecture

---

## 📝 Change Log

### September 5, 2025
- ✅ Optimized App Service Plans to Free F1
- ✅ Downgraded SQL Database to Standard S0  
- ✅ Achieved 40-45% cost reduction
- ✅ Extended credit duration to 4-6 months
- ✅ Fixed GitHub Actions workflow issues

---

**Last Updated**: September 5, 2025  
**Next Review**: October 5, 2025  
**Status**: ✅ Optimized and Running Efficiently
