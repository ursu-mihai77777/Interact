using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface IFriendRepository
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task AddFriendAsync(Friend friend);
        Task RemoveFriendAsync(int id);  // 🔥 AICI adaugă
        Task<List<FriendSuggestion>> GetSuggestionsForUserAsync(string userId);
    }
}
