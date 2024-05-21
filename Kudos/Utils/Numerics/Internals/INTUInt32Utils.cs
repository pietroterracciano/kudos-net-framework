using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTUInt32Utils : AINTNumericParsingUtils<uint>
    {
        protected override void OnNNParse(ref UInt32? oIn, out UInt32 oOut)
        {
            oOut = oIn != null ? oIn.Value : 0;
        }

        //protected override void OnParse(ref object oIn, out UInt32? oOut)
        //{
        //    oOut = Convert.ToUInt32(oIn);
        //}

        protected override void OnStringParse(ref string sIn, out UInt32? oOut)
        {
            UInt32 oOut0;
            if (UInt32.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
