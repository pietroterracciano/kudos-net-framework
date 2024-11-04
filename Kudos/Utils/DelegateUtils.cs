using System;
using Kudos.Constants;

namespace Kudos.Utils
{
	public static class DelegateUtils
	{
        #region public static Boolean Is(...)

        public static Boolean Is(Object? o) { return Is(TypeUtils.Get(o)); }
        public static Boolean Is(Type? t) { return ObjectUtils.IsSubclass(t, CType.Delegate); }

        #endregion

        #region public static Boolean DynamicInvoke(...)

        public static Object? DynamicInvoke(Object? o, params Object?[]? oa) { return DynamicInvoke(o as Delegate, oa); }
        public static Object? DynamicInvoke(Delegate? dlg, params Object?[]? oa)
        {
            if (dlg != null) try { return dlg.DynamicInvoke(oa); } catch { }
            return null;
        }

        #endregion
    }
}

