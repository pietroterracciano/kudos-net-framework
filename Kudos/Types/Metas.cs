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

        public Metas() : this(0, StringComparison.Ordinal) { }
        public Metas(StringComparison e) : this(0, e) { }
        public Metas(Int32 iSize) : this(iSize, StringComparison.Ordinal) { }
        public Metas(Int32 iSize, StringComparison e)
        {
            _e = e;
            _d = new Dictionary<String, Object?>(iSize);
        }

        public Int32 Count
        {
            get { return _d.Count; }
        }

        public Boolean Contains(String? s) { return Contains(s, _e); }
        public Boolean Contains(String? s, StringComparison e)
        {
            Boolean? b; Object? o;
            _TryGet(ref s, ref e, out b, out o);
            return b != null && b.Value;
        }

        public bool Set(String? s, Object? o) { return Set(s, o, _e); }
        public bool Set(String? s, Object? o, StringComparison e)
        {
            if (!_Normalize(ref s, ref e)) return false;
            _d[s] = o;
            return true;
        }

        public KeyValuePair<String, T?>[] Gets<T>()
        {
            KeyValuePair<String, Object?>[] kvpa = Gets();
            KeyValuePair<String, T?>[] kvpa0 = new KeyValuePair<String, T?>[kvpa.Length];

            for(int i=0;i<kvpa.Length; i++)
                kvpa0[i] = new KeyValuePair<string, T?>(kvpa[i].Key, ObjectUtils.Cast<T>(kvpa[i].Value));

            return kvpa0;
        }
        public KeyValuePair<String, Object?>[] Gets()
        {
            return _d.ToArray();
        }

        public T? Get<T>(String? s) { return Get<T>(s, _e); }
        public T? Get<T>(String? s, StringComparison e) { return ObjectUtils.Cast<T>(Get(s, e)); }
        public Object? Get(String? s) { return Get(s, _e); }
        public Object? Get(String? s, StringComparison e) { Object? o; _TryGet(ref s, ref e, out o); return o; }

        private void _TryGet(ref String? s, ref StringComparison e, out Object? o) { Boolean? b; _TryGet(ref s, ref e, out b, out o); }
        private void _TryGet(ref String? s, ref StringComparison e, out Boolean? b, out Object? o)
        {
            if (!_Normalize(ref s, ref e)) { b = null; o = null; return; }
            else if (!_d.TryGetValue(s, out o)) { b = false; return; }
            b = true;
        }

        private static Boolean _Normalize(ref String? s, ref StringComparison e)
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
