using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTSingleUtils : AINTNumericParsingUtils<float>
    {
        protected override void OnNNParse(ref Single? oIn, out Single oOut)
        {
            oOut = oIn != null ? oIn.Value : 0.0f;
        }

        protected override void OnParse(ref object oIn, out Single? oOut)
        {
            oOut = Convert.ToSingle(oIn);
        }

        protected override void OnStringParse(ref string sIn, out Single? oOut)
        {
            Single oOut0;
            if (Single.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
