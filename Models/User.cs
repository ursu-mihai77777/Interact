using Microsoft.AspNetCore.Identity;

namespace ThirdDelivery.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }

}
