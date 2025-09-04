// src/BrightLifeIMS.Web/Models/DTOs/AutoSaveDto.cs
namespace BrightLifeIMS.Web.Models.DTOs
{
    public class AutoSaveDto
    {
        public int InventoryId { get; set; }
        public object Data { get; set; } = new { };
        public string Status { get; set; } = "saving"; // saving, saved, error
    }

    public class AutoSaveResponseDto
    {
        public string Status { get; set; } = string.Empty;
        public DateTime? LastSaved { get; set; }
        public string? Message { get; set; }
    }
}