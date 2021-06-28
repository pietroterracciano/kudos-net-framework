using Kudos.Crypters.Models.SALTs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Symmetrics.RijndaelNS.Models.SALTs
{
    public class RSALTPreferencesModel : SALTPreferencesModel
    {
        public Int32 Splice { get; set; }
    }
}
