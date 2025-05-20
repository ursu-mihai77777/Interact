using ThirdDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace ThirdDelivery.Repository
{
    public class FriendSuggestionRepository : IFriendSuggestionRepository
    {
        private readonly InteractDbContext _context;

        public FriendSuggestionRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task<List<FriendSuggestion>> GetAllSuggestionsAsync()
        {
            return await _context.FriendSuggestions.ToListAsync();
        }

        public async Task<FriendSuggestion?> GetSuggestionByIdAsync(int id)
        {
            return await _context.FriendSuggestions.FindAsync(id);
        }

        public async Task AddSuggestionAsync(FriendSuggestion suggestion)
        {
            _context.FriendSuggestions.Add(suggestion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSuggestionAsync(FriendSuggestion suggestion)
        {
            _context.FriendSuggestions.Update(suggestion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSuggestionAsync(int id)
        {
            var suggestion = await _context.FriendSuggestions.FindAsync(id);
            if (suggestion != null)
            {
                _context.FriendSuggestions.Remove(suggestion);
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveSuggestionAsync(int id)
        {
            var suggestion = await _context.FriendSuggestions.FindAsync(id);
            if (suggestion != null)
            {
                _context.FriendSuggestions.Remove(suggestion);
                await _context.SaveChangesAsync();
            }
        }
       
    }
}
