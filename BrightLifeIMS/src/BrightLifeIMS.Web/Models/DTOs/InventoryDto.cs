// src/BrightLifeIMS.Web/Models/DTOs/InventoryDto.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? TitleBn { get; set; }
        public string? Description { get; set; }
        public string? DescriptionBn { get; set; }
        public string? ImageUrl { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public int LikesCount { get; set; }
        public int ViewsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateInventoryDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? TitleBn { get; set; }

        public string? Description { get; set; }
        public string? DescriptionBn { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public bool IsPublic { get; set; } = false;
        public string? CustomIdFormat { get; set; }
    }

    public class UpdateInventoryDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? TitleBn { get; set; }

        public string? Description { get; set; }
        public string? DescriptionBn { get; set; }
        public string? ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public int Version { get; set; }
    }


}