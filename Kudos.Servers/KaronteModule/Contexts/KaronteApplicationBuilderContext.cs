using Kudos.Utils;
using System;
using System.Collections.Generic;

namespace Kudos.Servers.KaronteModule.Contexts
{
    internal class KaronteApplicationBuilderContext
    {
        private readonly Dictionary<String, Object?> _d;

        public KaronteApplicationBuilderContext()
        {
            _d = new Dictionary<String, Object?>();
        }

        public bool SetMeta(String? s, Object? o)
        {
            if (!Normalize(ref s)) return false;
            _d[s] = o;
            return true;
        }

        public ObjectType? GetMeta<ObjectType>(String? s)
        {
            if (Normalize(ref s))
            {
                Object? o;
                if (_d.TryGetValue(s, out o))
                    return ObjectUtils.Cast<ObjectType>(o);
            }

            return default(ObjectType);
        }

        private static Boolean Normalize(ref String? s)
        {
            if (s == null) return false;
            s = s.ToUpper(); return true;
        }
    }
}
