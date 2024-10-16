﻿using Kudos.Constants;
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
        IDatabaseHandler
        where DbConnectionStringBuilderType : DbConnectionStringBuilder
        where DbConnectionType : DbConnection, new()
        where DbCommandType : DbCommand
    {
        private readonly DbConnectionType
            _oConnection;

        private DbCommandType?
            _oCommand;

        private readonly Object
            _lck;

        private IDatabaseHandler
            _this;

        private readonly EDatabaseConnectionBehaviour?
            _eConnectionBehaviour;

        public EDatabaseType Type { get; private set; }

        internal ADatabaseHandler(EDatabaseType e, ref DbConnectionStringBuilderType csb, ref EDatabaseConnectionBehaviour? edcb)
        {
            _this = this;
            Type = e;
            _lck = new object();
            _eConnectionBehaviour = edcb;
            _oConnection = new DbConnectionType() { ConnectionString = csb.ToString() };
        }

        #region Connection

        #region public DatabaseResult OpenConnection()

        public DatabaseResult OpenConnection()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnConnecting();
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

        private void _AutoOpening()
        {
            if
            (
                IsConnectionOpened()
                || _eConnectionBehaviour == null
                || !_eConnectionBehaviour.Value.HasFlag(EDatabaseConnectionBehaviour.AutomaticOpening)
            )
                return;

            OpenConnection();
        }

        #endregion

        #region public DatabaseResult CloseConnection()

        public DatabaseResult CloseConnection()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnConnecting();
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

        private void _AutoClosing()
        {
            if
            (
                IsConnectionClosed()
                || _eConnectionBehaviour == null
                || !_eConnectionBehaviour.Value.HasFlag(EDatabaseConnectionBehaviour.AutomaticClosing)
            )
                return;

            CloseConnection();
        }

        #endregion

        #region Status

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

        #region public Boolean ChangeSchema()

        public Task<DatabaseResult> ChangeSchemaAsync(String? s) { return Task.Run(() => ChangeSchema(s)); }
        public DatabaseResult ChangeSchema(String? s)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecuting();
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

        #endregion

        #region Transaction

        public Boolean IsIntoTransaction()
        {
            return _oCommand?.Transaction != null;
        }

        public Task<DatabaseResult> BeginTransactionAsync() { return Task.Run(BeginTransaction); }
        public DatabaseResult BeginTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecuting();
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

        public Task<DatabaseResult> CommitTransactionAsync() { return Task.Run(CommitTransaction); }
        public DatabaseResult CommitTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecuting();
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

        public Task<DatabaseResult> RollbackTransactionAsync() { return Task.Run(RollbackTransaction); }
        public DatabaseResult RollbackTransaction()
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnExecuting();
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

            lock (_lck)
            {
                dbbr.StopOnWaiting().StartOnConnecting();

                _AutoOpening();

                dbbr.StopOnConnecting().StartOnExecuting();

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

                dbbr.StopOnExecuting();

                _AutoClosing();
            }

            END_METHOD:

            return new DatabaseNonQueryResult(ref lLastInsertedID, ref iUpdatedRows, ref err, ref dbbr);
        }

        protected abstract DatabaseErrorResult? OnException(ref Exception e);

        protected abstract Int64 ExecuteNonQuery_GetLastInsertedID(DbCommandType cmd);

        #endregion

        #region public ... ...DatabaseQueryResult... ExecuteQuery...()

        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, params KeyValuePair<String, Object?>[]? a)
        {
            return
                Task.Run
                (
                    () =>
                    {
                        return ExecuteQuery(s, a);
                    }
                );
        }
        public DatabaseQueryResult ExecuteQuery(String? s, params KeyValuePair<String, Object?>[]? a) { return ExecuteQuery(s, null, a); }
        public Task<DatabaseQueryResult> ExecuteQueryAsync(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
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
        public DatabaseQueryResult ExecuteQuery(String? s, Int32? iExpectedRowsNumber, params KeyValuePair<String, Object?>[]? a)
        {
            DatabaseBenchmarkResult dbbr = new DatabaseBenchmarkResult().StartOnWaiting();

            DataTable? oDataTable;
            DatabaseErrorResult? err;

            lock (_lck)
            {
                dbbr.StopOnWaiting().StartOnConnecting();

                _AutoOpening();

                dbbr.StopOnConnecting().StartOnExecuting();

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

                        if (iExpectedRowsNumber != null && iExpectedRowsNumber > 0)
                            oDataTable.MinimumCapacity = iExpectedRowsNumber.Value;

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
                    if (err == null) err = new DatabaseErrorResult(ref e);
                }

                dbbr.StopOnExecuting();

                _AutoClosing();
            }

            END_METHOD:

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

        #region Dispose()

        public void Dispose()
        {
            if(_oCommand != null) try {_oCommand.Dispose(); } catch { }
            if (_oConnection != null) try { _oConnection.Dispose(); } catch { }
        }

        public async Task<ValueTask> DisposeAsync()
        {
            if (_oCommand != null) try { await _oCommand.DisposeAsync(); } catch { }
            if (_oConnection != null) try { await _oConnection.DisposeAsync(); } catch { }
            return ValueTask.CompletedTask;
        }

        #endregion
    }
}