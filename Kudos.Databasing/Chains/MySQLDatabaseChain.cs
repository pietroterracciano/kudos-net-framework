using Kudos.Databasing.Constants;
using Kudos.Databasing.Controllers;
using Kudos.Databasing.Enums;
using Kudos.Databasing.Handlers;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.Interfaces.Chains;
using MySql.Data.MySqlClient;
using System;

namespace Kudos.Databasing.Chains
{
    public class MySQLDatabaseChain : ABuildableDatabaseChain, IMySQLDatabaseChain
    {
        internal string? _Host;
        internal EDatabaseCharacterSet? _CharacterSet;
        internal bool? _IsSessionPoolInteractive, _IsConnectionResetEnabled;
        internal ushort? _Port;
        internal uint? _KeepAlive;
        internal MySqlConnectionProtocol? _ConnectionProtocol;


        public IMySQLDatabaseChain IsConnectionResetEnabled(bool? b) { _IsConnectionResetEnabled = b; return this; }
        public IMySQLDatabaseChain SetCharacterSet(EDatabaseCharacterSet? e) { _CharacterSet = e; return this; }
        public IMySQLDatabaseChain SetHost(string? s) { _Host = s; return this; }
        public IMySQLDatabaseChain SetPort(ushort? i) { _Port = i; return this; }
        public IMySQLDatabaseChain SetKeepAlive(uint? i) { _KeepAlive = i; return this; }
        public IMySQLDatabaseChain SetConnectionProtocol(MySqlConnectionProtocol? e) { _ConnectionProtocol = e; return this; }
        public IMySQLDatabaseChain IsSessionPoolInteractive(bool? b) { _IsSessionPoolInteractive = b; return this; }

        public override IDatabaseHandler BuildHandler()
        {
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            if (_IsLoggingEnabled != null) mscsb.Logging = _IsLoggingEnabled.Value;
            if (_KeepAlive != null) mscsb.Keepalive = _KeepAlive.Value;

            if (_Host != null) mscsb.Server = _Host;
            if (_Port != null) mscsb.Port = _Port.Value;

            if (_UserName != null) mscsb.UserID = _UserName;
            if (_UserPassword != null) mscsb.Password = _UserPassword;
            if (_SchemaName != null) mscsb.Database = _SchemaName;

            mscsb.DnsSrv = false;

            mscsb.Pooling = HasValidMinimumPoolSize || HasValidMaximumPoolSize;
            if (HasValidMinimumPoolSize) mscsb.MinimumPoolSize = _MinimumPoolSize.Value;
            if (HasValidMaximumPoolSize) mscsb.MaximumPoolSize = _MaximumPoolSize.Value;

            if (_CharacterSet != null)
                switch (_CharacterSet)
                {
                    case EDatabaseCharacterSet.utf8:
                        mscsb.CharacterSet = CDatabaseCharacterSet.utf8;
                        break;
                    case EDatabaseCharacterSet.utf8mb4:
                        mscsb.CharacterSet = CDatabaseCharacterSet.utf8mb4;
                        break;
                }

            if (_ConnectionTimeout != null) mscsb.ConnectionTimeout = _ConnectionTimeout.Value;
            if (_SessionPoolTimeout != null) mscsb.ConnectionLifeTime = _SessionPoolTimeout.Value;
            if (_IsSessionPoolInteractive != null) mscsb.InteractiveSession = _IsSessionPoolInteractive.Value;
            if (_CommandTimeout != null) mscsb.DefaultCommandTimeout = _CommandTimeout.Value;
            if (_IsCompressionEnabled != null) mscsb.UseCompression = _IsCompressionEnabled.Value;
            if (_IsConnectionResetEnabled != null) mscsb.ConnectionReset = _IsConnectionResetEnabled.Value;

            if (_ConnectionProtocol != null) mscsb.ConnectionProtocol = _ConnectionProtocol.Value;

            return new MySQLDatabaseHandler(ref mscsb, ref _ConnectionBehaviour);
        }

        internal MySQLDatabaseChain(DatabaseChain? o) : base(o) { }
    }
}