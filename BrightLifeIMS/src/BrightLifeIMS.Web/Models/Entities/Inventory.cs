// src/BrightLifeIMS.Web/Models/Entities/Inventory.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;
        
        [MaxLength(255)]
        public string? TitleBn { get; set; } // Bengali translation
        
        public string? Description { get; set; }
        
        public string? DescriptionBn { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public string CreatorId { get; set; } = string.Empty;
        
        public string OwnerId { get; set; } = string.Empty; // Added missing property
        
        public int? CategoryId { get; set; }
        
        public bool IsPublic { get; set; } = false;
        
        public bool IsActive { get; set; } = true;
        
        // Custom ID format configuration (stored as JSON)
        public string? CustomIdFormat { get; set; }
        
        // Optimistic locking
        public int Version { get; set; } = 1;
        
        public int LikesCount { get; set; } = 0;
        
        public int ViewsCount { get; set; } = 0;
        
        // Custom field definitions (15 total: 3 each of string, int, bool, text, url)
        // String fields (1-3)
        public bool CustomString1State { get; set; } = false;
        public string? CustomString1Name { get; set; }
        public string? CustomString1Description { get; set; }
        public bool CustomString1Displayed { get; set; } = true;
        public bool CustomString1Required { get; set; } = false;
        public int CustomString1Order { get; set; } = 1;
        
        public bool CustomString2State { get; set; } = false;
        public string? CustomString2Name { get; set; }
        public string? CustomString2Description { get; set; }
        public bool CustomString2Displayed { get; set; } = true;
        public bool CustomString2Required { get; set; } = false;
        public int CustomString2Order { get; set; } = 2;
        
        public bool CustomString3State { get; set; } = false;
        public string? CustomString3Name { get; set; }
        public string? CustomString3Description { get; set; }
        public bool CustomString3Displayed { get; set; } = true;
        public bool CustomString3Required { get; set; } = false;
        public int CustomString3Order { get; set; } = 3;
        
        // Integer fields (1-3)
        public bool CustomInt1State { get; set; } = false;
        public string? CustomInt1Name { get; set; }
        public string? CustomInt1Description { get; set; }
        public bool CustomInt1Displayed { get; set; } = true;
        public bool CustomInt1Required { get; set; } = false;
        public int CustomInt1Order { get; set; } = 4;
        
        public bool CustomInt2State { get; set; } = false;
        public string? CustomInt2Name { get; set; }
        public string? CustomInt2Description { get; set; }
        public bool CustomInt2Displayed { get; set; } = true;
        public bool CustomInt2Required { get; set; } = false;
        public int CustomInt2Order { get; set; } = 5;
        
        public bool CustomInt3State { get; set; } = false;
        public string? CustomInt3Name { get; set; }
        public string? CustomInt3Description { get; set; }
        public bool CustomInt3Displayed { get; set; } = true;
        public bool CustomInt3Required { get; set; } = false;
        public int CustomInt3Order { get; set; } = 6;
        
        // Boolean fields (1-3)
        public bool CustomBool1State { get; set; } = false;
        public string? CustomBool1Name { get; set; }
        public string? CustomBool1Description { get; set; }
        public bool CustomBool1Displayed { get; set; } = true;
        public bool CustomBool1Required { get; set; } = false;
        public int CustomBool1Order { get; set; } = 7;
        
        public bool CustomBool2State { get; set; } = false;
        public string? CustomBool2Name { get; set; }
        public string? CustomBool2Description { get; set; }
        public bool CustomBool2Displayed { get; set; } = true;
        public bool CustomBool2Required { get; set; } = false;
        public int CustomBool2Order { get; set; } = 8;
        
        public bool CustomBool3State { get; set; } = false;
        public string? CustomBool3Name { get; set; }
        public string? CustomBool3Description { get; set; }
        public bool CustomBool3Displayed { get; set; } = true;
        public bool CustomBool3Required { get; set; } = false;
        public int CustomBool3Order { get; set; } = 9;
        
        // Text fields (1-3)
        public bool CustomText1State { get; set; } = false;
        public string? CustomText1Name { get; set; }
        public string? CustomText1Description { get; set; }
        public bool CustomText1Displayed { get; set; } = true;
        public bool CustomText1Required { get; set; } = false;
        public int CustomText1Order { get; set; } = 10;
        
        public bool CustomText2State { get; set; } = false;
        public string? CustomText2Name { get; set; }
        public string? CustomText2Description { get; set; }
        public bool CustomText2Displayed { get; set; } = true;
        public bool CustomText2Required { get; set; } = false;
        public int CustomText2Order { get; set; } = 11;
        
        public bool CustomText3State { get; set; } = false;
        public string? CustomText3Name { get; set; }
        public string? CustomText3Description { get; set; }
        public bool CustomText3Displayed { get; set; } = true;
        public bool CustomText3Required { get; set; } = false;
        public int CustomText3Order { get; set; } = 12;
        
        // URL fields (1-3)
        public bool CustomUrl1State { get; set; } = false;
        public string? CustomUrl1Name { get; set; }
        public string? CustomUrl1Description { get; set; }
        public bool CustomUrl1Displayed { get; set; } = true;
        public bool CustomUrl1Required { get; set; } = false;
        public int CustomUrl1Order { get; set; } = 13;
        
        public bool CustomUrl2State { get; set; } = false;
        public string? CustomUrl2Name { get; set; }
        public string? CustomUrl2Description { get; set; }
        public bool CustomUrl2Displayed { get; set; } = true;
        public bool CustomUrl2Required { get; set; } = false;
        public int CustomUrl2Order { get; set; } = 14;
        
        public bool CustomUrl3State { get; set; } = false;
        public string? CustomUrl3Name { get; set; }
        public string? CustomUrl3Description { get; set; }
        public bool CustomUrl3Displayed { get; set; } = true;
        public bool CustomUrl3Required { get; set; } = false;
        public int CustomUrl3Order { get; set; } = 15;
        
        // Auto-save tracking
        public DateTime? LastSavedAt { get; set; }
        public string? AutoSaveData { get; set; } // JSON data
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties - enabled for Phase 1
        public ICollection<Item> Items { get; set; } = new List<Item>();
        public Category? Category { get; set; }
        public ICollection<InventoryTag> InventoryTags { get; set; } = new List<InventoryTag>();
        
        // These will be added later when we enable additional entities
        // public User Creator { get; set; } = null!;
        // public ICollection<AutoSave> AutoSaves { get; set; } = new List<AutoSave>();
    }
}