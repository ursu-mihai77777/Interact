using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace ThirdDelivery.Models
{


    public class InteractDbContext : IdentityDbContext<User>

    {
        public InteractDbContext(DbContextOptions<InteractDbContext> options) : base(options) { }

        public DbSet<Like> Likes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public new DbSet<User> Users { get; set; }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<MarketplaceItem> MarketplaceItems { get; set; }

        public DbSet<FriendSuggestion> FriendSuggestions { get; set; }

       
        }
}