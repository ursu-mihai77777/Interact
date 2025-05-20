using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
        Task CreateAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        Task<List<Post>> GetAllPostsForWatchAsync();
    }
}
