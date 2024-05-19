using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Types
{
    public class Metas
    {
        private readonly Dictionary<String, Object?> _d;
        private readonly StringComparison _e;

        public Metas(StringComparison e = StringComparison.Ordinal)
        {
            _e = e;
            _d = new Dictionary<String, Object?>();
        }

        public bool Set(String? s, Object? o) { return Set(s, o, _e); }
        public bool Set(String? s, Object? o, StringComparison e)
        {
            if (!Normalize(ref s, ref e)) return false;
            _d[s] = o;
            return true;
        }

        public T? Get<T>(String? s) { return Get<T>(s, _e); }
        public T? Get<T>(String? s, StringComparison e)
        {
            return ObjectUtils.Cast<T>(Get(s, e));
        }

        public Object? Get(String? s) { return Get(s, _e); }
        public Object? Get(String? s, StringComparison e)
        {
            if (Normalize(ref s, ref e))
            {
                Object? o;
                if (_d.TryGetValue(s, out o))
                    return o;
            }

            return null;
        }

        private static Boolean Normalize(ref String? s, ref StringComparison e)
        {
            if (s == null) return false;

            switch (e)
            {
                case StringComparison.OrdinalIgnoreCase:
                case StringComparison.CurrentCultureIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                    s = s.ToUpper();
                    break;
            }

            return true;
        }
    }
}
