using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public enum UserRole
    {
        User = 0,
        Creator = 1,
        Admin = 2
    }

    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }
        
        [MaxLength(100)]
        public string? LastName { get; set; }
        
        [MaxLength(100)]
        public string? FullName { get; set; }
        
        public UserRole Role { get; set; } = UserRole.User;
        
        public string? ProfileImageUrl { get; set; }
        
        public string PreferredLanguage { get; set; } = "en-US";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? LastLoginAt { get; set; }
        
        // Navigation properties - only include those with DbSets in context
        public ICollection<Inventory> CreatedInventories { get; set; } = new List<Inventory>();
        public ICollection<Item> CreatedItems { get; set; } = new List<Item>();
        
        // These will be added later when we enable additional entities
        // public ICollection<ItemLike> ItemLikes { get; set; } = new List<ItemLike>();
        // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}