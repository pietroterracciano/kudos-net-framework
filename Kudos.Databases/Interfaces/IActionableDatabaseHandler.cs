using Kudos.Databases.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Interfaces
{
    public interface IActionableDatabaseHandler
    {
        public DatabaseResult OpenConnection();
        public Task<DatabaseResult> OpenConnectionAsync();
        public DatabaseResult CloseConnection();
        public Task<DatabaseResult> CloseConnectionAsync();
        public Boolean IsConnectionOpened();
        public Boolean IsConnectionBroken();
        public Boolean IsConnectionClosed();
        public DatabaseResult ChangeSchema(String? s);
        public Boolean IsIntoTransaction();
        public DatabaseResult BeginTransaction();
        public DatabaseResult CommitTransaction();
        public DatabaseResult RollbackTransaction();
        public DatabaseNonQueryResult ExecuteNonQuery(String? s, params KeyValuePair<String, Object>[]? aParameters);
        public Task<DatabaseNonQueryResult> ExecuteNonQueryAsync(String? s, params KeyValuePair<String, Object>[]? aParameters);
        public DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object>[]? aParameters);
        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object>[]? aParameters);
        public DatabaseQueryResult ExecuteQuery(String? s, Int32 iExpectedRowsNumber, params KeyValuePair<String, Object>[]? aParameters);
        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32 iExpectedRowsNumber, params KeyValuePair<String, Object>[]? aParameters);
    }
}
