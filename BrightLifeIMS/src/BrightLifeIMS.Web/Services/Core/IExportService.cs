// src/BrightLifeIMS.Web/Services/Core/IExportService.cs
namespace BrightLifeIMS.Web.Services.Core
{
    public interface IExportService
    {
        Task<byte[]> ExportToCSVAsync(int inventoryId);
        Task<byte[]> ExportToExcelAsync(int inventoryId);
        Task<string> GenerateExportFilenameAsync(int inventoryId, string extension);
    }
}