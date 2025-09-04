// src/BrightLifeIMS.Web/Services/Core/ILikeService.cs
namespace BrightLifeIMS.Web.Services.Core
{
    public interface ILikeService
    {
        Task<bool> ToggleLikeAsync(int itemId, string userId);
        Task<int> GetLikeCountAsync(int itemId);
        Task<bool> IsLikedByUserAsync(int itemId, string userId);
        Task<IEnumerable<int>> GetUserLikedItemsAsync(string userId);
    }
}