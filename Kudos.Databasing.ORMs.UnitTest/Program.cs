using Kudos.Databasing.Chainers;
using Kudos.Databasing.Enums;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts;
using Kudos.Databasing.ORMs.UnitTest.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;


IDatabaseHandler
    dh;

#region Istanzio un DatabaseHandler

dh =
    DatabaseChainer
        .NewChain()
        .SetCommandTimeout(30)
        .SetUserName("arteideuser")
        .SetUserPassword("7YgTpwniTe4XdHH3£.")
        .SetSchemaName("arteidedb")
        .SetSessionPoolTimeout(30)
        .ConvertToMySQLChain()
            .IsConnectionResetEnabled(true)
            .IsSessionPoolInteractive(false)
            .SetCharacterSet(EDatabaseCharacterSet.utf8mb4)
            .SetHost("3.124.31.6")
            .SetPort(2024)
            .SetConnectionProtocol(MySqlConnectionProtocol.Socket)
            .BuildHandler();

#endregion

Stopwatch
    oStopwatch;

#region Istanzio uno Stopwatch

oStopwatch = new Stopwatch();

#endregion

Gefyra.RequestContext<CustomerModel>(null);