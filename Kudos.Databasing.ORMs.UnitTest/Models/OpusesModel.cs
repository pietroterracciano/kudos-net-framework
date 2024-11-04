using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.UnitTest.Models
{
    [GefyraTable("arteidedb", "tblOpuses")]
    public class OpusesModel
    {
        [GefyraColumn("iOpsID")]
        public UInt32 ID { get; set; }

        [GefyraColumn("iOpsClientID")]
        public UInt32 ClientID;

        [GefyraColumn("sOpsDescription")]
        public String Description;
    }
}
