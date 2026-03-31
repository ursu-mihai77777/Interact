using System.ComponentModel.DataAnnotations;

namespace ThirdDelivery.Models
{
     public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; } 

        [Required(ErrorMessage = "Content este obligatoriu.")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string UserName { get; set; } = "TEMP";  
        public bool IsGroupPost { get; set; } = false;

       

        public string ApplicationUserId { get; set; }

       
        public User ApplicationUser { get; set; }

        // ✅ Relații
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
