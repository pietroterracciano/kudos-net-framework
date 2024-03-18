using Kudos.Databases.Controllers;
using Kudos.Databases.Enums;
using Kudos.Databases.Models.Results;
using Kudos.Databases.ORMs.GefyraModule;
using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Contexts;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts;
using Kudos.Databases.ORMs.GefyraModule.Models;
using Kudos.Databases.ORMs.UnitTest.Models;
using Kudos.Mappings.Controllers;
using Kudos.Utils;
using System.Diagnostics;


MySQLDBController
    cntMySQLDB;

#region new DBController()

cntMySQLDB = new MySQLDBController();
cntMySQLDB.Config.Host = "127.0.0.1";// "18.156.48.121";
cntMySQLDB.Config.Port = 2023;// 3306;
cntMySQLDB.Config.ConnectionProtocol = MySql.Data.MySqlClient.MySqlConnectionProtocol.Tcp;
cntMySQLDB.Config.UserName = "root";// "test-albuio-user";
cntMySQLDB.Config.UserPassword = "N0bl5OBRkyuyZ7rS$%";// "Startproject1 -";
cntMySQLDB.Config.SchemaName = "local";// "test-albuio";

#endregion

Stopwatch
    oStopwatch;

#region newStopwatch()

oStopwatch = new Stopwatch();

#endregion

long
    lElapsedSWMilliSeconds;

#region Gefyra.Config

Gefyra.Config.DefaultDBController = cntMySQLDB;

#endregion

//GefyraTable
//    tblCustomer = Gefyra.TableOf<CustomerModel>();

//IGefyraCommandBuilder 
//    oCommandBuilder = Gefyra.NewCommandBuilder();

//GefyraCommandBuilt
//    oBuilt =
//        oCommandBuilder
//        .Select()
//        .From(tblCustomer)
//        .Where(tblCustomer.ColumnOf(nameof(CustomerModel.ID0)), EGefyraComparator.Equal, "1000")
//        .Build();

//cntMySQLDB.OpenConnection();
//cntMySQLDB.ExecuteQueryCommand(oBuilt.Text, oBuilt.GetParameters());


IGefyraContext<CustomerModel>
    ctxCustomer;

#region new GefyraContext()

ctxCustomer = Gefyra.NewContext<CustomerModel>();

#endregion

CustomerModel mCustomer = new CustomerModel();
mCustomer.FirstName = StringUtils.Random(10);
mCustomer.LastName = StringUtils.Random(10);
//mCustomer.Prova0 = new List<UInt32>() {1,2,3,4};
//mCustomer.Prova1 = new List<UInt32>() { 1, 2, 3, 4 };

#region ctxCustomer.Insert(...)


oStopwatch.Restart();
CustomerModel m1 = ctxCustomer.Insert(o => { o.FirstName = "Florin"; o.LastName = "Terracciano"; o.Prova0.AddRange(new UInt32[] { 1, 2, 3, 4, 5 }); }).Execute();
lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

oStopwatch.Restart();
CustomerModel m0 = ctxCustomer.Insert(mCustomer).Execute();
lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

oStopwatch.Restart();
CustomerModel m2 = ctxCustomer.Insert(o => { o.FirstName = mCustomer.FirstName; o.LastName = mCustomer.LastName; }).Execute();
lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

oStopwatch.Restart();

#endregion

#region ctxCustomer.Delete(...)

oStopwatch.Restart();

Boolean
    bIsDeleted0 =
        ctxCustomer
            .Delete(o => o.ID0 == 2)
            .Execute();

Boolean
    bIsDeleted1 =
        ctxCustomer
            .Delete(mCustomer)
            .Execute();


lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

#endregion

#region ctxCustomer.Update(...)

CustomerModel
    m3 =
        ctxCustomer
            .Update(o => o.FirstName = "Pietro")
            .Execute();

#endregion

#region ctxCustomer.Select(...)

oStopwatch.Restart();

//for (int i = 0; i < 1; i++)
//{
//UInt128
//    dsadsasa =
//        ctxCustomer
//            .Count()
//            .Execute();

CustomerModel[]
    prova =
        ctxCustomer
            .Select(o => o.ID0 == 863205)
            .Execute();
//}

lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

var aaa = "ciao";












//oStopwatch.Restart();
//ctxCustomer.Select(o => o.FirstName.Contains("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => !o.FirstName.Contains("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => o.FirstName.Contains("%andrea")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => !o.FirstName.Contains("%andrea")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;


