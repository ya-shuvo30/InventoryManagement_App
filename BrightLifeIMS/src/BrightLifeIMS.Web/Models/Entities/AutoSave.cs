// src/BrightLifeIMS.Web/Models/Entities/AutoSave.cs
namespace BrightLifeIMS.Web.Models.Entities
{
    public class AutoSave
    {
        public int Id { get; set; }
        
        public int InventoryId { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        public string SaveData { get; set; } = string.Empty; // JSON data
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Inventory Inventory { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
