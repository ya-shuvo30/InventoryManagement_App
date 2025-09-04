// src/BrightLifeIMS.Web/Services/Core/ITagService.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Services.Core
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetSuggestionsAsync(string partial);
        Task<Tag> GetOrCreateTagAsync(string tagName);
        Task AttachTagsToInventoryAsync(int inventoryId, List<string> tagNames);
        Task RemoveTagFromInventoryAsync(int inventoryId, int tagId);
        Task<IEnumerable<Tag>> GetInventoryTagsAsync(int inventoryId);
        Task<IEnumerable<Tag>> GetPopularTagsAsync(int count = 20);
    }
}
