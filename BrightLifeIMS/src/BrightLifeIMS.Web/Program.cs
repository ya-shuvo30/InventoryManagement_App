using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BrightLifeIMS.Web.Data;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;
using BrightLifeIMS.Web.Services.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure database context - SQLite for development, PostgreSQL for production
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity with external authentication support
builder.Services.AddDefaultIdentity<User>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<AppDbContext>();

// Configure external authentication providers
var authBuilder = builder.Services.AddAuthentication();

// Only add Google authentication if valid credentials are provided
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

if (!string.IsNullOrEmpty(googleClientId) && 
    !string.IsNullOrEmpty(googleClientSecret) &&
    googleClientId != "PLACEHOLDER_GOOGLE_CLIENT_ID" &&
    googleClientSecret != "PLACEHOLDER_GOOGLE_CLIENT_SECRET" &&
    googleClientSecret != "YOUR_ACTUAL_GOOGLE_CLIENT_SECRET_GOES_HERE")
{
    authBuilder.AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = googleClientId;
        googleOptions.ClientSecret = googleClientSecret;
        
        // Configure scopes for user information
        googleOptions.Scope.Add("profile");
        googleOptions.Scope.Add("email");
        
        // Save tokens for later use
        googleOptions.SaveTokens = true;
        
        // Set the callback path
        googleOptions.CallbackPath = "/signin-google";
    });
    
    // Log that Google authentication is enabled
    Console.WriteLine("✅ Google authentication configured successfully");
}
else
{
    // Log that Google authentication is not configured
    Console.WriteLine("⚠️  Google authentication not configured - missing or placeholder credentials");
    Console.WriteLine("   To enable Google login:");
    Console.WriteLine("   1. Go to Google Cloud Console");
    Console.WriteLine("   2. Get your Client Secret");
    Console.WriteLine("   3. Run: dotnet user-secrets set \"Authentication:Google:ClientSecret\" \"YOUR_ACTUAL_SECRET\"");
}

// Register repositories and services
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

// Register core services
builder.Services.AddScoped<ITagService, TagService>();

// Configure Controllers and Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.Migrate();
        Console.WriteLine("✅ Database migrations applied successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error applying migrations: {ex.Message}");
        // Don't fail the application startup, just log the error
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();