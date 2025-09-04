// src/BrightLifeIMS.Web/Data/Repositories/InventoryRepository.cs
using BrightLifeIMS.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<InventoryRepository> _logger;

        public InventoryRepository(AppDbContext context, ILogger<InventoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Inventory?> GetByIdAsync(int id, bool includeItems = false)
        {
            var query = _context.Inventories.AsQueryable();

            if (includeItems)
            {
                query = query.Include(i => i.Items);
            }

            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync(bool includeInactive = false)
        {
            var query = _context.Inventories.AsQueryable();

            if (!includeInactive)
            {
                query = query.Where(i => i.IsActive);
            }

            return await query.OrderByDescending(i => i.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetByUserAsync(string userId)
        {
            return await _context.Inventories
                .Where(i => i.CreatorId == userId && i.IsActive)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetPublicAsync()
        {
            return await _context.Inventories
                .Where(i => i.IsPublic && i.IsActive)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetPublicAsync();
            }

            var lowerSearchTerm = searchTerm.ToLower();
            
            return await _context.Inventories
                .Where(i => i.IsActive && i.IsPublic &&
                    (i.Title.ToLower().Contains(lowerSearchTerm) ||
                     (i.Description != null && i.Description.ToLower().Contains(lowerSearchTerm)) ||
                     (i.TitleBn != null && i.TitleBn.Contains(searchTerm))))
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Inventory> CreateAsync(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<Inventory> UpdateAsync(Inventory inventory)
        {
            inventory.Version++;
            inventory.UpdatedAt = DateTime.UtcNow;
            _context.Inventories.Update(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return false;
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Inventories.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inventory>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Inventories
                .Where(i => i.CategoryId == categoryId && i.IsActive)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetByTagAsync(int tagId)
        {
            return await _context.Inventories
                .Where(i => i.IsActive)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetNextSequenceNumberAsync(int inventoryId)
        {
            var lastItem = await _context.Items
                .Where(i => i.InventoryId == inventoryId)
                .OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();

            return lastItem?.Id ?? 0 + 1;
        }

        public async Task<bool> CheckOptimisticLockAsync(int id, int version)
        {
            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
            
            return inventory != null && inventory.Version == version;
        }
    }
}
