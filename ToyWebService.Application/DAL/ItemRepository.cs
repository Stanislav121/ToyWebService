using ToyWebService.Application.BLL.Entities;
using ToyWebService.Application.DAL.Utils;
using Dapper;
using ToyWebService.Application.BLL.Exceptions;
using ToyWebService.Application.BLL.Repositories;
using ToyWebService.Application.DAL.Models.Item;

namespace ToyWebService.Application.DAL;

public class ItemRepository : BasicRepository, IItemRepository
{
    private readonly string _tableName = "item";
    
    public ItemRepository(IDataBase dataBase) : base(dataBase)
    {
    }
    
    public async Task<Item> GetItem(long id, CancellationToken token)
    {
        var query = @$"select id, name, tag_ids from {_tableName} where id = @Id";

        // TODO переделай по новому
        var queryParams = new
        {
            Id = id
        };

        await using var connection = await GetConnection(token);
        var item = await connection.QuerySingleOrDefaultAsync<ItemV1>(new CommandDefinition(
            query, queryParams, cancellationToken: token));

        if (item == null)
        {
            throw new EntityNotFoundException(id);
        }

        return item.Map();
    }

    public Task<Item[]> GetItems(long? startId, long? endId, CancellationToken token)
    {
        throw new NotImplementedException();
        /*var query = @$"select id, name, tag_ids from {_tableName} where id = $Id";
        
        

        var queryParams = new
        {
            Id = id
        };

        await using var connection = await GetConnection(token);
        var item = connection.

            // Чтобы что материализовать данные до закрытия соединения с БД
        return item.ToArrya();*/
    }

    public IAsyncEnumerable<Item> GetItemsStream(long? startId, long? endId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    
}