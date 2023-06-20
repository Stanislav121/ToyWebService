using ToyWebService.Application.BLL.Entities;

namespace ToyWebService.Application.BLL.Repositories;

public interface IItemRepository
{
    Task<Item> GetItem(long id, CancellationToken token);
    
    Task<Item[]> GetItems(long? startId, long? endId, CancellationToken token);

    IAsyncEnumerable<Item> GetItemsStream(long? startId, long? endId, CancellationToken token);
}