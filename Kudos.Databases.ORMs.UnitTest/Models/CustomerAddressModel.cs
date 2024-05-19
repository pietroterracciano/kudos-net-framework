using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.UnitTest.Models
{
    [GefyraTableAttribute("tblCustomerAddresses")]
    public class CustomerAddressModel
    {
        [GefyraColumnAttribute("uiCAID")]
        public UInt32 ID;

        [GefyraColumnAttribute("sCAPostalCode")]
        public String PostalCode;

        [GefyraColumnAttribute("uiCACustomerID")]
        public UInt32 CustomerID;

        public CustomerAddressModel()
        {

        }
    }
}
