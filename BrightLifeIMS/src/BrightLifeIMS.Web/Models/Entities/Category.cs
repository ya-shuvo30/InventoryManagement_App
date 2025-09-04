// src/BrightLifeIMS.Web/Models/Entities/Category.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? NameBn { get; set; }
        
        public string? Description { get; set; }
        
        public string? Icon { get; set; }
        
        public int DisplayOrder { get; set; } = 0;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}