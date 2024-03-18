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

        public Metas()
        {
            _d = new Dictionary<String, Object?>();
        }

        public bool Set(String? s, Object? o, StringComparison e = StringComparison.Ordinal)
        {
            if (!Normalize(ref s, ref e)) return false;
            _d[s] = o;
            return true;
        }

        public ObjectType? Get<ObjectType>(String? s, StringComparison e = StringComparison.Ordinal)
        {
            return Kudos_Utils_ObjectUtils_Parse<ObjectType>(Get(s, e));
        }

        public Object? Get(String? s, StringComparison e = StringComparison.Ordinal)
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

            switch(e)
            {
                case StringComparison.OrdinalIgnoreCase:
                case StringComparison.CurrentCultureIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                    s = s.ToUpper();
                    break;
            }

            return true;
        }

        #region Kudos

        #region Utils

        #region ObjectUtils

        public static ObjectType? Kudos_Utils_ObjectUtils_Parse<ObjectType>(Object? o)
        {
            return
                o != null
                && o is ObjectType?
                    ? (ObjectType?)o
                    : default(ObjectType?);
        }

        #endregion

        #endregion

        #endregion
    }
}
