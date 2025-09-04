using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data.Repositories;

public interface IItemRepository
{
    Task<Item?> GetByIdAsync(int id);
    Task<List<Item>> GetAllByInventoryIdAsync(int inventoryId);
    Task<List<Item>> GetByInventoryAsync(int inventoryId); // Alias for GetAllByInventoryIdAsync
    Task<List<Item>> GetAllByUserIdAsync(string userId);
    Task AddAsync(Item item);
    Task<Item> CreateAsync(Item item); // Add CreateAsync method
    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
}
