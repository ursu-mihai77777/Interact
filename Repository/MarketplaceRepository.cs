
using ThirdDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace ThirdDelivery.Repository
{
    public class MarketplaceRepository : IMarketplaceRepository
    {
        private readonly InteractDbContext _context;

        public MarketplaceRepository(InteractDbContext context)
        {
            _context = context;
        }

        public async Task<List<MarketplaceItem>> GetAllAsync()
        {
            return await _context.MarketplaceItems.Include(i => i.ApplicationUser).ToListAsync();
        }

        public async Task<MarketplaceItem> GetByIdAsync(int id)
        {
            return await _context.MarketplaceItems.Include(i => i.ApplicationUser).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateAsync(MarketplaceItem item)
        {
            _context.MarketplaceItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MarketplaceItem item)
        {
            _context.MarketplaceItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MarketplaceItems.FindAsync(id);
            if (item != null)
            {
                _context.MarketplaceItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        

    }
}
