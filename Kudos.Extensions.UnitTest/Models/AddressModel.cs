using Kudos.Databases.ORMs.GefyraModule.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Extensions.UnitTest.Models
{
    [GefyraTableAttribute("tblAddresses")]
    internal class AddressModel
    {
        [GefyraColumn("iAID")]
        public Int32? ID;

        [GefyraColumn("sACAP")]
        public String? CAP;

        [GefyraColumn("sAStreet")]
        public String? Street;

        [GefyraColumn("sAStreetNumber")]
        public String? StreetNumber;

        [GefyraColumn("bAIsDeleted")]
        public Boolean? IsDeleted;
    }
}
