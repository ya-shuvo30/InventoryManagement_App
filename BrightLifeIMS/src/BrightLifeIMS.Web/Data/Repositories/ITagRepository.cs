// src/BrightLifeIMS.Web/Data/Repositories/ITagRepository.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<Tag?> GetByNameAsync(string name);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<IEnumerable<Tag>> GetPopularAsync(int count = 10);
        Task<IEnumerable<Tag>> SearchAsync(string searchTerm);
        Task<Tag> CreateAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<bool> DeleteAsync(int id);
        Task IncrementUsageAsync(int tagId);
        Task DecrementUsageAsync(int tagId);
    }
}