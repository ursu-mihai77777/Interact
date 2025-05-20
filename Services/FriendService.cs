using ThirdDelivery.Models;
using ThirdDelivery.Repository;

namespace ThirdDelivery.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendService(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            return await _friendRepository.GetAllFriendsAsync();
        }

        public async Task AddFriendAsync(Friend friend)
        {
            await _friendRepository.AddFriendAsync(friend);
        }

        public async Task RemoveFriendAsync(int id) 
        {
            await _friendRepository.RemoveFriendAsync(id);
        }
        public async Task<List<FriendSuggestion>> GetSuggestionsAsync(string userId)
        {
            return await _friendRepository.GetSuggestionsForUserAsync(userId);
        }
    }
}
