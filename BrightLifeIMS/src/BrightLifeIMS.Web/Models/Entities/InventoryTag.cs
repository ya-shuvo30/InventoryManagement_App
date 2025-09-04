// src/BrightLifeIMS.Web/Models/Entities/InventoryTag.cs
namespace BrightLifeIMS.Web.Models.Entities
{
    public class InventoryTag
    {
        public int InventoryId { get; set; }
        public int TagId { get; set; }
        
        // Navigation properties
        public Inventory Inventory { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}