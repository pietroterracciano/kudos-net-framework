using Kudos.Databases.Chains;
using Kudos.Databases.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Interfaces.Chains
{
    public interface IDatabaseChain
    {
        IDatabaseChain SetSchemaName(String? s);
        IDatabaseChain SetUserName(String? s);
        IDatabaseChain SetUserPassword(String? s);
        IDatabaseChain SetSessionPoolTimeout(UInt32? i);
        IDatabaseChain IsCompressionEnabled(Boolean? b);
        IDatabaseChain IsPoolingEnabled(Boolean? b);
        IDatabaseChain SetConnectionBehaviour(EDatabaseConnectionBehaviour? edcb);
        IDatabaseChain IsAutomaticCommitEnabled(Boolean? b);
        IDatabaseChain SetCommandTimeout(UInt32? i);
        IDatabaseChain SetConnectionTimeout(UInt32? i);
        IDatabaseChain IsLoggingEnabled(Boolean? b);
        IDatabaseChain SetMinimumPoolSize(UInt16? i);
        IDatabaseChain SetMaximumPoolSize(UInt16? i);

        IMySQLDatabaseChain ConvertToMySQLChain();
        IMSSQLDatabaseChain ConvertToMSSQLChain();
    }
}