using ToyWebService.Application.BLL.DTO;
using ToyWebService.Application.DAL.Interfaces;

namespace ToyWebService.Application.DAL;

public class ItemRepository : IItemRepository
{
    public IAsyncEnumerable<Item> GetItems(long? startId, long? endId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}