//oStopwatch.Restart();
//ctxCustomer.Select(o => new String[] { "Pietro", "Florin" }.Contains(o.FirstName)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => !(new String[] { "Pietro", "Florin" }.Contains(o.FirstName))).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => new String[] { "%andrea", "anna" }.Contains(o.FirstName)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => new String[] { "%andrea", "%anna" }.Contains(o.FirstName)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => !(new String[] { "%andrea", "anna" }.Contains(o.FirstName))).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//ctxCustomer.Select(o => !(new String[] { "%andrea", "%anna" }.Contains(o.FirstName))).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;


//oStopwatch.Restart();
//CustomerModel[] a0333333 = ctxCustomer.Select(o => o.FirstName.Equals("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a033333333 = ctxCustomer.Select(o => !o.FirstName.Equals("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

oStopwatch.Restart();
CustomerModel[] a03333333333 = ctxCustomer.Select(o => new String[] { "Pietro", "Florin" }.Equals(o.FirstName)).Limit(10).Execute();
lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

oStopwatch.Restart();
CustomerModel[] a033333333333 = ctxCustomer.Select(o => !(new String[] { "Pietro", "Florin" }.Equals(o.FirstName))).Limit(10).Execute();
lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;












//oStopwatch.Restart();
//CustomerModel[] a0333 = ctxCustomer.Select(o => o.FirstName.Contains(new String[] { "Pietro", "Florin" })).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a033 = ctxCustomer.Select(o => o.FirstName.Equals("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a043 = ctxCustomer.Select(o => new String[] { "Pietro", "Andrea" }.Contains(o.FirstName)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;


//oStopwatch.Restart();
//CustomerModel[] a02 = ctxCustomer.Select(o => new UInt32[0].Contains(o.ID0)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//List<String> lFirstNames = new List<string>() { "Pietro", "Andrea" };

//oStopwatch.Restart();
//CustomerModel[] a00 = ctxCustomer.Select(o => lFirstNames.Contains(o.FirstName)).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;


//oStopwatch.Restart();
//CustomerModel[] a01 = ctxCustomer.Select(o => o.FirstName.Equals("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a0 = ctxCustomer.Select().Limit(1).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a1 = ctxCustomer.Select().Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a2 = ctxCustomer.Select().Limit(1).Offset(5).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a3 = ctxCustomer.Select().Limit(10).Offset(5).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a4 = ctxCustomer.Select(o => o.ID0 == 1).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

//oStopwatch.Restart();
//CustomerModel[] a5 = ctxCustomer.Select(o => o.FirstName.Equals("Pietro")).Limit(10).Execute();
//lElapsedSWMilliSeconds = oStopwatch.ElapsedMilliseconds;

#endregion



























//#region Microsoft SQL Command Builder

//GefyraTable
//    tblCustomer = Gefyra.TableOf(typeof(CustomerModel));

//GefyraColumn
//    tblCustomerID0 = tblCustomer.ColumnOf(nameof(CustomerModel.ID0)),
//    tblCustomerID1 = tblCustomer.ColumnOf(nameof(CustomerModel.ID1));

//IGefyraCommandBuilder
//    cbrMSSQL0 = Gefyra.NewCommandBuilder(EDBType.MicrosoftSQL);

//var 
//    cbrMSSQL1 =
//        cbrMSSQL0
//            .Select()
//            .From(tblCustomer)
//            .Where(tblCustomerID0, EGefyraComparator.Equal, 102)
//            .And().Where(tblCustomerID1, EGefyraComparator.Equal, "")
//            .Offset(10)
//            .Limit(10);

//GefyraCommandBuilt
//    oMSSQLCommandBuilt =
//       cbrMSSQL1.Build();


//#endregion














//GefyraTable
//    tblCustomer = Gefyra.TableOf(typeof(CustomerModel));

//GefyraColumn
//    clmCustomerFirstName = tblCustomer.ColumnOf(nameof(CustomerModel.FirstName));



//Stopwatch
//    oStopwatch = new Stopwatch();

//long
//    lStopwatchElapsedMilliseconds;








//IGefyraCommandBuilder
//    oMSSQLCommandBuilder = Gefyra.NewCommandBuilder(EDBType.MicrosoftSQL);

