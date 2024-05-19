using Kudos.Utils.Parsings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Conditionals.Internals
{
    internal class INTBooleanUtils : AINTStructParsingUtils<Boolean>
    {
        protected override void OnNNParse(ref bool? oIn, out bool oOut)
        {
            oOut = oIn != null ? oIn.Value : false;
        }

        protected override void OnParse(ref Type to, ref object oIn, out bool? oOut)
        {
            oOut = Convert.ToBoolean(oIn);
        }
    }
}
