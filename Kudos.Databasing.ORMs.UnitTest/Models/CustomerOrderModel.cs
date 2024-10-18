using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.UnitTest.Models
{
    [GefyraTableAttribute("tblCustomerOrders")]
    public class CustomerOrderModel
    {
        [GefyraColumnAttribute("uiCOID")]
        public UInt32 ID;

        [GefyraColumnAttribute("sCODescription")]
        public String Description;

        [GefyraColumnAttribute("uiCOCustomerID")]
        public UInt32 CustomerID;

        public CustomerOrderModel()
        {

        }
    }
}
