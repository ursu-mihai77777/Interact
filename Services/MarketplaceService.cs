using ThirdDelivery.Models;
using ThirdDelivery.Repository;
using System.Security.Claims;
namespace ThirdDelivery.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly IMarketplaceRepository _repository;

        public MarketplaceService(IMarketplaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MarketplaceItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<MarketplaceItem> GetItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateItemAsync(MarketplaceItem item)
        {
            await _repository.CreateAsync(item);
        }

        public async Task UpdateItemAsync(MarketplaceItem item)
        {
            await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task AddItemAsync(MarketplaceItem item)
        {
            await _repository.CreateAsync(item);
        }

    }
}
