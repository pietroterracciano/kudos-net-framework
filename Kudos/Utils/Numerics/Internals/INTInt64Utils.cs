using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTInt64Utils : AINTNumericParsingUtils<long>
    {
        protected override void OnNNParse(ref Int64? oIn, out Int64 oOut)
        {
            oOut = oIn != null ? oIn.Value : 0L;
        }

        //protected override void OnParse(ref object oIn, out Int64? oOut)
        //{
        //    oOut = Convert.ToInt64(oIn);
        //}

        protected override void OnStringParse(ref string sIn, out Int64? oOut)
        {
            Int64 oOut0;
            if (Int64.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}