// src/BrightLifeIMS.Web/Models/DTOs/ItemDto.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string? CustomId { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public int Version { get; set; }
        public int LikesCount { get; set; }
        public string? CustomString1 { get; set; }
        public string? CustomString2 { get; set; }
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
        public string? CustomUrl1 { get; set; }
        public string? CustomUrl2 { get; set; }
        public string? CustomUrl3 { get; set; }
        public string? CloudImages { get; set; }
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateItemDto
    {
        public int InventoryId { get; set; }
        public string? CustomId { get; set; }
        public string? CustomString1 { get; set; }
        public string? CustomString2 { get; set; }
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
        public string? CustomUrl1 { get; set; }
        public string? CustomUrl2 { get; set; }
        public string? CustomUrl3 { get; set; }
        public string? CloudImages { get; set; }
        public Dictionary<string, object> CustomFields { get; set; } = new();
    }

    public class UpdateItemDto
    {
        public string? CustomId { get; set; }
        public string? CustomString1 { get; set; }
        public string? CustomString2 { get; set; }
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
        public string? CustomUrl1 { get; set; }
        public string? CustomUrl2 { get; set; }
        public string? CustomUrl3 { get; set; }
        public string? CloudImages { get; set; }
        public Dictionary<string, object> CustomFields { get; set; } = new();
        public int Version { get; set; }
    }
}