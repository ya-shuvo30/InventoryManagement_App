// src/BrightLifeIMS.Web/Models/ViewModels/HomeViewModel.cs
using BrightLifeIMS.Web.Models.Entities;

namespace BrightLifeIMS.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Inventory> RecentInventories { get; set; } = new();
        public List<Inventory> PopularInventories { get; set; } = new();
        public List<Tag> PopularTags { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public DashboardStats Stats { get; set; } = new();
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
    }

    public class DashboardStats
    {
        public int TotalInventories { get; set; }
        public int TotalItems { get; set; }
        public int TotalUsers { get; set; }
        public int RecentActivity { get; set; }
        public List<ActivityItem> RecentActivities { get; set; } = new();
    }

    public class ActivityItem
    {
        public string Type { get; set; } = string.Empty; // inventory_created, item_added, etc.
        public string Description { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? RelatedUrl { get; set; }
    }
}