using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly InteractDbContext _context;

        public LikeRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task AddLikeAsync(Like like)
        {
            await _context.Likes.AddAsync(like);
            await _context.SaveChangesAsync();
        }
    }
}
