using ThirdDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace ThirdDelivery.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly InteractDbContext _context;

        public PostRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.Comments)
                  .ThenInclude(c => c.ApplicationUser) // AICI e cheia!
                .Include(p => p.Likes)
                .Include(p => p.ApplicationUser) // ✅ adaugă asta

                .ToListAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts
    .Include(p => p.Likes)
     
    .Include(p => p.Comments)
        .ThenInclude(c => c.ApplicationUser)

    .FirstOrDefaultAsync(p => p.PostId == id); // ✅ returnează un singur Post
        }



        public async Task CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Post>> GetAllPostsForWatchAsync()
        {
            return await _context.Posts
                .Where(p => p.VideoUrl != null)
                .Include(p => p.ApplicationUser)
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .ToListAsync();
        }
    }
}
