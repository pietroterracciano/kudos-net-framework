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

        DatabaseNonQueryResult ExecuteNonQuery(String? s, params KeyValuePair<String, Object?>[]? a);
        Task<DatabaseNonQueryResult> ExecuteNonQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a);

        DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object?>[]? a);
        Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a);
        DatabaseQueryResult ExecuteQuery(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a);
        Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a);

        DatabaseTableDescriptor? GetTableDescriptor(String? sTableName);
        DatabaseTableDescriptor? GetTableDescriptor(String? sSchemaName, String? sTableName);
        Task<DatabaseTableDescriptor?> GetTableDescriptorAsync(String? sTableName);
        Task<DatabaseTableDescriptor?> GetTableDescriptorAsync(String? sSchemaName, String? sTableName);

        //DatabaseColumnDescriptor? GetColumnDescriptor(String? sTableName, String? sName);
        //DatabaseColumnDescriptor? GetColumnDescriptor(String? sSchemaName, String? sTableName, String? sName);
        //DatabaseColumnDescriptor? GetColumnDescriptor(DatabaseTableDescriptor? dbtd, String? sName);
        //Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(String? sTableName, String? sName);
        //Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(String? sSchemaName, String? sTableName, String? sName);
        //Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(DatabaseTableDescriptor? dtd, String? sName);

        //DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sTableName);
        //DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sSchemaName, String? sTableName);
        //DatabaseColumnDescriptor[]? GetColumnsDescriptors(DatabaseTableDescriptor? dbtd);
        //Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(String? sTableName);
        //Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(String? sSchemaName, String? sTableName);
        //Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(DatabaseTableDescriptor? dtd);

        public Task DisposeAsync();
    }
}
