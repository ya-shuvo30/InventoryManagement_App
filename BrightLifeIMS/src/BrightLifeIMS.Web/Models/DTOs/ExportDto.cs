// src/BrightLifeIMS.Web/Models/DTOs/ExportDto.cs
namespace BrightLifeIMS.Web.Models.DTOs
{
    public class ExportRequestDto
    {
        public int InventoryId { get; set; }
        public string Format { get; set; } = "csv"; // csv, excel
        public bool IncludeMetadata { get; set; } = false;
    }
}