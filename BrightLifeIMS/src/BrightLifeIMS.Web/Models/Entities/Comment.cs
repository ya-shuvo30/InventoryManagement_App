// src/BrightLifeIMS.Web/Models/Entities/Comment.cs
using System.ComponentModel.DataAnnotations;

namespace BrightLifeIMS.Web.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        
        public int ItemId { get; set; }
        
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public int? ParentCommentId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? EditedAt { get; set; }
        
        // Navigation properties
        public Item Item { get; set; } = null!;
        public User User { get; set; } = null!;
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
