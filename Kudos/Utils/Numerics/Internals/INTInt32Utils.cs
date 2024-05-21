using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTInt32Utils : AINTNumericParsingUtils<int>
    {
        protected override void OnNNParse(ref Int32? oIn, out Int32 oOut)
        {
            oOut = oIn != null ? oIn.Value : 0;
        }

        //protected override void OnParse(ref object oIn, out Int32? oOut)
        //{
        //    oOut = Convert.ToInt32(oIn);
        //}

        protected override void OnStringParse(ref string sIn, out Int32? oOut)
        {
            Int32 oOut0;
            if (Int32.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
