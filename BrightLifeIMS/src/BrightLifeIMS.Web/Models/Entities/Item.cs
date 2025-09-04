// src/BrightLifeIMS.Web/Models/Entities/Item.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        
        public int InventoryId { get; set; }
        
        [MaxLength(255)]
        public string? CustomId { get; set; }
        
        public string CreatedById { get; set; } = string.Empty;
        
        // Optimistic locking
        public int Version { get; set; } = 1;
        
        public int LikesCount { get; set; } = 0;
        
        // Custom field values
        [MaxLength(1000)]
        public string? CustomString1 { get; set; }
        
        [MaxLength(1000)]
        public string? CustomString2 { get; set; }
        
        [MaxLength(1000)]
        public string? CustomString3 { get; set; }
        
        public int? CustomInt1 { get; set; }
        public int? CustomInt2 { get; set; }
        public int? CustomInt3 { get; set; }
        
        public bool? CustomBool1 { get; set; }
        public bool? CustomBool2 { get; set; }
        public bool? CustomBool3 { get; set; }
        
        public string? CustomText1 { get; set; }
        public string? CustomText2 { get; set; }
        public string? CustomText3 { get; set; }
        
        [MaxLength(500)]
        public string? CustomUrl1 { get; set; }
        
        [MaxLength(500)]
        public string? CustomUrl2 { get; set; }
        
        [MaxLength(500)]
        public string? CustomUrl3 { get; set; }
        
        // Cloud storage references (stored as JSON array of Cloudinary URLs)
        public string? CloudImages { get; set; }
        
        // Additional flexible custom fields (stored as JSON TEXT in SQLite)
        public string? CustomFieldsJson { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties - only include those with DbSets in context
        public Inventory Inventory { get; set; } = null!;
        
        // These will be added later when we enable additional entities
        // public User CreatedBy { get; set; } = null!;
        // public ICollection<ItemLike> Likes { get; set; } = new List<ItemLike>();
        // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}