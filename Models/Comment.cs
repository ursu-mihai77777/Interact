using System.ComponentModel.DataAnnotations.Schema;

namespace ThirdDelivery.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
        // 🔧 Relația cu User
        public string? ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public User ApplicationUser { get; set; }
    }
}


