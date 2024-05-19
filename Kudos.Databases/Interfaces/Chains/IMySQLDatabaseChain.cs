using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Interfaces.Chains
{
    public interface IMySQLDatabaseChain : IBuildableDatabaseChain
    {
        IMySQLDatabaseChain SetHost(String? s);
        IMySQLDatabaseChain SetPort(UInt16? i);
        IMySQLDatabaseChain SetKeepAlive(UInt32? i);
        IMySQLDatabaseChain SetConnectionProtocol(MySqlConnectionProtocol? e);
        IMySQLDatabaseChain IsSessionPoolInteractive(Boolean? b);
    }
}