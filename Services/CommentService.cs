using ThirdDelivery.Models;
using ThirdDelivery.Repository;

namespace ThirdDelivery.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPostRepository _postRepository;

        public CommentService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task AddCommentAsync(int postId, string content, string userId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post != null)
            {
                var comment = new Comment
                {
                    PostId = postId,
                    Content = content,
                    CreatedAt = DateTime.Now,
                    ApplicationUserId = userId
                };

                if (post.Comments == null)
                    post.Comments = new List<Comment>();

                post.Comments.Add(comment);

                await _postRepository.UpdateAsync(post);
            }
        }
    }
}
