using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    public static class FieldUtils
    {
        #region public static FieldInfo? Get()

        public static FieldInfo? Get<Type>(String? s, BindingFlags bf = CBindingFlags.Public)
        {
            return Get(typeof(Type), s, bf);
        }
        public static FieldInfo? Get(Type? t, String? s, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null && !String.IsNullOrWhiteSpace(s))
                try { return t.GetField(s, bf); } catch { }

            return null;
        }

        #endregion

        #region public static FieldInfo[]? Get(...)

        public static FieldInfo[]? Get<Type>(BindingFlags bf = CBindingFlags.Public)
        {
            return Get(typeof(Type), bf);
        }
        public static FieldInfo[]? Get(Type? t, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null)
                try { return t.GetFields(bf); } catch { }

            return null;
        }

        #endregion

        #region public static Boolean SetValue(...)

        public static Boolean SetValue(Object? o, FieldInfo? f, Object? v)
        {
            if (o != null && f != null)
                try { f.SetValue(o, ObjectUtils.ChangeType(v, f.FieldType)); return true; } catch { }

            return false;
        }

        #endregion

        #region public static Object? GetValue(...)

        public static Object? GetValue(Object? o, FieldInfo? f)
        {
            if (f != null)
                try { return f.GetValue(o); } catch { }

            return null;
        }

        #endregion
    }
}
