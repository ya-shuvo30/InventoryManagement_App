// src/BrightLifeIMS.Web/Data/Repositories/ICategoryRepository.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync(bool includeInactive = false);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
    }
}