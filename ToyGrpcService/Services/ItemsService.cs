using Grpc.Core;
using ToyWebService.Application.BLL.Service.Interfaces;

namespace ToyGrpcService.Services;

public class ItemsService : Items.ItemsBase
{
    private readonly IItemService _itemService;

    public ItemsService(IItemService itemService)
    {
        _itemService = itemService;
    }

    public override async Task V1GetItems(V1GetItemsRequest request, IServerStreamWriter<V1GetItemsResponse> responseStream, ServerCallContext context)
    {
        await foreach (var instance in _itemService.GetItems(
                           request.StartId, request.FinishId, context.CancellationToken))
        {
            await responseStream.WriteAsync(new V1GetItemsResponse
            {
                ItemId = instance.Id,
            });
        }
    }
}