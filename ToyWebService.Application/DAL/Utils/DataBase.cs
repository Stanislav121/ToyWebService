using System.Data.Common;
using System.Transactions;
using Npgsql;
using Npgsql.NameTranslation;
using ToyWebService.Application.DAL.Models.Item;

namespace ToyWebService.Application.DAL.Utils;

public class DataBase : IDataBase
{
    private readonly NpgsqlDataSource _dataSource;

    private readonly long _transactionTimeout;
    
    public DataBase(string connString, long transactionTimeout)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connString)
        {
            // How Postgres will map names from db to CLR classes
            // Прежде всего нужно для маппинга кастомных типов
            DefaultNameTranslator = new NpgsqlSnakeCaseNameTranslator()
        };
        
        // Здесь тоже нужно версионирование моделей, чтобы не сломать обратную совместимость
        dataSourceBuilder.MapComposite<ItemV1>("item_v1");

        //dataSourceBuilder.UseJsonNet(new[] { typeof(MyClrType) });

        _dataSource = dataSourceBuilder.Build();

        _transactionTimeout = transactionTimeout;
    }
    
    public async Task<DbConnection> GetConnection(CancellationToken cancellationToken)
    {
        var connection = await _dataSource.OpenConnectionAsync(cancellationToken);
        // Нужно только если connetctionString будет одинаковой  для бизнес-логики и миграций
        // См метод GetConnectionString в миграторе
        //await connection.ReloadTypesAsync();
        return connection;
    }

    public TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(_transactionTimeout)
            },
            TransactionScopeAsyncFlowOption.Enabled
        );
    }
}