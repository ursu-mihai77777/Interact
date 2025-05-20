using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface ICommentRepository
    {
        Task AddCommentAsync(Comment comment);
    }
}
