// src/BrightLifeIMS.Web/Services/Core/IIDGeneratorService.cs
namespace BrightLifeIMS.Web.Services.Core
{
    public interface IIDGeneratorService
    {
        Task<string> GenerateIDAsync(string format, int inventoryId);
        string PreviewID(string format);
        bool ValidateFormat(string format);
        List<string> GetAvailableComponents();
    }
}