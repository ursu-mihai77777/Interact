using ThirdDelivery.Models;

namespace ThirdDelivery.Repository
{
    public interface IMarketplaceRepository
    {
        Task<List<MarketplaceItem>> GetAllAsync();
        Task<MarketplaceItem> GetByIdAsync(int id);
        Task CreateAsync(MarketplaceItem item);
        Task UpdateAsync(MarketplaceItem item);
        Task DeleteAsync(int id);
        
    }
}
