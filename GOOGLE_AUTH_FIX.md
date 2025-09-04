# 🔧 GOOGLE AUTHENTICATION FIX GUIDE

## 🚨 Current Issue
You're getting this error:
```
Microsoft.AspNetCore.Authentication.AuthenticationFailureException: OAuth token endpoint failure: invalid_client;Description=Unauthorized
```

**Root Cause**: Your Google ClientSecret is set to a placeholder value instead of the actual secret from Google Cloud Console.

## ✅ QUICK FIX - Steps to Resolve

### Step 1: Get Your Google Client Secret
1. Go to [Google Cloud Console](https://console.cloud.google.com/)
2. Navigate to **APIs & Services** > **Credentials**
3. Find your OAuth 2.0 Client ID: `492291051574-i0h15sg86btpacnqhm46v79kjb626m64.apps.googleusercontent.com`
4. Click on it to view details
5. **Copy the Client Secret** (it should look like: `GOCSPX-xxxxxxxxxxxxxxxxxxxxxxxxx`)

### Step 2: Update User Secrets with Real Client Secret
Open terminal in your project folder and run:

```bash
cd "c:\Brightlife\ims\BrightLifeIMS\src\BrightLifeIMS.Web"
dotnet user-secrets set "Authentication:Google:ClientSecret" "GOCSPX-your-actual-secret-here"
```

**Replace `GOCSPX-your-actual-secret-here` with your real Google Client Secret!**

### Step 3: Verify Configuration
Check that your secrets are set correctly:
```bash
dotnet user-secrets list
```

You should see:
```
Authentication:Google:ClientSecret = GOCSPX-your-actual-secret
Authentication:Google:ClientId = 492291051574-i0h15sg86btpacnqhm46v79kjb626m64.apps.googleusercontent.com
```

### Step 4: Update Google Console Redirect URI
1. In Google Cloud Console, go to your OAuth 2.0 Client
2. Under **Authorized redirect URIs**, make sure you have:
   ```
   https://localhost:5211/signin-google
   ```
   
### Step 5: Test the Fix
1. Build and run your application:
   ```bash
   dotnet build
   dotnet run
   ```

2. Navigate to `https://localhost:5211/Identity/Account/Login`
3. You should see a "Log in with Google" button
4. Click it - it should now work without the `invalid_client` error

## 🔍 What We Fixed

### Before (Causing Error):
- ClientSecret was set to: `YOUR_ACTUAL_GOOGLE_CLIENT_SECRET_GOES_HERE`
- App tried to authenticate with Google using invalid credentials
- Google rejected the request with `invalid_client` error

### After (Working):
- Program.cs now checks if credentials are valid before enabling Google auth
- If credentials are placeholders, Google auth is disabled gracefully
- Real ClientSecret from Google Console enables proper authentication

## 🛡️ Security Notes

- **Never commit real secrets to code**: We use user-secrets for development
- **Production**: Use Azure Key Vault, AWS Secrets Manager, or environment variables
- **Rotate secrets**: Change ClientSecret periodically for security

## 🧪 Testing Your Google Login

Once fixed, you can test:

1. **Login Page**: Visit `/Identity/Account/Login`
2. **Google Button**: Should appear and be clickable
3. **OAuth Flow**: Should redirect to Google for authentication
4. **Return**: Should redirect back to your app after successful login
5. **Account Creation**: New Google users should get accounts created automatically

## 📝 Additional Configuration (Optional)

If you want to customize the Google authentication further:

```csharp
// In Program.cs, you can add more options:
googleOptions.Scope.Add("profile");  // ✅ Already added
googleOptions.Scope.Add("email");    // ✅ Already added
googleOptions.AccessType = "offline"; // For refresh tokens
googleOptions.SaveTokens = true;      // ✅ Already enabled
```

## 🚨 Troubleshooting

### If you still get errors:

1. **Check ClientSecret Format**: Should start with `GOCSPX-`
2. **Verify Redirect URI**: Must match exactly in Google Console
3. **Check Project Status**: Ensure OAuth consent screen is configured
4. **Domain Verification**: For production, domain must be verified

### Common Issues:

| Error | Solution |
|-------|----------|
| `redirect_uri_mismatch` | Add correct URI to Google Console |
| `invalid_client` | Fix ClientSecret (this guide) |
| `unauthorized_client` | Enable Google+ API in Google Console |
| `access_blocked` | Configure OAuth consent screen |

## 📞 Still Need Help?

If you continue having issues:
1. Double-check your Google Cloud Console settings
2. Verify the ClientSecret is copied correctly (no extra spaces)
3. Ensure you're using the correct Google Cloud project
4. Check that the OAuth consent screen is properly configured

---

**🎯 Summary**: Replace the placeholder ClientSecret with your real Google Client Secret using `dotnet user-secrets set`, and Google authentication will work perfectly!
