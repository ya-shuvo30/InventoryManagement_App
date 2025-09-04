// src/BrightLifeIMS.Web/Services/Core/LikeService.cs
using Microsoft.EntityFrameworkCore;
using BrightLifeIMS.Web.Data;
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Services.Core
{
    public class LikeService : ILikeService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<LikeService> _logger;

        public LikeService(AppDbContext context, ILogger<LikeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ToggleLikeAsync(int itemId, string userId)
        {
            var existingLike = await _context.ItemLikes
                .FirstOrDefaultAsync(l => l.ItemId == itemId && l.UserId == userId);

            if (existingLike != null)
            {
                // Unlike
                _context.ItemLikes.Remove(existingLike);
                await UpdateLikeCountAsync(itemId, -1);
                await _context.SaveChangesAsync();
                return false;
            }
            else
            {
                // Like
                _context.ItemLikes.Add(new ItemLike
                {
                    ItemId = itemId,
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                });
                await UpdateLikeCountAsync(itemId, 1);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        private async Task UpdateLikeCountAsync(int itemId, int change)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item != null)
            {
                item.LikesCount = Math.Max(0, item.LikesCount + change);
            }
        }

        public async Task<bool> IsLikedByUserAsync(int itemId, string userId)
        {
            return await _context.ItemLikes
                .AnyAsync(l => l.ItemId == itemId && l.UserId == userId);
        }

        public async Task<int> GetLikeCountAsync(int itemId)
        {
            return await _context.ItemLikes
                .CountAsync(l => l.ItemId == itemId);
        }

        public async Task<IEnumerable<int>> GetUserLikedItemsAsync(string userId)
        {
            return await _context.ItemLikes
                .Where(l => l.UserId == userId)
                .Select(l => l.ItemId)
                .ToListAsync();
        }
    }
}

