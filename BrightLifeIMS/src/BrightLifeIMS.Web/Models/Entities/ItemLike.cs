// src/BrightLifeIMS.Web/Models/Entities/ItemLike.cs
namespace BrightLifeIMS.Web.Models.Entities
{
    public class ItemLike
    {
        public int Id { get; set; }
        
        public int ItemId { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Item Item { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}