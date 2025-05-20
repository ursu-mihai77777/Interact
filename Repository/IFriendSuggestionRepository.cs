using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface IFriendSuggestionRepository
    {
        Task<List<FriendSuggestion>> GetAllSuggestionsAsync();
        Task<FriendSuggestion?> GetSuggestionByIdAsync(int id);
        Task AddSuggestionAsync(FriendSuggestion suggestion);
        Task UpdateSuggestionAsync(FriendSuggestion suggestion);
        Task DeleteSuggestionAsync(int id);
        Task RemoveSuggestionAsync(int id);
       
    }
}
