using Microsoft.EntityFrameworkCore;
using BrightLifeIMS.Web.Data;
using BrightLifeIMS.Web.Models.Entities;
using BrightLifeIMS.Web.Data.Repositories;

namespace BrightLifeIMS.Web.Services.Core
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly AppDbContext _context;
        private readonly ILogger<TagService> _logger;

        public TagService(
            ITagRepository tagRepository,
            AppDbContext context,
            ILogger<TagService> logger)
        {
            _tagRepository = tagRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Tag>> GetSuggestionsAsync(string partial)
        {
            if (string.IsNullOrWhiteSpace(partial))
            {
                return await _tagRepository.GetPopularAsync();
            }

            return await _tagRepository.SearchAsync(partial);
        }

        public async Task<Tag> GetOrCreateTagAsync(string tagName)
        {
            var normalizedName = tagName.Trim().ToLower();
            var existingTag = await _tagRepository.GetByNameAsync(normalizedName);

            if (existingTag != null)
            {
                return existingTag;
            }

            var newTag = new Tag
            {
                Name = tagName.Trim(),
                UsageCount = 0
            };

            return await _tagRepository.CreateAsync(newTag);
        }

        public async Task AttachTagsToInventoryAsync(int inventoryId, List<string> tagNames)
        {
            // Remove existing tags
            var existingTags = await _context.InventoryTags
                .Where(it => it.InventoryId == inventoryId)
                .ToListAsync();

            _context.InventoryTags.RemoveRange(existingTags);

            // Decrement usage count for removed tags
            foreach (var existingTag in existingTags)
            {
                await _tagRepository.DecrementUsageAsync(existingTag.TagId);
            }

            // Add new tags
            foreach (var tagName in tagNames.Distinct())
            {
                if (string.IsNullOrWhiteSpace(tagName))
                    continue;

                var tag = await GetOrCreateTagAsync(tagName);

                _context.InventoryTags.Add(new InventoryTag
                {
                    InventoryId = inventoryId,
                    TagId = tag.Id
                });

                await _tagRepository.IncrementUsageAsync(tag.Id);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveTagFromInventoryAsync(int inventoryId, int tagId)
        {
            var inventoryTag = await _context.InventoryTags
                .FirstOrDefaultAsync(it => it.InventoryId == inventoryId && it.TagId == tagId);

            if (inventoryTag != null)
            {
                _context.InventoryTags.Remove(inventoryTag);
                await _tagRepository.DecrementUsageAsync(tagId);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tag>> GetInventoryTagsAsync(int inventoryId)
        {
            return await _context.InventoryTags
                .Where(it => it.InventoryId == inventoryId)
                .Include(it => it.Tag)
                .Select(it => it.Tag)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetPopularTagsAsync(int count = 20)
        {
            return await _tagRepository.GetPopularAsync(count);
        }
    }
}