using Kudos.Constants;
using Kudos.Utils.Parsings;
using System;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTDecimalUtils : AINTNumericParsingUtils<decimal>
    {
        protected override void OnNNParse(ref decimal? oIn, out decimal oOut)
        {
            oOut = oIn != null ? oIn.Value : 0.0m;
        }

        protected override void OnParse(ref object oIn, out decimal? oOut)
        {
            oOut = Convert.ToDecimal(oIn);
        }

        protected override void OnStringParse(ref string sIn, out decimal? oOut)
        {
            decimal oOut0;
            if (decimal.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}
