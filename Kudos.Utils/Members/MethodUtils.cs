using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    public abstract class MethodUtils
    {
        #region public static MethodInfo? Get()

        public static MethodInfo? Get<Type>(String? s, BindingFlags bf = CBindingFlags.Public)
        {
            return Get(typeof(Type), s, bf);
        }
        public static MethodInfo? Get(Type? t, String? s, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null && !String.IsNullOrWhiteSpace(s))
                try { return t.GetMethod(s, bf); } catch { }

            return null;
        }

        #endregion

        #region public static MethodInfo[]? Get(...)

        public static MethodInfo[]? Get(Type? t, BindingFlags bf = CBindingFlags.Public)
        {
            if (t != null)
                try { return t.GetMethods(bf); } catch { }

            return null;
        }

        #endregion

        #region public static Attribute? GetAttribute(...)

        public static AttributeType? GetAttribute<AttributeType>(
            MethodInfo? m,
            Boolean bFromInheritance = false
        )
            where AttributeType : Attribute
        {
            return MemberUtils.GetAttribute<AttributeType>(m, bFromInheritance);
        }

        public static Attribute? GetAttribute(
            Type? t,
            MethodInfo? m,
            Boolean bFromInheritance = false
        )
        {
            return MemberUtils.GetAttribute(t, m, bFromInheritance);
        }

        #endregion

        #region public static Attribute[] GetAttributes(...)

        public static AttributeType[]? GetAttributes<AttributeType>(
            MethodInfo? m,
            Boolean bFromInheritance = false
        )
            where AttributeType : Attribute
        {
            return MemberUtils.GetAttributes<AttributeType>(m, bFromInheritance);
        }

        public static Attribute[]? GetAttributes(
            Type? t,
            MethodInfo? m,
            Boolean bFromInheritance = false
        )
        {
            return MemberUtils.GetAttributes(t, m, bFromInheritance);
        }

        #endregion

        #region public static ObjectType Invoke()

        public static ObjectType? Invoke<ObjectType>(MethodInfo m, Object o, params Object[] a)
        {
            if (m != null)
                try { return ObjectUtils.Cast<ObjectType>(m.Invoke(o, a)); } catch { }

            return default(ObjectType?);
        }

        #endregion
    }
}