//var ciao =
//            oMSSQLCommandBuilder
//            .Select()
//            .From(tblCustomer)
//            .Offset(10)
//            .Limit(10);


//GefyraCommandBuilt
//    oMSSQLCommandBuilt =
//       ciao.Build();

//IGefyraCommandBuilder
//    oMYSQLCommandBuilder = Gefyra.NewCommandBuilder(EDBType.MySQL);

//oStopwatch.Start();

//for (int i = 0; i < 100000; i++)
//{
//    GefyraCommandBuilt
//        oMYSQLCommandBuilt =
//            oMYSQLCommandBuilder
//                .Select()
//                .From(tblCustomer)
//                .Offset(1000)
//                .Limit(10)
//                .Build();
//}

//oStopwatch.Stop();

//lStopwatchElapsedMilliseconds = oStopwatch.ElapsedMilliseconds;


//MySQLDBController 
//    cntMySQLDB;

//#region new DBController()

//cntMySQLDB = new MySQLDBController();
//cntMySQLDB.Config.Host = "127.0.0.1";// "18.156.48.121";
//cntMySQLDB.Config.Port = 2023;// 3306;
//cntMySQLDB.Config.ConnectionProtocol = MySql.Data.MySqlClient.MySqlConnectionProtocol.Tcp;
//cntMySQLDB.Config.UserName = "root";// "test-albuio-user";
//cntMySQLDB.Config.UserPassword = "N0bl5OBRkyuyZ7rS$%";// "Startproject1 -";
//cntMySQLDB.Config.SchemaName = "local";// "test-albuio";

//#endregion

//#region newStopwatch()

//oStopwatch = new Stopwatch();

//#endregion

//Gefyra.Config.DefaultDBController = cntMySQLDB;

//IGefyraContext<CustomerModel> 
//    ctxCustomer;

//#region new GefyraContext()

//ctxCustomer = Gefyra.NewContext<CustomerModel>();

//#endregion

//CustomerModel
//    mCustomer;

//#region new CustomerModel()

//mCustomer = new CustomerModel();

//#endregion

//#region ctxCustomer.Insert(...)

//mCustomer.ID0 = 100;
//mCustomer.FirstName = StringUtils.Random(10);
//mCustomer.LastName = StringUtils.Random(10);

//CustomerModel m0 = ctxCustomer.Insert(mCustomer).Execute();
//CustomerModel m1 = ctxCustomer.Insert(o => { o.FirstName = "Pietro"; o.LastName = "Terracciano"; }).Execute();
//CustomerModel m2 = ctxCustomer.Insert(o => { o.FirstName = mCustomer.FirstName; o.LastName = mCustomer.LastName; }).Execute();

//oStopwatch.Reset();
//oStopwatch.Start();


//for (int i = 0; i < 1; i++)
//{
//    CustomerModel[]
//        aCustomers = ctxCustomer.Select(o => o.FirstName == "Pietro").Limit(10).Execute();
//}
////mCustomer.FirstName = StringUtils.Random(10);
////mCustomer.LastName = StringUtils.Random(10);
////ctxCustomer.Insert(mCustomer).Execute();

//oStopwatch.Stop();

//lStopwatchElapsedMilliseconds = oStopwatch.ElapsedMilliseconds;

//#endregion


//#region ctxCustomer.Select(...)

//oStopwatch.Reset();
//oStopwatch.Start();

//ctxCustomer.Select(o => "Pietro".Equals(o.FirstName)).Execute();
//ctxCustomer.Select(o => o.FirstName.Equals("Pietro")).Execute();

//oStopwatch.Stop();

//lStopwatchElapsedMilliseconds = oStopwatch.ElapsedMilliseconds;

//#endregion


































//IGefyraCommandBuilder
//    oGefyraCommandBuilder = Gefyra.NewCommandBuilder();

//GefyraTable 
//    tblCustomers = Gefyra.TableOf(typeof(CustomerModel));


//GefyraColumn
//    clmCID1 = tblCustomers.ColumnOf(nameof(CustomerModel.ID1)),
//    clmCFirstName = tblCustomers.ColumnOf(nameof(CustomerModel.FirstName)),
//    clmCLastName = tblCustomers.ColumnOf(nameof(CustomerModel.LastName)),
//    clmCIsDeleted = tblCustomers.ColumnOf(nameof(CustomerModel.IsDeleted));

