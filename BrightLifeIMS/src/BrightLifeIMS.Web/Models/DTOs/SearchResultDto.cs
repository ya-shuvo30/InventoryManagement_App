// src/BrightLifeIMS.Web/Models/DTOs/SearchResultDto.cs
namespace BrightLifeIMS.Web.Models.DTOs
{
    public class SearchResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

    public class GlobalSearchResultDto
    {
        public List<InventoryDto> Inventories { get; set; } = new();
        public List<ItemDto> Items { get; set; } = new();
        public int TotalResults { get; set; }
        public string SearchTerm { get; set; } = string.Empty;
        public TimeSpan SearchTime { get; set; }
    }
}