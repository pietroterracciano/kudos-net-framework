// See https://aka.ms/new-console-template for more information
using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Extensions.UnitTest.Models;
using System.Diagnostics;


IGefyraTable
    tblCustomers = Gefyra.GetTable<CustomerModel>();

IGefyraColumn
    dasasdasdds =
tblCustomers.GetColumn(nameof(CustomerModel.ID));

String
    sSQL = dasasdasdds.GetSQL();


IGefyraTable
    tlbdso = Gefyra.GetTable<CustomerModel>().As("B");


dasasdasdds =
tlbdso.GetColumn(nameof(CustomerModel.ID)).As("MY_COLUMN");


sSQL = dasasdasdds.GetSQL();

String s = tlbdso.GetSQL();

tlbdso = Gefyra.GetTable<CustomerModel>().As("C");

s = tlbdso.GetSQL();

tlbdso = Gefyra.GetTable<CustomerModel>().As("D");

s = tlbdso.GetSQL();

tlbdso = tlbdso.As("B");

s = tlbdso.GetSQL();

tlbdso = tlbdso.As("C");

s = tlbdso.GetSQL();

Stopwatch o = new Stopwatch();
o.Restart();

for (int i = 0; i < 100000; i++)
{
    dasasdasdds =
    tlbdso.GetColumn(nameof(CustomerModel.ID));
}

o.Stop();
long l1 = o.ElapsedMilliseconds;

bool prova = false;


//GefyraTable
//    tblCustomers = Gefyra.GetTable<CustomerModel>().As("B");

//o.Restart();

//for (int i = 0; i < 1000000; i++)
//{
//    tblCustomers = Gefyra.GetTable<CustomerModel>();
//}

//o.Stop();
//l1 = o.ElapsedMilliseconds;


//GefyraColumn
//    clmCID = tblCustomers.GetColumn(nameof(CustomerModel.ID)).As("NUOVA_COLONNA"),
//    clmCAddressID = tblCustomers.GetColumn(nameof(CustomerModel.AddressID)),
//    clmCIsDeleted = tblCustomers.GetColumn(nameof(CustomerModel.IsDeleted));


////Stopwatch o = new Stopwatch();
////o.Restart();

////for (int i = 0; i < 1000000; i++)
////{
////    tblCustomers = Gefyra.GetTable<CustomerModel>();
////}

////o.Stop();
////long l1 = o.ElapsedMilliseconds;


//GefyraTable
//    tblAddresses = Gefyra.GetTable<AddressModel>().As("B");

//GefyraColumn
//    clmATuttodiB = tblAddresses.GetColumn("*").ToCountColumn(),
//    clmAID = tblAddresses.GetColumn(nameof(AddressModel.ID)),
//    clmAIsDeleted = tblAddresses.GetColumn(nameof(AddressModel.IsDeleted));

//var c =
//   Gefyra.RequestBuilder()
//        .Select(clmATuttodiB)
//            .From(tblCustomers)
//            .Join(EGefyraJoin.Left, tblAddresses)
//            .On
//            (
//                o =>
//                    o
//                        .OpenBlock()
//                            .Compare(clmCID, EGefyraCompare.Equal, clmCAddressID)
//                            .And().Compare(clmAIsDeleted, EGefyraCompare.Equal, false)
//                        .CloseBlock()
//                        .Or()
//                        .OpenBlock()
//                            .Compare(clmCID, EGefyraCompare.Equal, clmCAddressID)
//                            .And().Compare(clmAIsDeleted, EGefyraCompare.Equal, false)
//                        .CloseBlock()
//            )
//            .Where
//            (
//                o =>
//                    o
//                        .Compare(clmCID, EGefyraCompare.Equal, 1)
//                        .And().Compare(clmCIsDeleted, EGefyraCompare.Equal, false)
//            )
//            .OrderBy(clmAID, EGefyraOrder.Desc)
//            .Limit(10)
//            .Offset(5)
//            .Build();

//var provaaa = 
//c.ToString();

//var dsasd = true;


//IGefyraBuilder
//    prova = Gefyra.RequestBuilder();

//IGefyraSelectClausoleBuilder
//    select = prova.Select(clmCID);