//GefyraCommandBuilt
//    oGefyraCommandBuilt0 =
//        oGefyraCommandBuilder
//            .Insert()
//            .Into(tblCustomers, clmCID1, clmCFirstName, clmCLastName, clmCIsDeleted)
//            .Values("abc", "pietro", "terracciano", false)
//            .Build();

//cntMySQLDB.ExecuteNonQueryCommand(oGefyraCommandBuilt0.Text, oGefyraCommandBuilt0.GetParameters());



//IGefyraContext<CustomerModel>
//    ctxCustomerAddress = Gefyra.NewContext<CustomerModel>();

//CustomerModel
//    mCustomer = new CustomerModel()
//    {
//        ID0 = 100,
//        FirstName = "Sto cazoodso dsaodwe qofq weofwqofwqo fwqowfqo wfowqffowq ofwq ofwq dwq dqw"
//    };

//Stopwatch oProva = new Stopwatch();
//oProva.Start();


//UInt32[] b = new UInt32[] { 1, 2, 3 };
//List<List<UInt32>> proaava = new List<List<UInt32>>() { new List<UInt32>() { 1, 2, 3 } };
//for (int i = 0; i < 1; i++)
//{
//    ctxCustomerAddress
//        .Select(o => 
//        //o.ID1.Contains("Prova")
//        //||
//        //o.ID1.Equals("Prova")
//        //||
//        //o.ID1.Contains("%cde%")
//        //||
//        proaava[0].Contains(o.ID0)

//        //o.ID0.ToString().Contains(o.ID1)
//        //||
//        //o.ID1.Contains("Ciao")
//        //||
//        //CustomerModel.__proaava.Contains(o.ID0)
//        //||
//        //proaava.Contains(o.ID0) || new UInt32[] { 25, 32 }.Contains(o.ID0) || o.ID0 == new Int32[] { 25, 0 }.First()
//        //.Select(o =>
//        //     o.ID0 == new Int32[] { 25, 30 }.First()
//        //     ||
//        //    o.ID0.ToString() == o.ID1
//        //    ||
//        //    o.ID0 == new Int32[] { 25, 30 }.First() || o.ID1 == (new String[] { new String("Ciao"), new String("Pietro") }.First()) || o.ID0 == 6 || o.IsDeleted == !true)
//        ).Execute();


//}

//    oProva.Stop();

//long ia = oProva.ElapsedMilliseconds;

//Boolean ciao = true;

//ctxCustomerAddress
//    .Select(mCustomer)
//    .Execute();


//CustomerModel
//    mInserted =
//        ctxCustomerAddress
//            .Insert(mCustomer)
//            .Execute();

//Stopwatch oProva = new Stopwatch();
//oProva.Start();

//for (int i=0; i<100000; i++)
//{
//    ctxCustomerAddress
//        .Insert(o => { o.FirstName = "Pietro"; o.ID0 = 10; o.ID1 = "Ciao"; })
//        .Execute();

//}

//oProva.Stop();

//long ia = oProva.ElapsedMilliseconds;

//Boolean ciao = true;


//CustomerModel[] prova =
//    ctxCustomerAddress
//        .Select(o => o.ID1 == "Ciao" && o.ID0 == 10)
//        .Execute();

//CustomerModel prova2 =
//    ctxCustomerAddress
//        .Select(mCustomer)
//        .Execute();

//CustomerModel[] prova =
//    ctxCustomerAddress
//        .Select()
//        .Join<CustomerAddressModel>(EGefyraJoinType.Inner, (a, b) => a.ID0 == b.CustomerID && a.ID1 == "Ciao")
//        .Where(o => o.ID1 == "Ciao")
//        .Execute();

//CustomerModel prova2 =
//    ctxCustomerAddress
//        .Select(mCustomer)
//        .Execute();

//CustomerModel[] prova =
//    ctxCustomerAddress
//        .Select(o => o.ID1 == "Ciao")
//        .Execute();

//CustomerModel prova2 =
//    ctxCustomerAddress
//        .Select(mCustomer)
//        .Execute();





#region Insert(Action<ModelType>)

//ctxCustomerAddress
//    .Insert(o => { o.FirstName = "Pietro"; o.ID0 = 10; o.ID1 = "Ciao";  })
//    .Execute();

#endregion

//#region Insert(ModelType)

//ctxCustomerAddress
//    .Insert(mCustomer)
//    .Execute();

//#endregion



//#region Update(Action<ModelType>)

