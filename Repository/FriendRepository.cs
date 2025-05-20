using ThirdDelivery.Models;
using Microsoft.EntityFrameworkCore;
namespace ThirdDelivery.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private readonly InteractDbContext _context;

        public FriendRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task<List<Friend>> GetAllFriendsAsync()
        {
            return await _context.Friends.ToListAsync();
        }

        public async Task AddFriendAsync(Friend friend)
        {
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriendAsync(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend != null)
            {
                _context.Friends.Remove(friend);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<FriendSuggestion>> GetSuggestionsForUserAsync(string userId)
        {
            return await _context.FriendSuggestions
                .Where(s => s.ApplicationUserId != userId)
                .ToListAsync();
        }
    }
}