//IGefyraFromClausoleBuilder
//    from = select.From(tblCustomer);


//Stopwatch o = new Stopwatch();
//o.Restart();

//for (int i = 0; i < 1000000; i++)
//{
//    Gefyra.RequestBuilder()
//           .Select(clmCID)
//               .From(tblCustomer)
//               .Join(EGefyraJoin.Left, tblCustomer)
//               .On
//               (
//                   o => o.OpenBlock().Compare(clmCID, EGefyraComparator.Equal, 1).CloseBlock().And().Compare(clmCID, EGefyraComparator.NotLike, 1)
//               )
//               .Join(EGefyraJoin.Left, tblCustomer)
//               .On
//               (
//                   o => o.OpenBlock().Compare(clmCID, EGefyraComparator.Equal, 1).CloseBlock().And().Compare(clmCID, EGefyraComparator.NotLike, 1)
//               )
//               .Join(EGefyraJoin.Left, tblCustomer)
//               .On
//               (
//                   o => o.OpenBlock().Compare(clmCID, EGefyraComparator.Equal, 1).CloseBlock().And().Compare(clmCID, EGefyraComparator.NotLike, 1)
//               )
//               .Where
//               (
//                   o => o.OpenBlock().Compare(clmCID, EGefyraComparator.Equal, 1).CloseBlock().And().Compare(clmCID, EGefyraComparator.NotLike, 1)
//               );
//}

//o.Stop();
//long l1 = o.ElapsedMilliseconds;




//GefyraTable
//    tblCustomers = Gefyra.GetTable<CustomerModel>();

//var a0 = tblCustomers.GetSQL();
//var a1 = tblCustomers.GetColumn(nameof(CustomerModel.ID)).GetSQL();
//var a2 = tblCustomers.GetColumn(nameof(CustomerModel.FirstName)).GetSQL();

//var B = tblCustomers.As("B");

//var b0 = B.GetSQL();
//var b1 = B.GetColumn(nameof(CustomerModel.ID)).GetSQL();
//var b2 = B.GetColumn(nameof(CustomerModel.FirstName)).GetSQL();
//var b3 = B.GetColumn("*").GetSQL();

//Stopwatch o = new Stopwatch();
//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    tblCustomers.GetColumn(nameof(CustomerModel.ID));

//o.Stop();
//long l1 = o.ElapsedMilliseconds;




//tblCustomers.GetColumn(nameof(CustomerModel.ID));
//tblCustomers.GetColumn("iCID");












//CustomerModel c = new CustomerModel();
//c.CSingle = 100;
//c.CDouble = 200f;
//c.ID1 = 15;
//c.ID0 = 20;
//c.LastnameSetGetPublic = "Pietro";

//CustomerModel a = 
//ObjectUtils.Copy<CustomerModel>(c);

//Int32? ciao = 30;
//Int32Utils.Parse(ciao);

//Double? dsasad = 30.5d;
//Stopwatch o = new Stopwatch();
//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    Int32Utils.Parse(dsasad);

//o.Stop();
//long l1 = o.ElapsedMilliseconds;











//Func<CustomerModel, Boolean> dasc = (o) => o.CDouble == 100d;

//Console.WriteLine("Hello, World!");

////ExtendedCustomerModel m = new ExtendedCustomerModel();

////ObjectUtils.IsInstance(m, typeof(CustomerModel));

//var sdadsac = ReflectionUtils.GetMembers(dasc);
//var rewrewwer = ReflectionUtils.GetInstructions(dasc);

//Stopwatch o = new Stopwatch();
//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    ReflectionUtils.GetOpCode("NOP");

//o.Stop();
//long l1 = o.ElapsedMilliseconds;

//var c =
//typeof(CustomerModel).MetadataToken;

//var d =
//typeof(CustomerModel).MetadataToken;

//ReflectionUtils.GetMembers(typeof(CustomerModel));
//ReflectionUtils.GetMembers(typeof(ExtendedCustomerModel));
//ReflectionUtils.GetMembers(typeof(CustomerModelB));

//CustomerModel m1 = new CustomerModel();

