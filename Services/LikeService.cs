using ThirdDelivery.Models;
using ThirdDelivery.Repository;

namespace ThirdDelivery.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository; 

        public LikeService(ILikeRepository likeRepository) 
        {
            _likeRepository = likeRepository; 
        }

        public async Task AddLikeAsync(int postId)
        {
            var like = new Like
            {
                PostId = postId
            };

            await _likeRepository.AddLikeAsync(like);
        }
    }
}
