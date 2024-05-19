using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Parsings
{
    internal abstract class AINTStructParsingUtils<T>
        where T : struct
    {
        private readonly Type __;
        internal AINTStructParsingUtils() { __ = typeof(T); }

        protected abstract void OnParse(ref Type to, ref object oIn, out T? oOut);
        protected abstract void OnNNParse(ref T? oIn, out T oOut);

        public void Parse(ref object? oIn, out T? oOut)
        {
            if (oIn != null)
            {
                Type to = oIn.GetType();
                Type? tu = Nullable.GetUnderlyingType(to);
                if (tu != null) to = tu;
                if (to != __) try { OnParse(ref to, ref oIn, out oOut); return; } catch { }
                try { oOut = (T)oIn; return; } catch { }
            }

            oOut = default;
        }

        public void NNParse(ref object? oIn, out T oOut)
        {
            T? oOut0;
            Parse(ref oIn, out oOut0);
            OnNNParse(ref oOut0, out oOut);
        }
    }
}
