using System.Data.Common;
using System.Transactions;

namespace ToyWebService.Application.DAL.Utils;

public abstract class BasicRepository
{
    private readonly IDataBase _dataBase;

    protected BasicRepository(IDataBase dataBase)
    {
        _dataBase = dataBase;
    }

    public Task<DbConnection> GetConnection(CancellationToken cancellationToken)
    {
        return _dataBase.GetConnection(cancellationToken);
    }

    public TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        return _dataBase.CreateTransactionScope(isolationLevel);
    }
}