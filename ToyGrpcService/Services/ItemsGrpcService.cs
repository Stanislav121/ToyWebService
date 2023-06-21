using Grpc.Core;
using ToyWebService.Application.BLL.Service.Interfaces;

namespace ToyGrpcService.Services;

public class ItemsGrpcService : Items.ItemsBase
{
    private readonly IItemService _itemService;

    public ItemsGrpcService(IItemService itemService)
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

    public override async Task<V1GetItemResponse> V1GetItem(V1GetItemRequest request, ServerCallContext context)
    {
        var result = await _itemService.GetItem(request.Id, context.CancellationToken);
        return new V1GetItemResponse
        {
            Id = result.Id,
            Name = result.Name,
        };
    }
}