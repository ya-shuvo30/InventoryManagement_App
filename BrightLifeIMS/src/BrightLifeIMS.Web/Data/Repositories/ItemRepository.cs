using BrightLifeIMS.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(AppDbContext context, ILogger<ItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .Include(i => i.Inventory)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Item>> GetAllByInventoryIdAsync(int inventoryId)
        {
            return await _context.Items
                .Where(i => i.InventoryId == inventoryId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Item>> GetByInventoryAsync(int inventoryId)
        {
            // Alias method for controller compatibility
            return await GetAllByInventoryIdAsync(inventoryId);
        }

        public async Task<List<Item>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Items
                .Where(i => i.CreatedById == userId)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Item> CreateAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}