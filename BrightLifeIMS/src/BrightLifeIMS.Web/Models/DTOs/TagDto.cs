// src/BrightLifeIMS.Web/Models/DTOs/TagDto.cs
namespace BrightLifeIMS.Web.Models.DTOs
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? NameBn { get; set; }
        public int UsageCount { get; set; }
    }

    public class TagSuggestionDto
    {
        public string Name { get; set; } = string.Empty;
        public string? NameBn { get; set; }
        public int UsageCount { get; set; }
    }
}