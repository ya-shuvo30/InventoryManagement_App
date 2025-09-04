// src/BrightLifeIMS.Web/Services/Core/IAccessControlService.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Services.Core
{
    public interface IAccessControlService
    {
        Task<bool> CanViewInventoryAsync(User user, int inventoryId);
        Task<bool> CanEditInventoryAsync(User user, int inventoryId);
        Task<bool> CanDeleteInventoryAsync(User user, int inventoryId);
        Task<bool> CanAddItemAsync(User user, int inventoryId);
        Task<bool> CanEditItemAsync(User user, int itemId);
        Task<bool> CanDeleteItemAsync(User user, int itemId);
        bool IsAdmin(User user);
        bool IsCreator(User user);
    }
}