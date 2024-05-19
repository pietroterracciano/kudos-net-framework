using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Parsings
{
    internal abstract class AINTClassParsingUtils<T>
        where T : class
    {
        private readonly Type __;
        internal AINTClassParsingUtils() { __ = typeof(T); }

        protected abstract void OnParse(ref Type to, ref object oIn, out T? oOut);
        protected abstract void OnNNParse(ref T? oIn, out T oOut);

        public void Parse(ref object? oIn, out T? oOut)
        {
            if (oIn != null)
            {
                Type to = oIn.GetType();
                if (to != __) try { OnParse(ref to, ref oIn, out oOut); return; } catch { }
                try { oOut = oIn as T; return; } catch { }
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
