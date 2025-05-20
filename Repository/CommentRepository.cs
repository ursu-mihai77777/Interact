using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly InteractDbContext _context;

        public CommentRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}
