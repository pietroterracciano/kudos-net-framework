using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTUInt64Utils : AINTNumericParsingUtils<ulong>
    {
        protected override void OnNNParse(ref UInt64? oIn, out UInt64 oOut)
        {
            oOut = oIn != null ? oIn.Value : 0L;
        }

        //protected override void OnParse(ref object oIn, out UInt64? oOut)
        //{
        //    oOut = Convert.ToUInt64(oIn);
        //}

        protected override void OnStringParse(ref string sIn, out UInt64? oOut)
        {
            UInt64 oOut0;
            if (UInt64.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
