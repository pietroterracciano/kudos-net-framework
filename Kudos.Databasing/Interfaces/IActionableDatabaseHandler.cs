using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.Interfaces
{
    public interface IActionableDatabaseHandler : IDisposable 
    {
        DatabaseResult OpenConnection();
        Task<DatabaseResult> OpenConnectionAsync();
        DatabaseResult CloseConnection();
        Task<DatabaseResult> CloseConnectionAsync();
        Boolean IsConnectionOpened();
        Boolean IsConnectionOpening();
        Boolean IsConnectionBroken();
        Boolean IsConnectionClosed();
        Task<DatabaseResult> ChangeSchemaAsync(String? s);
        DatabaseResult ChangeSchema(String? s);
        Boolean IsIntoTransaction();
        Task<DatabaseResult> BeginTransactionAsync();
        DatabaseResult BeginTransaction();
        Task<DatabaseResult> CommitTransactionAsync();
        DatabaseResult CommitTransaction();
        Task<DatabaseResult> RollbackTransactionAsync();
        DatabaseResult RollbackTransaction();
        DatabaseNonQueryResult ExecuteNonQuery(String? s, params KeyValuePair<String, Object?>[]? aParameters);
        Task<DatabaseNonQueryResult> ExecuteNonQueryAsync(String? s, params KeyValuePair<String, Object>[]? aParameters);
        DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object?>[]? aParameters);
        Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object?>[]? aParameters);
        DatabaseQueryResult ExecuteQuery(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? aParameters);
        Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? aParameters);

        DatabaseTableDescriptor? GetTableDescriptor(String? sName);
        DatabaseTableDescriptor? GetTableDescriptor(String? sSchemaName, String? sName);

        DatabaseColumnDescriptor? GetColumnDescriptor(String? sTableName, String? sName);
        DatabaseColumnDescriptor? GetColumnDescriptor(String? sSchemaName, String? sTableName, String? sName);
        DatabaseColumnDescriptor? GetColumnDescriptor(DatabaseTableDescriptor? dbtd, String? sName);

        DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sTableName);
        DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sSchemaName, String? sTableName);
        DatabaseColumnDescriptor[]? GetColumnsDescriptors(DatabaseTableDescriptor? dbtd);

        public Task<ValueTask> DisposeAsync();
    }
}
