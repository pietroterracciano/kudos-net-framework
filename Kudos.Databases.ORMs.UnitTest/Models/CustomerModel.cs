using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.UnitTest.Models
{
    [GefyraTableAttribute("local.tblCustomersWMPK")]
    public class CustomerModel
    {
        public static List<UInt32> __proaava = new List<UInt32>() { 1, 2, 3, };

        [Flags]
        public enum EType
        {
            Administrator = CBinaryFlag._0,
            User = CBinaryFlag._1
        }

        [GefyraColumnAttribute("uiCID0")]
        public UInt32 ID0 { get; set; }
        [GefyraColumnAttribute("sCID1")]
        public String ID1 { get; set; }

        public String Ciao { get; set; }

        [GefyraColumnAttribute("sCFirstName")]
        public String FirstName;

        [GefyraColumnAttribute("sCLastName")]
        public String LastName;

        [GefyraColumnAttribute("bCIsDeleted")]
        public Boolean IsDeleted;

        [GefyraColumnAttribute("eCType")]
        public EType Type;

        [GefyraColumnAttribute("oCProva0")]
        public List<UInt32> Prova0;

        //[GefyraColumnAttribute("oCProva1")]
        //public List<UInt32> Prova1;

        public UInt32 ParentID { get; set; }

        public CustomerModel Parent;


        public CustomerModel Child;

        //public CustomerAddressModel Address;

        //public CustomerOrderModel Order;

        public CustomerModel()
        {
            Prova0 = new List<uint>();
            Prova0.Add(1);
        }
    }
}
