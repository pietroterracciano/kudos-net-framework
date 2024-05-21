using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils.Parsings;

namespace Kudos.Utils.Numerics.Internals
{
    internal class INTDoubleUtils : AINTNumericParsingUtils<double>
    {
        protected override void OnNNParse(ref double? oIn, out double oOut)
        {
            oOut = oIn != null ? oIn.Value : 0.0d;
        }

        //protected override void OnParse(ref object oIn, out double? oOut)
        //{
        //    oOut = Convert.ToDouble(oIn);
        //}

        protected override void OnStringParse(ref string sIn, out double? oOut)
        {
            double oOut0;
            if (double.TryParse(sIn, out oOut0)) { oOut = oOut0; return; }
            oOut = null;
        }
    }
}