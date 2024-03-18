using System;
using System.Collections;

namespace Kudos.Utils.Collections
{
    public abstract class CollectionUtils
    {
        #region public static ICollection? Cast(...)

        public static ICollection? Cast(Object? o)
        {
            return o as ICollection;
        }

        #endregion

        #region public static Boolean IsValidIndex(...)

        public static Boolean IsValidIndex(ICollection? o, Int32 i)
        {
            return
                o != null
                && i > -1
                && i < o.Count;
        }

        #endregion

/*

public static ICollection? AsCollection(Object? oObject)
{
    return oObject as ICollection;
}

public static Boolean IsCollection(Object? oObject)
{
    return AsCollection(oObject) != null;
}

public static IEnumerator AsEnumerator(Object? oObject)
{
    IEnumerable? enmObject = AsEnumerable(oObject);

    return enmObject != null
        ? enmObject.GetEnumerator()
        : null;
}

public static IEnumerable? AsEnumerable(Object? oObject)
{
    return oObject as IEnumerable;
}

public static Boolean IsEnumerable(Object? oObject)
{
    return AsEnumerable(oObject) != null;
}
*/
}
}