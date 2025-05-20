using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task CreatePostAsync(Post post, string userId);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id); // 🔴 Asigură-te că există!
        Task AddCommentAsync(int postId, string content, string userId);
        Task LikePostAsync(int postId);
        Task<List<Post>> GetAllPostsForWatchAsync();

       
    }
}
