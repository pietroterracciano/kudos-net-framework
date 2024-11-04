using Kudos.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.UnitTest.Models
{
    [GefyraTable("arteidedb", "tblClients")]
    public class ClientModel
    {
        [GefyraColumn("iClnID")]
        public UInt32 ID { get; set; }

        [GefyraJoin]
        public OpusesModel[]? Opuses;

        [GefyraColumn("sClnFirstname")]
        public String FirstName;

        [GefyraColumn("sClnLastname")]
        public String LastName;

        [GefyraColumn("sClnMail")]
        public String Mail;

        [GefyraColumn("iClnRoleID")]
        public UInt32 RoleID;
    }
}