//MemberInfo? prodsava = 
//TypeUtils.GetMember(typeof(CustomerModel), nameof(CustomerModel.ID0));
//TypeUtils.GetProperties(typeof(CustomerModel));

//Stopwatch o = new Stopwatch();
//o.Start();

//for (int i = 0; i < 1000000; i++)
//    //typeof(CustomerModel).GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
//TypeUtils.GetProperties(typeof(CustomerModel), nameof(CustomerModel.LastnameSetGetPublic));

//o.Stop();

//long l1 = o.ElapsedMilliseconds;

//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    TypeUtils.GetCustomAttributes<ExtensionAttribute>(prodsava);

//o.Stop();


//long l3 = o.ElapsedMilliseconds;

//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    TypeUtils.GetMemberValueType(prodsava);

//o.Stop();


//long l3212 = o.ElapsedMilliseconds;




//o.Restart();

//for (int i = 0; i < 1000000; i++)
//    MemberUtils.Get(typeof(CustomerModel), "ID0");

//o.Stop();


//long l2 = o.ElapsedMilliseconds;




//o.Restart();

//for (int i = 0; i < 1000000; i++)
//{
//    var fafsaasf = Int32Utils.Parse(MemberTypes.All);
//}

//o.Stop();


//long l5 = o.ElapsedMilliseconds;



//ObjectUtils.IsInstance(m1, typeof(CustomerModel));

//ObjectUtils.IsSubclass(typeof(CustomerModel), typeof(CustomerModel));

//Int32? prova = 23;

//Decimal? dcm;
//dcm = ObjectUtils.Parse<Decimal?>(prova);

//Convert.ToDecimal(prova);
//var c  = ObjectUtils.Parse<UInt32?>(null, true);

//var rrr = ObjectUtils.Parse<EAttributeTarget>("Class");
//var rrrdsa = ObjectUtils.Parse<EAttributeTarget?>("dsalodsaw");
//EAttributeTarget? coaodsa = ObjectUtils.Parse<EAttributeTarget?>(EAttributeTarget.Class);
//var dsasda = ObjectUtils.Parse<EAttributeTarget?>(1);
//var dsasdaj = ObjectUtils.Parse<EAttributeTarget>(30214);

//String? dasads;

//Byte[]? a = new byte[] { 20, 30 };

//dasads = ObjectUtils.Parse<String?>(EAttributeTarget.Member);
//dasads = ObjectUtils.Parse<String?>('V');
//dasads = ObjectUtils.Parse<String?>('5');
//dasads = ObjectUtils.Parse<String?>(a);
//dasads = ObjectUtils.Parse<String?>("25,30300");
//dasads = ObjectUtils.Parse<String?>("Pietrro");
//dasads = ObjectUtils.Parse<String?>(UInt128.MaxValue);
////dcm = ObjectUtils.Parse<Decimal?>(new CustomerModel());


//m.ID0 = null;
//var v0 = ObjectUtils.Parse<String?>(m.ID0);
//var v1 = ObjectUtils.Parse<Single?>(m.ID0);
//var v2 = ObjectUtils.Parse<Double?>(m.ID0);

//m.ID0 = 2050;
//var v3 = ObjectUtils.Parse<String?>(m.ID0);
//var v4 = ObjectUtils.Parse<Single?>(m.ID0);
//var v5 = ObjectUtils.Parse<Double?>(m.ID0);

//m.FirstName = null;
//var v6 = ObjectUtils.Parse<String?>(m.FirstName);
//var v7 = ObjectUtils.Parse<Single?>(m.FirstName);
//var v8 = ObjectUtils.Parse<Double?>(m.FirstName);

//m.FirstName = "Pietro";
//var v9 = ObjectUtils.Parse<String?>(m.FirstName);
//var v10 = ObjectUtils.Parse<Single?>(m.FirstName);
//var v11 = ObjectUtils.Parse<Double?>(m.FirstName);

//m.FirstName = "23.5010";
//var v12 = ObjectUtils.Parse<String?>(m.FirstName);
//var v13 = ObjectUtils.Parse<Single?>(m.FirstName);
//var v14 = ObjectUtils.Parse<Double?>(m.FirstName);

//bool
//    b = true;