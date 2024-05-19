using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTInt16Utils : AINTNumericParsingUtils<short>
    {
        protected override void OnNNParse(ref Int16? oIn, out Int16 oOut)
        {
            oOut = oIn != null ? oIn.Value : (short)0;
        }

        protected override void OnParse(ref object oIn, out Int16? oOut)
        {
            oOut = Convert.ToInt16(oIn);
        }

        protected override void OnStringParse(ref string sIn, out Int16? oOut)
        {
            Int16 oOut0;
            if (Int16.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}