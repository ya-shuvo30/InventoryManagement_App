// src/BrightLifeIMS.Web/Services/Core/AutoSaveService.cs
using BrightLifeIMS.Web.Data;
using BrightLifeIMS.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace BrightLifeIMS.Web.Services.Core
{
    public class AutoSaveService : IAutoSaveService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AutoSaveService> _logger;

        public AutoSaveService(AppDbContext context, ILogger<AutoSaveService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string> SaveInventoryDraftAsync(int inventoryId, string userId, object data)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(data);
                
                var autoSave = new AutoSave
                {
                    InventoryId = inventoryId,
                    UserId = userId,
                    SaveData = jsonData,
                    CreatedAt = DateTime.UtcNow
                };

                _context.AutoSaves.Add(autoSave);
                
                // Also update the inventory's last saved time
                var inventory = await _context.Inventories.FindAsync(inventoryId);
                if (inventory != null)
                {
                    inventory.LastSavedAt = DateTime.UtcNow;
                    inventory.AutoSaveData = jsonData;
                }

                await _context.SaveChangesAsync();

                // Clean up old auto-saves (keep only last 10)
                var oldAutoSaves = await _context.AutoSaves
                    .Where(a => a.InventoryId == inventoryId && a.UserId == userId)
                    .OrderByDescending(a => a.CreatedAt)
                    .Skip(10)
                    .ToListAsync();

                _context.AutoSaves.RemoveRange(oldAutoSaves);
                await _context.SaveChangesAsync();

                return "saved";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving draft for inventory {InventoryId}", inventoryId);
                return "error";
            }
        }

        public async Task<T?> GetLatestDraftAsync<T>(int inventoryId, string userId) where T : class
        {
            var autoSave = await _context.AutoSaves
                .Where(a => a.InventoryId == inventoryId && a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefaultAsync();

            if (autoSave == null || string.IsNullOrEmpty(autoSave.SaveData))
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<T>(autoSave.SaveData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing draft for inventory {InventoryId}", inventoryId);
                return null;
            }
        }

        public async Task ClearDraftsAsync(int inventoryId, string userId)
        {
            var drafts = await _context.AutoSaves
                .Where(a => a.InventoryId == inventoryId && a.UserId == userId)
                .ToListAsync();

            _context.AutoSaves.RemoveRange(drafts);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasConflictAsync(int inventoryId, int version)
        {
            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inventoryId);

            return inventory != null && inventory.Version != version;
        }


    }
}