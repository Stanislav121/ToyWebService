using System.Data.Common;
using System.Transactions;

namespace ToyWebService.Application.DAL.Utils;

public interface IDataBase
{
    Task<DbConnection> GetConnection(CancellationToken cancellationToken);

    TransactionScope CreateTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}