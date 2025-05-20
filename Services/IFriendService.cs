using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface IFriendService
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task AddFriendAsync(Friend friend);
        Task RemoveFriendAsync(int id);
        Task<List<FriendSuggestion>> GetSuggestionsAsync(string userId);
    }
}
