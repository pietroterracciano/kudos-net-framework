using Kudos.DataBases.Models;

using Kudos.DataBases.Models.Configs;
using Kudos.DataBases.Models.Results;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Kudos.DataBases.Controllers
{
    public abstract class ADBController<DBConfigModelType, DbCommandType, DbConnectionType, DbConnectionStringBuilderType> 
        where DBConfigModelType : ADBConfigModel, new()
        where DbCommandType : DbCommand
        where DbConnectionType : DbConnection, new()
        where DbConnectionStringBuilderType : DbConnectionStringBuilder
    {
        private DbConnectionType
            _oConnection;

        private DbCommandType
            _oCommand;

        private Boolean
            _bIsExecutingCommand;

        public DBConfigModelType Config
        {
            get;
            private set;
        }

        public DBErrorModel LastError
        {
            get;
            private set;
        }

        public ADBController()
        {
            Config = new DBConfigModelType();
        }

        protected abstract DbConnectionStringBuilderType OnConnectionStringBuilderCreation();

        #region Connection

        #region public Boolean OpenConnection()

        public Boolean OpenConnection()
        {
            ClearLastError();

            if (IsConnectionOpened())
            {
                SetLastError(0, "Connection is already opened");
                return false;
            }

            #region Create and verify ConnectionStringBuilder

            DbConnectionStringBuilder
                oConnectionStringBuilder = OnConnectionStringBuilderCreation();

            String
                sConnectionString = oConnectionStringBuilder != null ? oConnectionStringBuilder.ToString() : null;

            if (sConnectionString == null)
            {
                SetLastError(0, "Impossible to create ConnectionString");
                return false;
            }

            #endregion

            #region Create, verify Connection

            _oConnection = new DbConnectionType()
            {
                ConnectionString = sConnectionString
            };

            if (_oConnection == null)
            {
                SetLastError(0, "Impossible to create ConnectionHandler");
                return false;
            }

            #endregion

            try
            {
                _oConnection.Open();
            }
            catch (Exception oException)
            {
                SetLastError(0, oException.Message);
            }

            if (!IsConnectionStatusWhole())
            {
                SetLastError(0, "Impossible to establish a Connection");
                return false;
            }

            _oCommand = _oConnection.CreateCommand() as DbCommandType;

            return true;
        }

        public Task<Boolean> OpenConnectionAsync()
        {
            return Task.Run(
                delegate ()
                {
                    return OpenConnection();
                }
            );
        }

        #endregion

        #region public Boolean CloseConnection()

        public Boolean CloseConnection()
        {
            ClearLastError();

            if (IsConnectionClosed())
            {
                SetLastError(0, "Connection is already closed");
                return false;
            }

            try { _oConnection.Close(); } catch { }

            if (!IsConnectionClosed())
            {
                SetLastError(0, "Impossible to close Connection");
                return false;
            }

            RollbackTransaction();

            if (_oCommand != null)
            {
                if (_bIsExecutingCommand)
                    try
                    {
                        _oCommand.Cancel();
                    }
                    catch
                    {
                    }

                try
                {
                    _oCommand.Dispose();
                }
                catch
                {

                }
            }

            _oCommand = null;
            _oConnection = null;

            return true;
        }

        public Task<Boolean> CloseConnectionAsync()
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

        protected Boolean IsConnectionStatusWhole()
        {
            return
                _oConnection != null
                && (
                    _oConnection.State == ConnectionState.Open
                    || _oConnection.State == ConnectionState.Fetching
                    || _oConnection.State == ConnectionState.Executing
                );
        }


        public Boolean IsConnectionOpened()
        {
            return
                IsConnectionStatusWhole()
                && _oCommand != null;
        }

        public Boolean IsConnectionBroken()
        {
            return
                _oConnection != null
                && (
                    _oConnection.State == ConnectionState.Broken
                    || _oCommand == null
                );
        }

        public Boolean IsConnectionClosed()
        {
            return
                _oConnection == null
                || _oConnection.State == ConnectionState.Closed;
        }

        #endregion

        #endregion

        protected void ClearLastError()
        {
            LastError = null;
        }

        protected bool SetLastError(Int32 iErrorID, String sErrorMessage)
        {
            if (LastError != null)
                return false;

            LastError = new DBErrorModel(iErrorID, sErrorMessage);
            return true;
        }

        #region public Boolean ChangeSchema()

        public Boolean ChangeSchema(String sSchemaName)
        {
            if (IsConnectionOpened() && !String.IsNullOrWhiteSpace(sSchemaName))
                try 
                {
                    _oConnection.ChangeDatabase(sSchemaName);
                    Config.SchemaName = sSchemaName;
                    return true;
                } 
                catch
                {
                }

            return false;
        }

        public Task<Boolean> ChangeSchemaAsync(String sSchemaName)
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
            return
                IsConnectionOpened()
                && _oCommand.Transaction != null;
        }

        public Boolean BeginTransaction()
        {
            if (IsConnectionOpened() && !IsIntoTransaction())
                try
                {
                    return (_oCommand.Transaction = _oConnection.BeginTransaction()) != null;
                }
                catch
                {
                    SetLastError(0, "Impossible to begin Transaction");
                }

            return false;
        }

        public Boolean CommitTransaction()
        {
            if (IsConnectionOpened() && IsIntoTransaction())
                try
                {
                    _oCommand.Transaction.Commit();
                    _oCommand.Transaction.Dispose();
                    _oCommand.Transaction = null;
                    return true;
                }
                catch
                {
                    SetLastError(0, "Impossible to commit Transaction");
                }

            return false;
        }

        public Boolean RollbackTransaction()
        {
            if (IsConnectionOpened() && IsIntoTransaction())
                try
                {
                    _oCommand.Transaction.Rollback();
                    _oCommand.Transaction.Dispose();
                    _oCommand.Transaction = null;
                }
                catch
                {
                    SetLastError(0, "Impossible to rollback Transaction");
                }

            return false;
        }

        #endregion

        #region Command

        private Boolean PrepareCommand(ref String sCommand, ref Dictionary<String, Object> dCommandParameters)
        {
            ClearLastError();

            if (!IsConnectionOpened())
            {
                SetLastError(0, "Connection is closed");
                return false;
            }

            if (String.IsNullOrWhiteSpace(sCommand))
            {
                SetLastError(0, "Command is empty");
                return false;
            }

            if ( _oCommand == null )
            {
                SetLastError(0, "Error in Command instance");
                return false;
            }

            _oCommand.Parameters.Clear();
            _oCommand.CommandTimeout = (Int32)Config.CommandTimeout;
            _oCommand.CommandText = sCommand;

            if (dCommandParameters != null)
                foreach (KeyValuePair<String, Object> kvpCParameter in dCommandParameters)
                {
                    if (String.IsNullOrWhiteSpace(kvpCParameter.Key))
                        continue;

                    DbParameter oParameter = _oCommand.CreateParameter();
                    oParameter.ParameterName = kvpCParameter.Key.Trim().Replace("@", "");
                    oParameter.Value = kvpCParameter.Value;

                    _oCommand.Parameters.Add(oParameter);
                }

            if (!Config.IsAutoCommitEnabled)
            {
                RollbackTransaction();
                if (!BeginTransaction())
                    return false;
            }

            return true;
        }

        #region public DBNonQueryCommandResultModel ExecuteNonQueryCommand

        /// <summary>Nullable</summary>
        public DBNonQueryCommandResultModel ExecuteNonQueryCommand(String sCommand)
        {
            return ExecuteNonQueryCommand(sCommand, null);
        }

        /// <summary>Nullable</summary>
        public DBNonQueryCommandResultModel ExecuteNonQueryCommand(String sCommand, Dictionary<String, Object> dCommandParameters)
        {
            if (!PrepareCommand(ref sCommand, ref dCommandParameters))
                return null;

            try
            {
                Stopwatch
                    oStopwatch = new Stopwatch();

                _bIsExecutingCommand = true;
                oStopwatch.Start();

                Int32
                    iUpdatedRows = _oCommand.ExecuteNonQuery();

                oStopwatch.Stop();
                _bIsExecutingCommand = false;

                Int64
                    iLastInsertedID = ExecuteNonQueryCommand_GetLastInsertedID(_oCommand);

                return new DBNonQueryCommandResultModel(ref iLastInsertedID, ref iUpdatedRows, ref oStopwatch);
            }
            catch (ExternalException oExternalException)
            {
                SetLastError(oExternalException.ErrorCode, oExternalException.Message);
            }
            catch (Exception oException)
            {
                SetLastError(0, oException.Message);
            }

            return null;
        }

        protected abstract Int64 ExecuteNonQueryCommand_GetLastInsertedID(DbCommandType oDbCommand);

        #endregion

        #region public DBQueryCommandResultModel ExecuteQueryCommand()

        public DBQueryCommandResultModel ExecuteQueryCommand(String sCommand)
        {
            return ExecuteQueryCommand(sCommand, null, 0);
        }

        public DBQueryCommandResultModel ExecuteQueryCommand(String sCommand, Dictionary<String, Object> dCommandParameters)
        {
            return ExecuteQueryCommand(sCommand, dCommandParameters, 0);
        }

        public DBQueryCommandResultModel ExecuteQueryCommand(String sCommand, Int32 i32ExpectedReadRowsNumber)
        {
            return ExecuteQueryCommand(sCommand, null, i32ExpectedReadRowsNumber);
        }

        public DBQueryCommandResultModel ExecuteQueryCommand(String sCommand, Dictionary<String, Object> dCommandParameters, Int32 i32ExpectedReadRowsNumber)
        {
            if (!PrepareCommand(ref sCommand, ref dCommandParameters))
                return null;

            try
            {
                Stopwatch
                    oStopwatch = new Stopwatch();

                oStopwatch.Start();

                _bIsExecutingCommand = true;

                DbDataReader 
                    oDataReader = _oCommand.ExecuteReader();

                _bIsExecutingCommand = false;

                oStopwatch.Stop();

                if (oDataReader == null)
                {
                    SetLastError(0, "Error in DataReader instance");
                    return null;
                }

                DataTable
                    oDataTable;

                if (oDataReader.HasRows)
                {
                    oDataTable = new DataTable();

                    if (i32ExpectedReadRowsNumber > 0)
                        oDataTable.MinimumCapacity = i32ExpectedReadRowsNumber;

                    oDataTable.Load(oDataReader);
                }
                else
                    oDataTable = null;
               
                oDataReader.DisposeAsync();

                return new DBQueryCommandResultModel(ref oDataTable, ref oStopwatch);
            }
            catch (ExternalException oExternalException)
            {
                SetLastError(oExternalException.ErrorCode, oExternalException.Message);
            }
            catch (Exception oException)
            {
                SetLastError(0, oException.Message);
            }

            return null;
        }

        #endregion

        #region public String PrepareQueryColumnsNames()

        public String PrepareQueryColumnsNames(String sTableName, String sFakeTableName = null)
        {
            return PrepareQueryColumnsNames(Config.SchemaName, sTableName, sFakeTableName);
        }

        public String PrepareQueryColumnsNames(String sSchemaName, String sTableName, String sFakeTableName = null)
        {
            if (
                String.IsNullOrWhiteSpace(sSchemaName)
                || String.IsNullOrWhiteSpace(sTableName)
            )
                return null;

            if (String.IsNullOrWhiteSpace(sFakeTableName))
                sFakeTableName = sTableName;

            String sCommand =
                "SELECT " +
                    "GROUP_CONCAT( CONCAT('" + sFakeTableName + "', '.', COLUMN_NAME, ' AS " + sFakeTableName + "', COLUMN_NAME) SEPARATOR ',') AS PreparedQueryColumnsNames " +
                "FROM " +
                    "information_schema.columns " +
                "WHERE " +
                    "table_schema = \"" + sSchemaName + "\" " +
                "AND " +
                    "table_name = \"" + sTableName + "\" "; 

            DBQueryCommandResultModel
                mResult = ExecuteQueryCommand(sCommand, 1);

            return
                mResult != null
                && mResult.Data != null
                && mResult.Data.Rows.Count > 0
                    ? StringUtils.From(mResult.Data.Rows[0]["PreparedQueryColumnsNames"])
                    : null;
        }

        #endregion

        #endregion
    }
}