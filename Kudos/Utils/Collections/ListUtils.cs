using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Collections
{
    public abstract class ListUtils : CollectionUtils
    {
        #region public static T? Get(...)

        public static T? Get<T>(List<T>? l, Int32 i)
        {
            return IsValidIndex(l, i) ? l[i] : default(T);
        }

        #endregion
    }
}