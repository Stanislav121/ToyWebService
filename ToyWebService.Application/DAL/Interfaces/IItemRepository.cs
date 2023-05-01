using ToyWebService.Application.BLL.DTO;

namespace ToyWebService.Application.DAL.Interfaces;

public interface IItemRepository
{
    IAsyncEnumerable<Item> GetItems(long? startId, long? endId, CancellationToken token);
}