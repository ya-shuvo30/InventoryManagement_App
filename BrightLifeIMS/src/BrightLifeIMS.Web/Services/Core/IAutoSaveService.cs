// src/BrightLifeIMS.Web/Services/Core/IAutoSaveService.cs
namespace BrightLifeIMS.Web.Services.Core
{
    public interface IAutoSaveService
    {
        Task<string> SaveInventoryDraftAsync(int inventoryId, string userId, object data);
        Task<T?> GetLatestDraftAsync<T>(int inventoryId, string userId) where T : class;
        Task ClearDraftsAsync(int inventoryId, string userId);
        Task<bool> HasConflictAsync(int inventoryId, int version);
    }
}