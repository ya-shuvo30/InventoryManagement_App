// src/BrightLifeIMS.Web/Models/Entities/Tag.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? NameBn { get; set; }
        
        public int UsageCount { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public ICollection<InventoryTag> InventoryTags { get; set; } = new List<InventoryTag>();
    }
}