using Kudos.Constants;
using System;
using System.Numerics;

namespace Kudos.Utils.Parsings
{
    internal abstract class AINTNumericParsingUtils<T> : AINTStructParsingUtils<T>
        where T : struct, INumberBase<T>
    {
        protected abstract void OnStringParse(ref String sIn, out T? oOut);
        protected abstract void OnParse(ref object oIn, out T? oOut);

        protected override void OnParse(ref Type to, ref object oIn, out T? oOut)
        {
            if (to == CType.Char)
            {
                char? c = oIn as char?;
                if (!char.IsNumber(c.Value))
                {
                    oOut = default;
                    return;
                }
                String s = char.ToString(c.Value);
                OnStringParse(ref s, out oOut);
                return;
            }
            else if (to == CType.DateTime) 
                oIn = ((DateTime)oIn).Ticks;
            else if (ObjectUtils.IsSubclass(to, CType.String))
            {
                String? s = oIn as String;
                OnStringParse(ref s, out oOut);
                return;
            }
            OnParse(ref oIn, out oOut);
        }
    }
}