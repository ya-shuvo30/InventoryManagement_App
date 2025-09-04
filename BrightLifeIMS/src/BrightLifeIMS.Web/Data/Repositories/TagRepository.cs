// src/BrightLifeIMS.Web/Data/Repositories/TagRepository.cs
using BrightLifeIMS.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrightLifeIMS.Web.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetPopularAsync(int count = 10)
        {
            return await _context.Tags
                .OrderByDescending(t => t.UsageCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetPopularAsync();
            }

            var lowerSearchTerm = searchTerm.ToLower();
            
            return await _context.Tags
                .Where(t => t.Name.ToLower().Contains(lowerSearchTerm) ||
                           (t.NameBn != null && t.NameBn.Contains(searchTerm)))
                .OrderByDescending(t => t.UsageCount)
                .Take(10)
                .ToListAsync();
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return false;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task IncrementUsageAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null)
            {
                tag.UsageCount++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecrementUsageAsync(int tagId)
        {
            var tag = await _context.Tags.FindAsync(tagId);
            if (tag != null && tag.UsageCount > 0)
            {
                tag.UsageCount--;
                await _context.SaveChangesAsync();
            }
        }
    }
}
