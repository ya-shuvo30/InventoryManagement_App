// src/BrightLifeIMS.Web/Data/Repositories/IInventoryRepository.cs
using BrightLifeIMS.Web.Models.Entities;
using System.Linq.Expressions;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByIdAsync(int id, bool includeItems = false);
        Task<IEnumerable<Inventory>> GetAllAsync(bool includeInactive = false);
        Task<IEnumerable<Inventory>> GetByUserAsync(string userId);
        Task<IEnumerable<Inventory>> GetPublicAsync();
        Task<IEnumerable<Inventory>> SearchAsync(string searchTerm);
        Task<Inventory> CreateAsync(Inventory inventory);
        Task<Inventory> UpdateAsync(Inventory inventory);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Inventory>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Inventory>> GetByTagAsync(int tagId);
        Task<int> GetNextSequenceNumberAsync(int inventoryId);
        Task<bool> CheckOptimisticLockAsync(int id, int version);
    }
}