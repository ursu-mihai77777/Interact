using NuGet.Protocol.Core.Types;
using ThirdDelivery.Models;
using ThirdDelivery.Repository;

namespace ThirdDelivery.Services
{
    public class FriendSuggestionService : IFriendSuggestionService
    {
        private readonly IFriendSuggestionRepository _friendSuggestionRepository;

        public FriendSuggestionService(IFriendSuggestionRepository friendSuggestionRepository)
        {
            _friendSuggestionRepository = friendSuggestionRepository;
        }

        public async Task<List<FriendSuggestion>> GetAllSuggestionsAsync()
        {
            return await _friendSuggestionRepository.GetAllSuggestionsAsync();
        }

        public async Task<FriendSuggestion?> GetSuggestionByIdAsync(int id)
        {
            return await _friendSuggestionRepository.GetSuggestionByIdAsync(id);
        }

        public async Task AddSuggestionAsync(FriendSuggestion suggestion)
        {
            await _friendSuggestionRepository.AddSuggestionAsync(suggestion);
        }

        public async Task RemoveSuggestionAsync(int id) 
        {
            await _friendSuggestionRepository.RemoveSuggestionAsync(id);
        }
        public async Task UpdateSuggestionAsync(FriendSuggestion suggestion)
        {
            await _friendSuggestionRepository.UpdateSuggestionAsync(suggestion);
        }
    }
}