//ctxCustomerAddress
//    .Update(o => { o.FirstName = "Pietro"; o.ID = 13319; })
//    .Execute();

//#endregion

//#region Update(ModelType)

//ctxCustomerAddress
//    .Update(mCustomer)
//    .Execute();

//#endregion



//#region Delete(Func<ModelType, Boolean>)

//ctxCustomerAddress
//    .Delete(o => o.ID == 10 )
//    .Execute();

//#endregion

//#region Delete(ModelType)

//ctxCustomerAddress
//    .Delete(mCustomer)
//    .Execute();

//#endregion







//Boolean uscita0 =
//    ctxCustomerAddress
//        .Update()
//            .Set(o => { o.FirstName = "Florin"; o.LastName = "Aparaschivei"; })
//            .Where(o => o.ID ==6)
//            .Execute();


//CustomerModel[] uscita1 =

//    ctxCustomerAddress
//        .Select(o => o.ID == 10)
//            .Execute();

//Boolean uscita2 =
//    ctxCustomerAddress
//        .Delete(o =>  o.ID == 5 ).Execute();

//    Boolean
//    aaa = true;


//ctxCustomerAddress
//    .Insert()
//        .Value(o => { o.ID = 10000; o.CustomerID = 25; o.PostalCode = "83100"; })
//        .Execute();

//ctxCustomerAddress
//    .Select()
//        .Join()
//        .Where()
//        .Execute();

//ctxCustomerAddress
//    .Delete()
//        .Where()
//        .Execute();


//ctxCustomerAddress
//    .Update(o => o.ID = 5)
//        .Where()
//        .Execute();\





//ctxCustomerAddress
//    .Save(o => { o.ID = 10000; o.CustomerID = 25; o.PostalCode = "83100"; })
//        .Execute();

//ctxCustomerAddress
//    .Get(o => o.ID == 5)
//        .Include()
//        .Execute();

//ctxCustomerAddress
//    .Delete(o => o.ID == 5)
//        .Execute();


//CustomerAddressModel[]
//    aCAddresses =
//              ctxCustomerAddress
//                .Select(o => o.ID == 5)
//                .ExecuteQuery();

//IGefyraContext<CustomerModel>
//    ctxCustomer = GefyraContext.New<CustomerModel>(cntMySQLDB);

//Stopwatch oProva = new Stopwatch();
//oProva.Start();

//CustomerModel[]
//    aClienti =
//              ctxCustomer
//                .Select()
//                .Join<CustomerAddressModel>(EGefyraJoinType.Inner, (C, CA) => C.ID == CA.CustomerID)
//                .Join<CustomerOrderModel>(EGefyraJoinType.Left, (C, CO) => C.ID == CO.CustomerID)
//                .Where(C => C.ID == 10)
//                .ExecuteQuery();


//oProva.Stop();

//long
//    iElapsed = oProva.ElapsedMilliseconds;


































//cntMySQLDB.Config.Host = "127.0.0.1";
//cntMySQLDB.Config.Port = 2023;
//cntMySQLDB.Config.ConnectionProtocol = MySql.Data.MySqlClient.MySqlConnectionProtocol.Tcp;
//cntMySQLDB.Config.UserName = "root";
//cntMySQLDB.Config.UserPassword = "9H8mWTw29wlrmD$%";
//cntMySQLDB.Config.SchemaName = "local";



//for (int i = 0; i < 100000; i++)
//{
//    //CustomerModel[]
//    //    aCustomers =
//            ctxCustomer
//                .Select()
//                    .Join<CustomerAddressModel>(EGefyraJoinType.Left, (x, y) => x.ID == y.CustomerID)
//                    .Join<CustomerModel>(EGefyraJoinType.Right, (x, y) => x.FirstName == "Pietro" && x.LastName == "Terracciano" && x.IsDeleted == false)
//                    .Where(o => o.ID == 15 || o.ID == 10 || o.ID == 15 || o.ID == 5 || o.ID == 111 || o.FirstName == "Pietro")
//                    .ExecuteQuery();
//}

//oProva.Stop();



//CustomerModel[]
//    aab = ctxCustomer.Delete(null);

//ctxCustomer.GetUsingIDAndJoin(1);


////ctxCustomer.Save(new CustomerModel() { FirstName = "Florin", LastName = "Aparaschivei" });

