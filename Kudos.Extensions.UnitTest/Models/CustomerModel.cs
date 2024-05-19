using Kudos.Constants;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Extensions.Attributes;
using Kudos.Extensions.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Extensions.UnitTest.Models
{
    [GefyraTableAttribute("mioSchema.tblCustomers")]
    internal class CustomerModel
    {
        [GefyraColumn("iCID")]
        public Int32? ID;

        [GefyraColumn("iCAID")]
        public Int32? AddressID;

        [GefyraColumn("sCFirstName")]
        public String? FirstName;

        [GefyraColumn("sCLastName")]
        public String? LastName;

        [GefyraColumn("bCIsDeleted")]
        public Boolean? IsDeleted;
    }
}
