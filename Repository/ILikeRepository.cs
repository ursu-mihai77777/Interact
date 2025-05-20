using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface ILikeRepository
    {
        Task AddLikeAsync(Like like);  
    }
}
