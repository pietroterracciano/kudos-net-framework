using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTUInt16Utils : AINTNumericParsingUtils<ushort>
    {
        protected override void OnNNParse(ref UInt16? oIn, out UInt16 oOut)
        {
            oOut = oIn != null ? oIn.Value : (ushort)0;
        }

        protected override void OnParse(ref object oIn, out UInt16? oOut)
        {
            oOut = Convert.ToUInt16(oIn);
        }

        protected override void OnStringParse(ref string sIn, out UInt16? oOut)
        {
            UInt16 oOut0;
            if (UInt16.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}