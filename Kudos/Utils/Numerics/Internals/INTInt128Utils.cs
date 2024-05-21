using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTInt128Utils : AINTNumericParsingUtils<Int128>
    {
        protected override void OnNNParse(ref Int128? oIn, out Int128 oOut)
        {
            oOut = oIn != null ? oIn.Value : 0;
        }

        //protected override void OnParse(ref object oIn, out Int128? oOut)
        //{
        //    oOut = (Int128)oIn;
        //}

        protected override void OnStringParse(ref string sIn, out Int128? oOut)
        {
            Int128 oOut0;
            if (Int128.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