////for (int i = 0; i < 10000; i++)
////{
////    ctxCustomer.Save(new CustomerModel() { FirstName = "Florin", LastName = "Aparaschivei" });
////}
//    CustomerModel[]
//        mCustomer = ctxCustomer.GetUsingIDAndJoin(1); //ctxCustomer.GetAll();

//    // String sTTableFullName = GefyraMapper.GetTableFullName(typeof(CustomerModel));

//    //// if (sTTableFullName != null)
//    //  //   _sbText.Replace(oType.Name, sTTableFullName);

//    // Dictionary<String, String> dTColumnsNames = GefyraMapper.GetColumnsNames(typeof(CustomerModel));

//    // if (dTColumnsNames != null)
//    // {
//    //     //foreach (KeyValuePair<String, String> kvpTColumnName in dTColumnsNames)
//    //       //  _sbText.Replace(kvpTColumnName.Key, kvpTColumnName.Value);
//    // }

////}

//oProva.Stop();

//long
//    l = oProva.ElapsedMilliseconds;



//String
//    provaa;



//Stopwatch oProva = new Stopwatch();
//oProva.Start();

//for (int i = 0; i < 1000000; i++)
//{
//    provaa = GefyraMapper.GetTableFullName(typeof(CustomerModel));
//}

//oProva.Stop();

//long
//    l = oProva.ElapsedMilliseconds;


//GefyraDataRowMapper<CustomerModel>
//    provab = new GefyraDataRowMapper<CustomerModel>();




//CustomerContext
//    ctxCustomer = new CustomerContext(cntMySQLDB);

//Stopwatch oProva = new Stopwatch();
//oProva.Start();

//CustomerModel[]
//    mCustomer = ctxCustomer.GetAll();

//oProva.Stop();

//long
//    l = oProva.ElapsedMilliseconds;


//Stopwatch oProva = new Stopwatch();
//oProva.Start();

//for (int i = 0; i < 1000000; i++)
//{
//    ctxCustomer = new CustomerContext(cntMySQLDB);
//    CustomerModel
//    mCustomer = ctxCustomer.GetUsingID(10);

//}

//oProva.Stop();

//long
//    l = oProva.ElapsedMilliseconds;












// var prova00 = GefyraCommandBuilder.New();
//            prova00
//                .Delete()
//                    .From(new GefyraDataBaseModel("gsdb", "tblCustomers"))
//                    .Where("colonna1", EGefyraComparator.Equal, 1);

//            var prova1 = GefyraCommandBuilder.New();

//            prova1
//                .Select()
//                    .From(new GefyraDataBaseModel("tblCustomers"))
//                    .Where("colonna1", EGefyraComparator.Equal, 50)
//                        .And().Where("colonna2", EGefyraComparator.Major, 133);

//            GefyraCommandBuilt
//                    ooo =
//                        GefyraCommandBuilder.New()
//                            .Update(new GefyraDataBaseModel("tblCustomers"))
//                                .Set("colonna1", 5)
//                                .Set("colonna2", 10)
//                            .Where("colonna3", EGefyraComparator.Equal, 10)
//                            .Build();

//            var prova3 = GefyraCommandBuilder.New();

//ooo =
//            prova3
//                .Select()
//                    .From(new GefyraDataBaseModel("tblCustomers"))
//                    .Join(EGefyraJoinType.Inner, new GefyraDataBaseModel("tblCustAddresses"))
//                        .On("colonna1", EGefyraComparator.Equal, "colonna2")
//                    .Join(EGefyraJoinType.Left, new GefyraDataBaseModel("tblCustOrders"))
//                        .On("colonna3", EGefyraComparator.Equal, "colonna4")
//                        .Or().On("colonna5", EGefyraComparator.Equal, "colonna6")
//                    .Where("colonna1", EGefyraComparator.Equal, 50)
//                        .And().Where("colonna2", EGefyraComparator.Major, 133).Build();

//            var prova4 = GefyraCommandBuilder.New();

//            prova4
//                .Select()
//                    .From(new GefyraDataBaseModel("tblCustomers"))
//                    .Join(EGefyraJoinType.Inner, new GefyraDataBaseModel("tblCustAddresses"))
//                        .On("colonna1", EGefyraComparator.Equal, "colonna2")
//                    .Join(EGefyraJoinType.Left, new GefyraDataBaseModel("tblCustOrders"))
//                        .On("colonna3", EGefyraComparator.Equal, "colonna4")
//                        .Or().On("colonna5", EGefyraComparator.Equal, "colonna6")
//                    .Where("colonna1", EGefyraComparator.Equal, 50)
//                        .And().Where("colonna2", EGefyraComparator.Major, 133);

