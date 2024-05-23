using Kudos.Constants;
using Kudos.Databases.Constants;
using Kudos.Databases.Descriptors;
using Kudos.Databases.Enums;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Databases.Results;
using Kudos.Databases.Utils;
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

namespace Kudos.Databases.Controllers
{
    public abstract class 
        ADatabaseHandler
        <
            DbConnectionStringBuilderType,
            DbConnectionType,
            DbCommandType
        > 
    : 
        IDatabaseHandler
        where DbConnectionStringBuilderType : DbConnectionStringBuilder
        where DbConnectionType : DbConnection, new()
        where DbCommandType : DbCommand
    {
        private readonly DbConnectionType
            _oConnection;

        private DbCommandType?
            _oCommand;

        private readonly Queue<IDatabaseHandler> 
            _queInStandby, _queInUse;

        //private readonly IBuildableDatabaseChain
        //    _oBuildableChain;

        //private Boolean
        //    _bIsExecuting;

        private readonly Object
            //_oSuperficialLock,
            _oDeeperLock;

        private IDatabaseHandler
            _this;

        public EDatabaseType Type { get; private set; }

        internal ADatabaseHandler(EDatabaseType e, ref DbConnectionStringBuilderType csb)
        {
            _this = this;
            //_oSuperficialLock = new Object();
            _oDeeperLock = new Object();
            Type = e;
            _oConnection = new DbConnectionType() { ConnectionString = csb.ToString() };
        }

        #region Connection

        #region public DatabaseResult OpenConnection()

        public DatabaseResult OpenConnection()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionOpened())
            {
                try
                {
                    _oConnection.Open();
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
                    catch(Exception e)
                    {
                        dber = new DatabaseErrorResult(ref e);
                    }

                    if(
                        dber == null 
                        && _oCommand == null
                    )
                        dber = new DatabaseErrorResult(CDatabaseErrorCode.InternalFailure, "Create Command is failed");
                }
            }
            else
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsAlreadyOpened, "Connection is already opened");

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public Task<DatabaseResult> OpenConnectionAsync()
        {
            return Task.Run(
                delegate ()
                {
                    return OpenConnection();
                }
            );
        }

        #endregion

        #region public DatabaseResult CloseConnection()

        public DatabaseResult CloseConnection()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionClosed())
                try
                {
                    _oConnection.Close();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }
            else
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsAlreadyClosed, "Connection is already closed");

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public Task<DatabaseResult> CloseConnectionAsync()
        {
            return Task.Run(
                delegate ()
                {
                    return CloseConnection();
                }
            );
        }

        #endregion

        #region Status

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

        #region public Boolean ChangeSchema()

        public DatabaseResult ChangeSchema(String? s)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if(String.IsNullOrWhiteSpace(s))
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ParameterIsInvalid, "Parameter is invalid");
            else
                try 
                {
                    _oConnection.ChangeDatabase(s);
                    dber = null;
                } 
                catch(Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public Task<DatabaseResult> ChangeSchemaAsync(String sSchemaName)
        {
            return Task.Run(
                delegate ()
                {
                    return ChangeSchema(sSchemaName);
                }
            );
        }

        #endregion

        #region Transaction

        public Boolean IsIntoTransaction()
        {
            return _oCommand?.Transaction != null;
        }

        public DatabaseResult BeginTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction != null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.TransactionIsAlreadyBegun, "Transaction is alredy begun");
            else
                try
                {
                    dber = (_oCommand.Transaction = _oConnection.BeginTransaction()) != null
                        ? null
                        : new DatabaseErrorResult(CDatabaseErrorCode.ImpossibleToBeginTransaction, "Impossible to begin Transaction");
                }
                catch(Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public DatabaseResult CommitTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction == null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.NotInTransaction, "NotInTransaction");
            else
                try
                {
                    _oCommand.Transaction.Commit();
                    Transaction__DisposeAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            return new DatabaseResult(ref dber, ref dbbr);
        }

        public DatabaseResult RollbackTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecution();
            DatabaseErrorResult? dber;

            if (!IsConnectionOpened())
                dber = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
            else if (_oCommand.Transaction == null)
                dber = new DatabaseErrorResult(CDatabaseErrorCode.NotInTransaction, "NotInTransaction");
            else
                try
                {
                    _oCommand.Transaction.Rollback();
                    Transaction__DisposeAsync();
                    dber = null;
                }
                catch (Exception e)
                {
                    dber = new DatabaseErrorResult(ref e);
                }

            return new DatabaseResult(ref dber, ref dbbr);
        }

        private void Transaction__DisposeAsync()
        {
            try { _oCommand.Transaction.DisposeAsync(); } catch { }
            _oCommand.Transaction = null;
        }

        #endregion

        #region Command

        //private Boolean Command__Prepare(
        //    ref String s, 
        //    ref KeyValuePair<String, Object>[] a,
        //    out DbCommandType? cmd,
        //    out DatabaseErrorResult? err
        //)
        //{
        //    if (!IsConnectionOpened())
        //    {
        //        cmd = null;
        //        err = new DatabaseErrorResult(CDatabaseErrorCode.ConnectionIsClosed, "Connection is closed");
        //        return false;
        //    }
        //    else if (String.IsNullOrWhiteSpace(s))
        //    {
        //        cmd = null;
        //        err = new DatabaseErrorResult(CDatabaseErrorCode.ParameterIsInvalid, "Parameter is invalid");
        //        return false;
        //    }

        //    try
        //    {
        //        cmd = _oConnection.CreateCommand() as DbCommandType;
        //    }
        //    catch
        //    {
        //        cmd = null;
        //    }

        //    if (cmd == null)
        //    {
        //        cmd = null;
        //        err = new DatabaseErrorResult(CDatabaseErrorCode.InternalFailure, "Create Command is failed");
        //        return false;
        //    }

        //    //oCommand.CommandTimeout = (Int32)Config.CommandTimeout;
        //    cmd.CommandText = s;

        //    if (a != null)
        //        foreach (KeyValuePair<String, Object> kvp in a)
        //        {
        //            if (String.IsNullOrWhiteSpace(kvp.Key))
        //                continue;

        //            DbParameter oDbParameter = cmd.CreateParameter();
        //            oDbParameter.ParameterName = kvp.Key.Trim().Replace("@", "");
        //            oDbParameter.Value = kvp.Value;
        //            cmd.Parameters.Add(oDbParameter);
        //        }

        //    //if (!Config.IsAutoCommitEnabled)
        //    //{
        //    //    RollbackTransaction();
        //    //    if (!BeginTransaction())
        //    //    {
        //    //        Command__DisposeAsync(ref oCommand);
        //    //        mError = new DBErrorModel(0, "Impossible to Begin Transaction");
        //    //        return false;
        //    //    }
        //    //}

        //    err = null;
        //    return true;
        //}

        private Boolean Command__Prepare(
            ref String s,
            ref KeyValuePair<String, Object>[] a,
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

        #region public DBNonQueryCommandResultModel ExecuteNonQuery

        public Task<DatabaseNonQueryResult> ExecuteNonQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            return
                Task.Run
                (
                    () =>
                    {
                        return ExecuteNonQuery(s, a);
                    }
                );
        }
        public DatabaseNonQueryResult ExecuteNonQuery(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            DataTable? dt;
            DatabaseErrorResult? err;
            Int64 lLastInsertedID;
            Int32 iUpdatedRows;

            //lock(_oSuperficialLock)
            //{
            //    if (_bIsExecuting)
            //    {
            //        IDatabaseHandler dbh = _oBuildableChain.BuildHandler();

            //        if (dbh.OpenConnection().HasError())
            //            goto EXECUTE;

            //        DatabaseNonQueryResult dbnqr = dbh.ExecuteNonQuery(s, a);
            //        dbh.CloseConnectionAsync();
            //        return dbnqr;
            //    }

            //    _bIsExecuting = true;
            //}
            
            //EXECUTE:

            lock (_oDeeperLock)
            {
                dbbr.StopOnWaiting().StartOnExecution();

                if (!Command__Prepare(ref s, ref a, out err))
                {
                    lLastInsertedID = iUpdatedRows = 0;
                    goto END_METHOD;
                }

                try
                {
                    iUpdatedRows = _oCommand.ExecuteNonQuery();
                    lLastInsertedID = ExecuteNonQuery_GetLastInsertedID(_oCommand);
                }
                catch (Exception e)
                {
                    lLastInsertedID = iUpdatedRows = 0;
                    err = OnException(ref e);
                    if (err == null) err = new DatabaseErrorResult(ref e);
                }
            }

            END_METHOD:

            //lock (_oSuperficialLock)
            //{
            //    _bIsExecuting = false;
            //}

            return new DatabaseNonQueryResult(ref lLastInsertedID, ref iUpdatedRows, ref err, ref dbbr);
        }

        protected abstract DatabaseErrorResult? OnException(ref Exception e);

        protected abstract Int64 ExecuteNonQuery_GetLastInsertedID(DbCommandType cmd);

        #endregion

        #region public DBQueryCommandResultModel ExecuteQuery()

        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            return
                Task.Run
                (
                    () =>
                    {
                        return ExecuteQuery(s, 0, a);
                    }
                );
        }
        public DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object?>[]? a) { return ExecuteQuery(s, 0, a); }
        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32 iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
        {
            return
                Task.Run
                (
                    () =>
                    {
                        return ExecuteQuery(s, iExpectedRowsNumber, a);
                    }
                );
        }
        public DatabaseQueryResult ExecuteQuery(String? s, Int32 iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            DataTable? oDataTable;
            DatabaseErrorResult? err;

            //lock (_oSuperficialLock)
            //{
            //    if (_bIsExecuting)
            //    {
            //        IDatabaseHandler dbh = _oBuildableChain.BuildHandler();

            //        if (dbh.OpenConnection().HasError())
            //            goto EXECUTE;

            //        DatabaseQueryResult dbqr = dbh.ExecuteQuery(s, iExpectedRowsNumber, a);
            //        dbh.CloseConnectionAsync();
            //        return dbqr;
            //    }

            //    _bIsExecuting = true;
            //}

            //EXECUTE:

            lock (_oDeeperLock)
            {
                dbbr.StopOnWaiting().StartOnExecution();

                if (!Command__Prepare(ref s, ref a, out err))
                {
                    oDataTable = null;
                    goto END_METHOD;
                }

                try
                {
                    DbDataReader oDataReader = _oCommand.ExecuteReader();

                    if (oDataReader.HasRows)
                    {
                        oDataTable = new DataTable();

                        if (iExpectedRowsNumber > 0)
                            oDataTable.MinimumCapacity = iExpectedRowsNumber;

                        oDataTable.Load(oDataReader);
                    }
                    else
                        oDataTable = null;
               
                    oDataReader.DisposeAsync();
                }
                catch (Exception e)
                {
                    oDataTable = null;
                    err = OnException(ref e);
                    if(err == null) err = new DatabaseErrorResult(ref e);
                }
            }

            END_METHOD:

            //lock (_oSuperficialLock)
            //{
            //    _bIsExecuting = false;
            //}

            return new DatabaseQueryResult(ref oDataTable, ref err, ref dbbr);
        }

        #endregion

        #region public DatabaseTableDescriptor? GetTableDescriptor(...)

        public DatabaseTableDescriptor? GetTableDescriptor(String? sName) { return GetTableDescriptor(null, sName); }
        public DatabaseTableDescriptor? GetTableDescriptor(String? sSchemaName, String? sName)
        {
            DatabaseTableDescriptor dbtd;
            DatabaseTableDescriptor.Get(ref _this, ref sSchemaName, ref sName, out dbtd);
            return dbtd;
        }

        #endregion

        #region public DatabaseColumnDescriptor? GetColumnDescriptor(...)

        public DatabaseColumnDescriptor? GetColumnDescriptor(String? sTableName, String? sName) { return GetColumnDescriptor(null, sTableName, sName); }
        public DatabaseColumnDescriptor? GetColumnDescriptor(String? sSchemaName, String? sTableName, String? sName)
        {
            return GetColumnDescriptor(GetTableDescriptor(sSchemaName, sTableName), sName);
        }
        public DatabaseColumnDescriptor? GetColumnDescriptor(DatabaseTableDescriptor? dscTable, String? sName)
        {
            DatabaseColumnDescriptor? dbcd;
            DatabaseColumnDescriptor.Get(ref _this, ref dscTable, ref sName, out dbcd);
            return dbcd;
        }

        #endregion

        #region public DatabaseColumnDescriptor[]? GetColumnsDescriptors(...)

        public DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sTableName) { return GetColumnsDescriptors(null, sTableName); }
        public DatabaseColumnDescriptor[]? GetColumnsDescriptors(String? sSchemaName, String? sTableName)
        {
            return GetColumnsDescriptors(GetTableDescriptor(sSchemaName, sTableName));
        }
        public DatabaseColumnDescriptor[]? GetColumnsDescriptors(DatabaseTableDescriptor? dscTable)
        {
            DatabaseColumnDescriptor[]? dbcda;
            DatabaseColumnDescriptor.Get(ref _this, ref dscTable, out dbcda);
            return dbcda;
        }

        #endregion
    }
}