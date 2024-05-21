using System;
using System.Collections;

namespace Kudos.Utils.Collections
{
    public abstract class CollectionUtils
    {
        #region public static Boolean IsValidIndex(...)

        public static Boolean IsValidIndex(ICollection? o, Int32 i)
        {
            return
                o != null
                && i > -1
                && i < o.Count;
        }

        #endregion
    }
}