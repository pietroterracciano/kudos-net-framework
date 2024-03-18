using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    public static class PropertyUtils
    {
        #region public static PropertyInfo? Get()

        public static PropertyInfo? Get(Type? t, String? s, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null && !String.IsNullOrWhiteSpace(s))
                try { return t.GetProperty(s, bf); } catch { }

            return null;
        }

        #endregion

        #region public static PropertyInfo[]? Get(...)

        public static PropertyInfo[]? Get(Type? t, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null)
                try { return t.GetProperties(bf); } catch { }

            return null;
        }

        #endregion

        #region public static Attribute? GetAttribute(...)

        public static AttributeType? GetAttribute<AttributeType>(
            PropertyInfo? p,
            Boolean bFromInheritance = false
        )
            where AttributeType : Attribute
        {
            return MemberUtils.GetAttribute<AttributeType>(p, bFromInheritance);
        }

        public static Attribute? GetAttribute(
            Type? t,
            PropertyInfo? p,
            Boolean bFromInheritance = false
        )
        {
            return MemberUtils.GetAttribute(t, p, bFromInheritance);
        }

        #endregion

        #region public static Attribute[] GetAttributes(...)

        public static AttributeType[]? GetAttributes<AttributeType>(
            PropertyInfo? p,
            Boolean bFromInheritance = false
        )
            where AttributeType : Attribute
        {
            return MemberUtils.GetAttributes<AttributeType>(p, bFromInheritance);
        }

        public static Attribute[]? GetAttributes(
            Type? t,
            PropertyInfo? p,
            Boolean bFromInheritance = false
        )
        {
            return MemberUtils.GetAttributes(t, p, bFromInheritance);
        }

        #endregion

        #region public static Boolean SetValue(...)

        public static Boolean SetValue(Object? o, PropertyInfo? p, Object? v)
        {
            if (o != null && p != null)
                try { p.SetValue(o, ObjectUtils.ChangeType(v, p.PropertyType)); return true; } catch { }

            return false;
        }

        #endregion

        #region public static Object? GetValue(...)

        public static Object? GetValue(Object? o, PropertyInfo? p)
        {
            if (p != null)
                try { return p.GetValue(o); } catch { }

            return null;
        }

        #endregion
    }
}