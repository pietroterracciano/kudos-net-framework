using System;
using Kudos.Constants;

namespace Kudos.Utils
{
	public static class ActionUtils
	{
        #region public static Boolean Is(...)

        public static Boolean Is(Object? o) { return Is(TypeUtils.Get(o)); }
        public static Boolean Is(Type? t)
        {
            return
                t != null
                &&
                (
                    t == CType.Action
                    ||
                    (
                        t.FullName != null
                        && t.FullName.Contains(CType.Action.FullName, StringComparison.OrdinalIgnoreCase)
                    )
                );
        }

        #endregion

        #region public static Boolean Invoke(...)

        public static Boolean Invoke(Action? act)
        {
            if (act != null) try { act.Invoke(); return true; } catch { }
            return false;
        }

        public static Boolean DynamicInvoke(Action? act, params Object?[]? o)
        {
            if (act != null) try { act.DynamicInvoke(o); return true; } catch { }
            return false;
        }

        public static Boolean DynamicInvoke(Object? act, params Object?[]? o)
        {
            if (!Is(act)) return false;
            Delegate? dnm = act as Delegate;
            try { dnm.DynamicInvoke(o); return true; } catch { }
            return false;
        }

        #endregion

    }
}

