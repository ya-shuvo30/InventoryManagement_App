# 🔑 GOOGLE AUTHENTICATION SETUP COMMANDS

## Current Status
Your Google ClientId is correctly configured:
```
492291051574-i0h15sg86btpacnqhm46v79kjb626m64.apps.googleusercontent.com
```

But your ClientSecret is still the placeholder:
```
YOUR_ACTUAL_GOOGLE_CLIENT_SECRET_GOES_HERE
```

## Fix Commands

### 1. Set Your Real Google Client Secret
Replace `GOCSPX-your-actual-secret-here` with your real Google Client Secret:

```bash
cd "c:\Brightlife\ims\BrightLifeIMS\src\BrightLifeIMS.Web"
dotnet user-secrets set "Authentication:Google:ClientSecret" "GOCSPX-your-actual-secret-here"
```

### 2. Verify the Configuration
```bash
dotnet user-secrets list
```

You should see:
```
Authentication:Google:ClientSecret = GOCSPX-your-real-secret
Authentication:Google:ClientId = 492291051574-i0h15sg86btpacnqhm46v79kjb626m64.apps.googleusercontent.com
```

### 3. Test the Application
```bash
dotnet run
```

You should see:
```
✅ Google authentication configured successfully
```

Instead of:
```
⚠️ Google authentication not configured - missing or placeholder credentials
```

## Google Cloud Console Setup

### Make sure you have these settings in Google Cloud Console:

1. **Authorized JavaScript origins:**
   ```
   http://localhost:5211
   https://localhost:5211
   ```

2. **Authorized redirect URIs:**
   ```
   http://localhost:5211/signin-google
   https://localhost:5211/signin-google
   ```

### Where to find your Client Secret:
1. Go to https://console.cloud.google.com/
2. Navigate to "APIs & Services" > "Credentials"
3. Click on your OAuth 2.0 Client ID
4. Copy the "Client secret" (starts with GOCSPX-)

## Testing Steps

1. **Start the application** - should show "✅ Google authentication configured successfully"
2. **Navigate to** http://localhost:5211/Identity/Account/Login
3. **Look for** "Log in with Google" button
4. **Click the button** - should redirect to Google
5. **Complete Google login** - should redirect back to your app

## Troubleshooting

If you still have issues after setting the real ClientSecret:

### Error: "redirect_uri_mismatch"
- Add the exact redirect URI to Google Console: `http://localhost:5211/signin-google`

### Error: "invalid_client"
- Double-check the ClientSecret is correct (no extra spaces)
- Ensure you're using the right Google Cloud project

### Error: "unauthorized_client"
- Enable the Google+ API in Google Cloud Console
- Configure the OAuth consent screen

### No Google button appears
- Check the console log when starting the app
- Should show "✅ Google authentication configured successfully"
- If not, the ClientSecret is still incorrect
