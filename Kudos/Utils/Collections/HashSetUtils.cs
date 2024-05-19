using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Collections
{
    public static class HashSetUtils
    {
        public static void IntersectWith<T>(HashSet<T>? hs0, IEnumerable<T>? hs1)
        {
            if (hs0 == null) return;
            else if (hs1 == null) hs1 = new HashSet<T>(0);
            hs0.IntersectWith(hs1);
        }

        public static void UnionWith<T>(HashSet<T>? hs0, IEnumerable<T>? hs1)
        {
            if (hs0 == null || hs1 == null) return;
            hs0.UnionWith(hs1);
        }
    }
}
