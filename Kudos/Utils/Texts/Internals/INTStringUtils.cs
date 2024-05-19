using Kudos.Constants;
using Kudos.Utils.Parsings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Texts.Internals
{
    internal class INTStringUtils : AINTClassParsingUtils<String>
    {
        protected override void OnNNParse(ref string? oIn, out string oOut)
        {
            oOut = oIn != null ? oIn : String.Empty;
        }

        protected override void OnParse(ref Type to, ref object oIn, out string? oOut)
        {
            if (to == CType.BytesArray) { oOut = Encoding.UTF8.GetString(oIn as byte[]); return; }
            oOut = Convert.ToString(oIn);
        }
    }
}