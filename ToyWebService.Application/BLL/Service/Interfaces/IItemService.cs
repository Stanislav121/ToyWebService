using ToyWebService.Application.BLL.Entities;

namespace ToyWebService.Application.BLL.Service.Interfaces;

public interface IItemService
{
    IAsyncEnumerable<Item> GetItems(long? startId, long? endId, CancellationToken token);
    Task<Item> GetItem(long id, CancellationToken token);
}