// src/BrightLifeIMS.Web/Models/ViewModels/ItemViewModel.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Models.ViewModels
{
    public class ItemViewModel
    {
        public Item Item { get; set; } = new();
        public Inventory Inventory { get; set; } = new();
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public string CurrentUserId { get; set; } = string.Empty;
    }

    public class ItemListViewModel
    {
        public Inventory Inventory { get; set; } = new();
        public List<Item> Items { get; set; } = new();
        public bool CanAddItems { get; set; }
        public bool CanEditInventory { get; set; }
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalCount { get; set; }
        public string SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
    }

    public class CreateItemViewModel
    {
        public Item Item { get; set; } = new();
        public Inventory Inventory { get; set; } = new();
        public string GeneratedId { get; set; } = string.Empty;
        public Dictionary<string, object> CustomFieldValues { get; set; } = new();
    }
}
