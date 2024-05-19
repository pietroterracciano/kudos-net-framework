using Kudos.Databases.Controllers;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Interfaces.Chains;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace Kudos.Databases.Chains
{
    public class MySQLDatabaseChain : ABuildableDatabaseChain, IMySQLDatabaseChain
    {
        internal string? _Host;
        internal bool? _IsSessionPoolInteractive;
        internal ushort? _Port;
        internal uint? _KeepAlive;
        internal MySqlConnectionProtocol? _ConnectionProtocol;

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

            if (_IsPoolingEnabled != null) mscsb.Pooling = _IsPoolingEnabled.Value;
            if (_MinimumPoolSize != null) mscsb.MinimumPoolSize = _MinimumPoolSize.Value;
            if (_MaximumPoolSize != null) mscsb.MaximumPoolSize = _MaximumPoolSize.Value;

            if (_ConnectionTimeout != null) mscsb.ConnectionTimeout = _ConnectionTimeout.Value;
            if (_SessionPoolTimeout != null) mscsb.ConnectionLifeTime = _SessionPoolTimeout.Value;
            if (_IsSessionPoolInteractive != null) mscsb.InteractiveSession = _IsSessionPoolInteractive.Value;
            if (_CommandTimeout != null) mscsb.DefaultCommandTimeout = _CommandTimeout.Value;
            if (_IsCompressionEnabled != null) mscsb.UseCompression = _IsCompressionEnabled.Value;

            if (_ConnectionProtocol != null) mscsb.ConnectionProtocol = _ConnectionProtocol.Value;

            return new MySQLDatabaseHandler(ref mscsb);
        }

        internal MySQLDatabaseChain(DatabaseChain? o) : base(o) { }
    }
}