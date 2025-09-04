// src/BrightLifeIMS.Web/Models/ViewModels/InventoryViewModel.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Models.ViewModels
{
    public class InventoryViewModel
    {
        public Inventory Inventory { get; set; } = new();
        public List<Item> Items { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAddItems { get; set; }
        public string CurrentUserId { get; set; } = string.Empty;
        public string CurrentUserRole { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public string? SearchTerm { get; set; }
    }

    public class InventoryListViewModel
    {
        public List<Inventory> Inventories { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string? SelectedCategory { get; set; }
        public string? SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int TotalCount { get; set; }
        public string ViewType { get; set; } = "grid"; // grid, table
    }

    public class CreateInventoryViewModel
    {
        public Inventory Inventory { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<string> AvailableIdComponents { get; set; } = new();
        public Dictionary<string, List<string>> FieldTypeOptions { get; set; } = new();
    }
}
