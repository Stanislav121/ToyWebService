using ToyWebService.Application.BLL.Entities;
using ToyWebService.Application.BLL.Service.Interfaces;
using ToyWebService.Application.DAL.Interfaces;

namespace ToyWebService.Application.BLL.Service;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public IAsyncEnumerable<Item> GetItems(long? startId, long? endId, CancellationToken token)
    {
        return _itemRepository.GetItems(startId, endId, token)
            .Select(ins => new Item
            {
                Id = ins.Id,
            });
    }
}