using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface IFriendSuggestionService
    {
        Task<List<FriendSuggestion>> GetAllSuggestionsAsync();
        Task<FriendSuggestion?> GetSuggestionByIdAsync(int id);
        Task AddSuggestionAsync(FriendSuggestion suggestion);
        Task RemoveSuggestionAsync(int id);  
        Task UpdateSuggestionAsync(FriendSuggestion suggestion);
    }
}