//.Select()

//.From(new GefyraDataBaseModel("tblCustomers"))

//.Join(EGefyraJoinType.Left, new GefyraDataBaseModel("tblCustomerAddresses"))

//    .On("iCustAddressCustomerID", EGefyraComparator.Equal, "iCustomerID")
//    .And()
//    .On("iCustAddressSecondID", EGefyraComparator.Equal, "iCustomerSecondID")

//.Join(EGefyraJoinType.Right, new GefyraDataBaseModel("tblCustomerOrders"))
//    .On()
//        .OpenBlock()
//            .On("iCustOrderCustomerID", EGefyraComparator.Equal, "iCustomerID")
//            .Or()
//            .OpenBlock()
//                .On("iCustOrderLeadID", EGefyraComparator.Equal, "iCustomerID")
//                .And()
//                .On("iCustOrderSecondID", EGefyraComparator.In, "iCustomerSecondID")
//            .CloseBlock()
//        .CloseBlock()

//.Where()
//    .OpenBlock()
//        .Where("colonna1", EGefyraComparator.Major, 5)
//        .And().Where("colonna2", EGefyraComparator.Minor, 5)
//    .CloseBlock()
//    .Or()
//    .OpenBlock()
//        .Where("colonna1", EGefyraComparator.Major, 5)
//        .And().Where("colonna2", EGefyraComparator.Minor, 5)
//    .CloseBlock()

//.OrderBy("colonna2", EGefyraOrdering.Desc)
//.OrderBy("colonna2", EGefyraOrdering.Desc)
//.Limit(5)
////.Offset(10);

//String
//                sprova = opoo.ToString();


//Boolean
//                prova = true;

//var prova2 = new GefyraCommandBuilder();

//prova2
//    .Select("colonna1", "colonna2", "colonna3")
//    .From("db", "tabella1")
//    .Where()













//var prrvaaa = new CustomerContext(null);

//CustomerModel miocustomer = prrvaaa.GetUsingID(0);

//var prova = GefyraCommandBuilderA.New();

//prova
//    .Delete().From("tabella1")
//    .Where("colonna1", EGefyraCommandClausoleComparator.Equal, 1);

//prova = GefyraCommandBuilderA.New();

//prova
//    .Insert().Into("", "tabella1", "ciao1","ciao2", "ciao3")
//    .Values("ciao", 1, "ciao", 2);

//prova = GefyraCommandBuilderA.New();

//prova
//    .Update("ciao")
//        .Set("?", 1)
//    .Where("colonna", EGefyraCommandClausoleComparator.Equal, 5);

//prova = GefyraCommandBuilderA.New();

//prova
//    .Select("colonna1", "colonna2", "colonna3")
//    .From("tabella1")

//    .Where("colonna1", EGefyraCommandClausoleComparator.Equal, "?")
//    .
//    .Or()
//    .OpenBlock()
//        .Where("colonna1", EGefyraCommandClausoleComparator.NotEqual, "?")
//        .Or().Where("colonna3", EGefyraCommandClausoleComparator.Minor, "?")
//        .And().Where("colonna4", EGefyraCommandClausoleComparator.MinorOrEqual, "?")
//        .Or().Where("colonna5", EGefyraCommandClausoleComparator.Major, 1)
//        .And().Where("colonna6", EGefyraCommandClausoleComparator.MajorOrEqual, 1)
//    .CloseBlock()
//    .Or()
//    .OpenBlock()
//        .Where("colonna7", EGefyraCommandClausoleComparator.NotEqual, 1)
//        .And().Where("colonna8", EGefyraCommandClausoleComparator.Minor, 1)
//        .And().Where("colonna9", EGefyraCommandClausoleComparator.MinorOrEqual, 1)
//        .And().Where("colonna10", EGefyraCommandClausoleComparator.Major, 1)
//        .And().Where("colonna11", EGefyraCommandClausoleComparator.MajorOrEqual, 1)
//    .CloseBlock();


//bool
//    provaaa = true;

//GefyraCommandBuilderResult
//    sSQL = prova.Build();

//sSQL.ChangeParameterValue(1, "ciaone");

//var ciao =
//sSQL.GetParameters();

//Console.Write(sSQL);