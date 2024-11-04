using Kudos.Databasing.Chainers;
using Kudos.Databasing.Enums;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
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

#region Apro la connessione al Database

await dh.OpenConnectionAsync();

#endregion

Stopwatch
    sw;

#region Istanzio uno Stopwatch

sw = new Stopwatch();

#endregion

IGefyraContext
    gc;

#region Istanzio un GefyraContext<ClientModel>

gc = Gefyra.RequestContext(dh);

#endregion

ClientModel[]? cea;

#region Effettuo una Select+ReturnMany ramite Delegate

sw.Restart();

cea =
    await
        gc
            .Select<ClientModel>()
            .InnerJoin<OpusesModel>
            (
                (cm, om) =>
                    cm.ID == om.ClientID
            )
            .CopyIn(Gefyra.GetJoin<ClientModel>(nameof(ClientModel.Opuses))
            .Where
            (
                cm =>
                    cm.Mail == "sdaasddsa@ab.it"
            )
            .ExecuteAndReturnManyAsync();

sw.Stop();

Console.WriteLine("Select+ReturnMany    " + sw.ElapsedMilliseconds);

#endregion

//#region Effettuo una Select+Join+ReturnMany ramite Delegate

//sw.Restart();

//cea =
//    await
//        gc
//            .Select<ClientModel>
//            (
//                cm =>
//                    new String[] { "Pietro", "Florin" }.Contains(cm.FirstName)
//            )
//            .Join<OpusesModel>
//            (
//                (cm, om) =>
//                    cm.ID == om.ClientID
//            )
//            .ExecuteAndReturnManyAsync();

//sw.Stop();

//Console.WriteLine("Select+Order+ReturnMany    " + sw.ElapsedMilliseconds);

//#endregion

//#region Effettuo una Select+Limit+ReturnMany tramite Delegate

//sw.Restart();

//cea =
//    await
//        gc
//            .Select<ClientModel>
//            (
//                c =>
//                    c.FirstName == "Pietro"
//                    || c.FirstName == "Florin"
//            )
//            .Limit(3)
//            .ExecuteAndReturnManyAsync();

//sw.Stop();

//Console.WriteLine("Select+Limit+ReturnMany    " + sw.ElapsedMilliseconds);

//#endregion

//ClientModel? cm;

//#region Effettuo una Select+ReturnFirst tramite Delegate

//sw.Restart();

//cm =
//    await
//        gc
//            .Select<ClientModel>
//            (
//                c =>
//                    c.FirstName == "Pietro"
//                    || c.FirstName == "Florin"
//            )
//            .ExecuteAndReturnFirstAsync();

//sw.Stop();

//Console.WriteLine("Select+ReturnFirst   " + sw.ElapsedMilliseconds);

//#endregion

//#region Effettuo una Select+ReturnLast tramite Delegate

//sw.Restart();

//cm =
//    await
//        gc
//            .Select<ClientModel>
//            (
//                c =>
//                    c.FirstName == "Pietro"
//                    || c.FirstName == "Florin"
//            )
//            .ExecuteAndReturnLastAsync();

//sw.Stop();

//Console.WriteLine("Select+ReturnLast   " + sw.ElapsedMilliseconds);

//#endregion

Boolean b = true;




//#region Effettuo un Update tramite Delegate

//oStopwatch.Restart();

//await
//    gc
//        .Update
//        (
//            (c) =>
//            {
//                c.RoleID = 10;
//                c.FirstName = "Pietro";
//                c.LastName = "Terracciano";
//            }
//        )
//        .ExecuteAsync();

//oStopwatch.Stop();

//l = oStopwatch.ElapsedMilliseconds;

//#endregion

//#region Effettuo un Insert diretto

//oStopwatch.Restart();

//await
//    gc
//        .Insert
//        (
//            new CustomerModel()
//            {
//                FirstName = "Florin",
//                LastName = "Aparaschivei",
//                RoleID = 10
//            }
//        )
//        .ExecuteAsync();

//oStopwatch.Stop();

//l = oStopwatch.ElapsedMilliseconds;

//#endregion

//#region Effettuo un Insert tramite Delegate

//oStopwatch.Restart();

//await
//    gc
//        .Insert
//        (
//            (c) =>
//            {
//                c.RoleID = 10;
//                c.FirstName = "Pietro";
//                c.LastName = "Terracciano";
//            }
//        )
//        .ExecuteAsync();

//oStopwatch.Stop();

//l = oStopwatch.ElapsedMilliseconds;

//#endregion










//for (int i = 0; i < 10; i++)
//{
//    gc
//        .Insert
//        (
//            (c) =>
//            {
//                c.RoleID = 10;
//                c.FirstName = "Pietro";
//                c.LastName = "Terracciano";
//            }
//        )
//        .ExecuteAsync()
//        .Wait();
//}

//oStopwatch.Stop();

//long l = oStopwatch.ElapsedMilliseconds;