using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface ICommentService
    {
        Task AddCommentAsync(int postId, string content, string userId);

    }
}
