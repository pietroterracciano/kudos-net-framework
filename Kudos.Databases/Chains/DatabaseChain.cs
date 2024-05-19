using Kudos.Constants;
using Kudos.Databases.Interfaces.Chains;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Chains
{
    public class DatabaseChain : IDatabaseChain
    {
        internal string? _UserName, _UserPassword, _SchemaName;
        internal uint? _CommandTimeout, _ConnectionTimeout, _SessionPoolTimeout;
        internal ushort? _MinimumPoolSize, _MaximumPoolSize;
        internal bool? _IsAutoCommitEnabled, _IsCompressionEnabled, _IsLoggingEnabled, _IsPoolingEnabled;

        public IDatabaseChain IsAutoCommitEnabled(bool? b) { _IsAutoCommitEnabled = b; return this; }
        public IDatabaseChain IsCompressionEnabled(bool? b) { _IsCompressionEnabled = b; return this; }
        public IDatabaseChain IsLoggingEnabled(bool? b) { _IsLoggingEnabled = b; return this; }
        public IDatabaseChain IsPoolingEnabled(bool? b) { _IsPoolingEnabled = b; return this; }
        public IDatabaseChain SetCommandTimeout(uint? i) { _CommandTimeout = i; return this; }
        public IDatabaseChain SetConnectionTimeout(uint? i) { _ConnectionTimeout = i; return this; }
        public IDatabaseChain SetMaximumPoolSize(ushort? i) { _MaximumPoolSize = i; return this; }
        public IDatabaseChain SetMinimumPoolSize(ushort? i) { _MinimumPoolSize = i; return this; }
        public IDatabaseChain SetSchemaName(string? s) { _SchemaName = s; return this; }
        public IDatabaseChain SetSessionPoolTimeout(string? s) { _SchemaName = s; return this; }
        public IDatabaseChain SetSessionPoolTimeout(uint? i) { _SessionPoolTimeout = i; return this; }
        public IDatabaseChain SetUserName(string? s) { _UserName = s; return this; }
        public IDatabaseChain SetUserPassword(string? s) { _UserPassword = s; return this; }

        public IMySQLDatabaseChain ConvertToMySQLChain() { return new MySQLDatabaseChain(this); }
        public IMSSQLDatabaseChain ConvertToMSSQLChain() { return new MSSQLDatabaseChain(this); }

        internal DatabaseChain(DatabaseChain? o = null)
        {
            ObjectUtils.Copy(o, this, CBindingFlags.Instance);
        }
    }
}
