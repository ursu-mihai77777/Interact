using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ThirdDelivery.Models;
using ThirdDelivery.Repository;

namespace ThirdDelivery.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _postRepository.UpdateAsync(post);
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task CreatePostAsync(Post post, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID is null — utilizatorul nu este autentificat.");
            }

            post.ApplicationUserId = userId;
            await _postRepository.CreateAsync(post);
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task LikePostAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                post.Likes ??= new List<Like>();
                post.Likes.Add(new Like { PostId = postId });
                await _postRepository.UpdateAsync(post);
            }
        }

        public async Task AddCommentAsync(int postId, string content, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                post.Comments ??= new List<Comment>();
                post.Comments.Add(new Comment
                {
                    Content = content,
                    CreatedAt = DateTime.Now,
                    ApplicationUserId = userId,
                    PostId = postId
                });

                await _postRepository.UpdateAsync(post);
            }
        }

        public async Task<List<Post>> GetAllPostsForWatchAsync()
        {
            return await _postRepository.GetAllPostsForWatchAsync();
        }
    }
}
