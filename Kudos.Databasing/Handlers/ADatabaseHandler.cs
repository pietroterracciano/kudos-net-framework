using Kudos.Constants;
using Kudos.Databasing.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Enums;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.Interfaces.Chains;
using Kudos.Databasing.Results;
using Kudos.Databasing.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Texts;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace Kudos.Databasing.Controllers
{
    public abstract class 
        ADatabaseHandler
        <
            DbConnectionStringBuilderType,
            DbConnectionType,
            DbCommandType
        > 
    :
        SemaphorizedObject,
        IDatabaseHandler
        where DbConnectionStringBuilderType : DbConnectionStringBuilder
        where DbConnectionType : DbConnection, new()
        where DbCommandType : DbCommand
    {
        private readonly DbConnectionType
            _oConnection;

        private DbCommandType?
            _oCommand;

        private IDatabaseHandler
            _this;

        private readonly EDatabaseConnectionBehaviour?
            _eConnectionBehaviour;

        public EDatabaseType Type { get; private set; }

        internal ADatabaseHandler(EDatabaseType e, ref DbConnectionStringBuilderType csb, ref EDatabaseConnectionBehaviour? edcb)
        {
            _this = this;
            Type = e;
            _eConnectionBehaviour = edcb;
            _oConnection = new DbConnectionType() { ConnectionString = csb.ToString() };
        }

        #region Connection

        #region public ... ...DatabaseResult... OpenConnection...()

        public DatabaseResult OpenConnection()
        {
            Task<DatabaseResult> tdr = OpenConnectionAsync();
            tdr.Wait();
            return tdr.Result;
        }
        public async Task<DatabaseResult> OpenConnectionAsync()
        {
            await _WaitSemaphoreAsync();
            DatabaseResult dr = await _OpenConnectionAsync();
            _ReleaseSemaphore();
            return dr;
        }
        private async Task<DatabaseResult> _OpenConnectionAsync()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            DatabaseErrorResult? dber;

            dbbr.StopOnWaiting().StartOnConnecting();

            if (!IsConnectionOpened())
            {
                try
                {
                    await _oConnection.OpenAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

                if (dber == null)
                {
                    try
                    {
                        _oCommand = _oConnection.CreateCommand() as DbCommandType;
                    }
                    catch (Exception e)
                    {
                        dber = new DatabaseErrorResult(ref e);
                    }

                    if (
                        dber == null
                        && _oCommand == null
                    )
                        dber = new DatabaseErrorResult(CDatabaseErrorCode.InternalFailure, "Create Command is failed");
                }
            }
            else
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsAlreadyOpened, "Connection is already opened");

            dbbr.StopOnConnecting();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        private async Task _AutoOpenConnectionAsync()
        {
            if
            (
                IsConnectionOpened()
                || _eConnectionBehaviour == null
                || !_eConnectionBehaviour.Value.HasFlag(EDatabaseConnectionBehaviour.AutomaticOpening)
            )
                return;

            await _OpenConnectionAsync();
        }

        #endregion

        #region public ... ...DatabaseResult... CloseConnection...()

        public DatabaseResult CloseConnection()
        {
            Task<DatabaseResult> tdr = CloseConnectionAsync();
            tdr.Wait();
            return tdr.Result;
        }
        public async Task<DatabaseResult> CloseConnectionAsync()
        {
            await _WaitSemaphoreAsync();
            DatabaseResult dr = await _CloseConnectionAsync();
            _ReleaseSemaphore();

            return dr;
        }
        private async Task<DatabaseResult> _CloseConnectionAsync()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            DatabaseErrorResult? dber;

            dbbr.StopOnWaiting().StartOnExecuting();

            if (!IsConnectionClosed())
                try
                {
                    await _oConnection.CloseAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }
            else
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsAlreadyClosed, "Connection is already closed");

            dbbr.StopOnExecuting();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        private async Task _AutoCloseConnectionAsync()
        {
            if
            (
                IsConnectionClosed()
                || _eConnectionBehaviour == null
                || !_eConnectionBehaviour.Value.HasFlag(EDatabaseConnectionBehaviour.AutomaticClosing)
            )
                return;

            await _CloseConnectionAsync();
        }

        #endregion

        #region Boolean IsConnection...()

        public Boolean IsConnectionOpening()
        {
            return
                _oConnection.State == ConnectionState.Connecting
                && _oCommand != null;
        }

        public Boolean IsConnectionOpened()
        {
            return
                (
                    _oConnection.State == ConnectionState.Open
                    || _oConnection.State == ConnectionState.Fetching
                    || _oConnection.State == ConnectionState.Executing
                )
                && _oCommand != null;
        }

        public Boolean IsConnectionBroken()
        {
            return
                _oConnection.State == ConnectionState.Broken 
                || _oCommand == null;
        }

        public Boolean IsConnectionClosed()
        {
            return
                _oConnection.State == ConnectionState.Closed;
        }

        #endregion

        #endregion

        #region Transaction

        public Boolean IsIntoTransaction()
        {
            return
                _oCommand != null
                && _oCommand.Transaction != null;
        }

        public DatabaseResult BeginTransaction()
        {
            Task<DatabaseResult> tdr = BeginTransactionAsync();
            tdr.Wait();
            return tdr.Result;
        }
        public async Task<DatabaseResult> BeginTransactionAsync()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            DatabaseErrorResult? dber;

            dbbr.StopOnWaiting().StartOnExecuting();

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction != null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.TransactionIsAlreadyBegun, "Transaction is alredy begun");
            else
                try
                {
                    dber = (_oCommand.Transaction = await _oConnection.BeginTransactionAsync()) != null
                        ? null
                        : new DatabaseErrorResult(CDatabaseErrorCode.ImpossibleToBeginTransaction, "Impossible to begin Transaction");
                }
                catch(Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            dbbr.StopOnExecuting();

            _ReleaseSemaphore();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public DatabaseResult CommitTransaction()
        {
            Task<DatabaseResult> tdr = CommitTransactionAsync();
            tdr.Wait();
            return tdr.Result;
        }
        public async Task<DatabaseResult> CommitTransactionAsync()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            DatabaseErrorResult? dber;

            dbbr.StopOnWaiting().StartOnExecuting();

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction == null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.NotInTransaction, "NotInTransaction");
            else
                try
                {
                    await _oCommand.Transaction.CommitAsync();
                    await _Transaction__DisposeAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            dbbr.StopOnExecuting();

            _ReleaseSemaphore();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public DatabaseResult RollbackTransaction()
        {
            Task<DatabaseResult> tdr = RollbackTransactionAsync();
            tdr.Wait();
            return tdr.Result;
        }
        public async Task<DatabaseResult> RollbackTransactionAsync()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            DatabaseErrorResult? dber;

            dbbr.StopOnWaiting().StartOnExecuting();

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction == null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.NotInTransaction, "NotInTransaction");
            else
                try
                {
                    await _oCommand.Transaction.RollbackAsync();
                    await _Transaction__DisposeAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            dbbr.StopOnExecuting();

            _ReleaseSemaphore();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        private async Task _Transaction__DisposeAsync()
        {
            try { await _oCommand.Transaction.DisposeAsync(); } catch { }
            _oCommand.Transaction = null;
        }

        #endregion

        #region Command

        private Boolean Command__Prepare(
            ref String? s,
            ref KeyValuePair<String, Object>[]? a,
            out DatabaseErrorResult? err
        )
        {
            if (!IsConnectionOpened())
            {
                err = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
                return false;
            }
            else if (String.IsNullOrWhiteSpace(s))
            {
                err = new DatabaseErrorResult(CDatabaseErrorCode.ParameterIsInvalid, "Parameter is invalid");
                return false;
            }

            _oCommand.CommandText = s;
            _oCommand.Parameters.Clear();

            if (a != null)
                foreach (KeyValuePair<String, Object> kvp in a)
                {
                    if (String.IsNullOrWhiteSpace(kvp.Key))
                        continue;

                    DbParameter oDbParameter = _oCommand.CreateParameter();
                    oDbParameter.ParameterName = kvp.Key.Trim().Replace("@", "");
                    oDbParameter.Value = kvp.Value;

                    //if (kvp.Value != null)
                    //{
                    //    Type t = kvp.Value.GetType();

                    //    if (t == CType.UInt16 || t == CType.NullableUInt16)
                    //        oDbParameter.DbType = DbType.UInt16;
                    //}

                    _oCommand.Parameters.Add(oDbParameter);
                }

            err = null;
            return true;
        }

        //private void Command__DisposeAsync(ref DbCommandType oCommand)
        //{
        //    try { oCommand.DisposeAsync(); } catch { }
        //}

        #endregion

        #region public ... ...DatabaseResult... ChangeSchema...(...)

        public DatabaseResult ChangeSchema(String? s)
        {
            Task<DatabaseResult> tdr = ChangeSchemaAsync(s);
            tdr.Wait();
            return tdr.Result;
        }

        public async Task<DatabaseResult> ChangeSchemaAsync(String? s)
        {
            DatabaseErrorResult? dber;

            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            dbbr.StopOnWaiting().StartOnExecuting();

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (String.IsNullOrWhiteSpace(s))
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ParameterIsInvalid, "Parameter is invalid");
            else
                try
                {
                    await _oConnection.ChangeDatabaseAsync(s);
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            dbbr.StopOnWaiting().StopOnExecuting();

            _ReleaseSemaphore();

            return new DatabaseResult(ref dber, ref dbbr);
        }

        #endregion

        #region public ... DatabaseNonQueryResult ... ExecuteNonQuery...(...)

        public DatabaseNonQueryResult ExecuteNonQuery(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            Task<DatabaseNonQueryResult> t = ExecuteNonQueryAsync(s, a);
            t.Wait();
            return t.Result;
        }
        public async Task<DatabaseNonQueryResult> ExecuteNonQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            dbbr.StopOnWaiting().StartOnConnecting();

            await _AutoOpenConnectionAsync();

            dbbr.StopOnConnecting().StartOnPreparing();

            DatabaseErrorResult? err;
            Boolean b = Command__Prepare(ref s, ref a, out err);

            dbbr.StopOnPreparing();

            Int64 lLastInsertedID;
            Int32 iUpdatedRows;

            if (!b)
                lLastInsertedID = iUpdatedRows = 0;
            else
            {
                dbbr.StartOnExecuting();

                try
                {
                    iUpdatedRows = await _oCommand.ExecuteNonQueryAsync();
                    lLastInsertedID = ExecuteNonQuery_GetLastInsertedID(_oCommand);
                }
                catch (Exception e)
                {
                    lLastInsertedID = iUpdatedRows = 0;
                    err = OnException(ref e);
                    if (err == null) err = new DatabaseErrorResult(ref e);
                }

                dbbr.StopOnExecuting();
            }

            await _AutoCloseConnectionAsync();

            _ReleaseSemaphore();

            return new DatabaseNonQueryResult(ref lLastInsertedID, ref iUpdatedRows, ref err, ref dbbr);
        }

        protected abstract DatabaseErrorResult? OnException(ref Exception e);

        protected abstract Int64 ExecuteNonQuery_GetLastInsertedID(DbCommandType cmd);

        #endregion

        #region public ... ...DatabaseQueryResult... ExecuteQuery...(...);
        
        public DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object?>[]? a) { return ExecuteQuery(s, null, a); }
        public DatabaseQueryResult ExecuteQuery(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
        {
            Task<DatabaseQueryResult> t = ExecuteQueryAsync(s, iExpectedRowsNumber, a);
            t.Wait();
            return t.Result;
        }
        public async Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            return await ExecuteQueryAsync(s, null, a);
        }
        public async Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            await _WaitSemaphoreAsync();

            dbbr.StopOnWaiting().StartOnConnecting();

            await _AutoOpenConnectionAsync();

            dbbr.StopOnConnecting().StartOnPreparing();

            DatabaseErrorResult? err;
            Boolean b = Command__Prepare(ref s, ref a, out err);

            dbbr.StopOnPreparing();

            DataTable? dt;

            if (!b)
                dt = null;
            else
            {
                dbbr.StartOnExecuting();

                try
                {
                    DbDataReader oDataReader = await _oCommand.ExecuteReaderAsync();

                    if (oDataReader.HasRows)
                    {
                        dt = new DataTable();

                        if (iExpectedRowsNumber != null && iExpectedRowsNumber > 0)
                            dt.MinimumCapacity = iExpectedRowsNumber.Value;

                        dt.Load(oDataReader);
                    }
                    else
                        dt = null;

                    await oDataReader.DisposeAsync();
                }
                catch (Exception e)
                {
                    dt = null;
                    err = OnException(ref e);
                    if (err == null) err = new DatabaseErrorResult(ref e);
                }

                dbbr.StopOnExecuting();
            }

            await _AutoCloseConnectionAsync();

            _ReleaseSemaphore();

            return new DatabaseQueryResult(ref dt, ref err, ref dbbr);
        }

        #endregion

        #region public ... ...DatabaseTableDescriptor?... GetTableDescriptor...(...)

        public DatabaseTableDescriptor? GetTableDescriptor(String? sName)
        {
            DatabaseTableDescriptor? dtd;
            DatabaseTableDescriptor.Get(ref _this, ref sName, out dtd);
            Task<DatabaseTableDescriptor?> t = _FinalizeTableDescriptorAsync(dtd);
            t.Wait();
            return t.Result;
        }
        public DatabaseTableDescriptor? GetTableDescriptor(String? sSchemaName, String? sName)
        {
            DatabaseTableDescriptor? dtd;
            DatabaseTableDescriptor.Get(ref _this, ref sSchemaName, ref sName, out dtd);
            Task<DatabaseTableDescriptor?> t = _FinalizeTableDescriptorAsync(dtd);
            t.Wait();
            return t.Result;
        }

        public async Task<DatabaseTableDescriptor?> GetTableDescriptorAsync(String? sName)
        {
            return await _FinalizeTableDescriptorAsync(await DatabaseTableDescriptor.GetAsync(_this, sName));
        }
        public async Task<DatabaseTableDescriptor?> GetTableDescriptorAsync(String? sSchemaName, String? sName)
        {
            return await _FinalizeTableDescriptorAsync(await DatabaseTableDescriptor.GetAsync(_this, sSchemaName, sName));
        }

        private async Task<DatabaseTableDescriptor?> _FinalizeTableDescriptorAsync(DatabaseTableDescriptor? dtd)
        {
            if (dtd != null) dtd.Eject(await DatabaseColumnDescriptor.GetAllAsync(_this, dtd));
            return dtd;
        }

        #endregion

        //#region public ... ...DatabaseTableDescriptor?... GetColumnDescriptor...(...)

        //public DatabaseColumnDescriptor? GetColumnDescriptor(String? sTableName, String? sName)
        //{
        //    DatabaseColumnDescriptor? dcd;
        //    DatabaseColumnDescriptor.Get(ref _this, ref sTableName, ref sName, out dcd);
        //    return dcd;
        //}
        //public DatabaseColumnDescriptor? GetColumnDescriptor(String? sSchemaName, String? sTableName, String? sName)
        //{
        //    DatabaseColumnDescriptor? dcd;
        //    DatabaseColumnDescriptor.Get(ref _this, ref sSchemaName, ref sTableName, ref sName, out dcd);
        //    return dcd;
        //}
        //public DatabaseColumnDescriptor? GetColumnDescriptor(DatabaseTableDescriptor? dscTable, String? sName)
        //{
        //    DatabaseColumnDescriptor? dcd;
        //    DatabaseColumnDescriptor.Get(ref _this, ref dscTable, ref sName, out dcd);
        //    return dcd;
        //}
        //public async Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(String? sTableName, String? sName)
        //{
        //    return await DatabaseColumnDescriptor.GetAsync(_this, sTableName, sName);
        //}
        //public async Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(String? sSchemaName, String? sTableName, String? sName)
        //{
        //    return await DatabaseColumnDescriptor.GetAsync(_this, sSchemaName, sTableName, sName);
        //}
        //public async Task<DatabaseColumnDescriptor?> GetColumnDescriptorAsync(DatabaseTableDescriptor? dscTable, String? sName)
        //{
        //    return await DatabaseColumnDescriptor.GetAsync(_this, dscTable, sName);
        //}

        //#endregion

        //#region public ... ...DatabaseTableDescriptor[]?... GetColumnsDescriptors...(...)

        //public DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sTableName)
        //{
        //    DatabaseColumnDescriptor[]? dcda;
        //    DatabaseColumnDescriptor.GetAll(ref _this, ref sTableName, out dcda);
        //    return dcda;
        //}
        //public DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sSchemaName, String? sTableName)
        //{
        //    DatabaseColumnDescriptor[]? dcda;
        //    DatabaseColumnDescriptor.GetAll(ref _this, ref sSchemaName, ref sTableName, out dcda);
        //    return dcda;
        //}
        //public DatabaseColumnDescriptor[]? GetColumnsDescriptors(DatabaseTableDescriptor? dscTable)
        //{
        //    DatabaseColumnDescriptor[]? dcda;
        //    DatabaseColumnDescriptor.GetAll(ref _this, ref dscTable, out dcda);
        //    return dcda;
        //}
        //public async Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(String? sTableName)
        //{
        //    return await DatabaseColumnDescriptor.GetAllAsync(_this, sTableName);
        //}
        //public async Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(String? sSchemaName, String? sTableName)
        //{
        //    return await DatabaseColumnDescriptor.GetAllAsync(_this, sSchemaName, sTableName);
        //}
        //public async Task<DatabaseColumnDescriptor[]?> GetColumnsDescriptorsAsync(DatabaseTableDescriptor? dscTable)
        //{
        //    return await DatabaseColumnDescriptor.GetAllAsync(_this, dscTable);
        //}

        //#endregion

        #region Dispose()

        public void Dispose()
        {
            DisposeAsync().Wait();
        }

        public async Task DisposeAsync()
        {
            if (_oCommand != null) try { await _oCommand.DisposeAsync(); } catch { }
            if (_oConnection != null) try { await _oConnection.DisposeAsync(); } catch { }
        }

        #endregion
    }
}