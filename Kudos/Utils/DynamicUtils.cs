using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class DynamicUtils
    {
        public static dynamic? Union(params dynamic[]? da)
        {
            if (da == null) return null;

            List<ExpandoObject> leo = new List<ExpandoObject>(da.Length);

            ExpandoObject? eoi;
            for(int i=0; i<da.Length; i++)
            {
                eoi = da[i] as ExpandoObject;
                if (eoi == null) continue;
                leo.Add(eoi);
            }

            return ExpandoObjectUtils.Union(leo.ToArray());
        }

        public static Boolean IsNull(dynamic? dnm)
        {
            Object? o = dnm as Object;
            if (o != null)
                return false;

            Enum? e = dnm as Enum;
            if (e != null)
                return false;

            return true;
        }

        public static Object? GetValue(dynamic? dnm, String? s)
        {
            IDictionary<String, Object?> d = dnm as IDictionary<String, Object?>;
            if (d == null) return null;
            Object? o;
            d.TryGetValue(s, out o);
            return o;
        }
    }
}