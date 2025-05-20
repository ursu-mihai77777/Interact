using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface ILikeService
    {
        Task AddLikeAsync(int postId);  
    }
}
