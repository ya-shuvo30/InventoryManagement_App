// # Models/Enums/UserRole.cs
namespace BrightLifeIMS.Web.Models.Enums
{
    public enum UserRole
    {
        Guest = 0,      // Read-only access to public inventories
        User = 1,       // Can create inventories and add items where permitted
        Creator = 2,    // Full control over owned inventories
        Admin = 3       // System-wide administrative access
    }
}