using ToyWebService.Application.BLL.Entities;
using ToyWebService.Application.BLL.Repositories;
using ToyWebService.Application.BLL.Service.Interfaces;

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
        return _itemRepository.GetItemsStream(startId, endId, token)
            .Select(ins => new Item
            {
                Id = ins.Id,
            });
    }

    public Task<Item> GetItem(long id, CancellationToken token)
    {
        return _itemRepository.GetItem(id, token);
    }
}