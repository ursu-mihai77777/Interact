using ThirdDelivery.Models;

namespace ThirdDelivery.Services
{
    public interface IMarketplaceService
    {
        Task<List<MarketplaceItem>> GetAllItemsAsync();
        Task<MarketplaceItem> GetItemByIdAsync(int id);
        Task CreateItemAsync(MarketplaceItem item);
        Task UpdateItemAsync(MarketplaceItem item);
        Task DeleteItemAsync(int id);
        Task AddItemAsync(MarketplaceItem item);
    }
}
