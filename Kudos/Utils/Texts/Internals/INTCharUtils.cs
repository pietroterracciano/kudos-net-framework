using Kudos.Utils.Parsings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Texts.Internals
{
    internal class INTCharUtils : AINTStructParsingUtils<Char>
    {
        protected override void OnNNParse(ref char? oIn, out char oOut)
        {
            oOut = oIn != null ? oIn.Value : Char.MinValue;
        }

        protected override void OnParse(ref Type to, ref object oIn, out char? oOut)
        {
            oOut = Convert.ToChar(oIn);
        }
    }
}
