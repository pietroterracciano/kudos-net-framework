using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils.Members
{
    public static class ConstructorUtils
    {
        #region public static ConstructorInfo? Get(...)

        public static ConstructorInfo? Get(
            Type? t,
            BindingFlags bf = CBindingFlags.Public
        )
        {
            return Get(t, bf, null);
        }

        public static ConstructorInfo? Get(
            Type? t,
            params Type[]? a
        )
        {
            return Get(t, CBindingFlags.Public, a);
        }

        public static ConstructorInfo? Get(
            Type? t,
            BindingFlags bf = CBindingFlags.Public,
            params Type[]? a
        )
        {
            if (t != null)
                try { return t.GetConstructor(bf, a != null ? a : Type.EmptyTypes); } catch { }

            return null;
        }

        #endregion

        #region public static ConstructorInfo[]? Gets(...)

        public static ConstructorInfo[]? Gets(
            Type? t,
            BindingFlags bf = CBindingFlags.Public
        )
        {
            if (t != null)
                try { return t.GetConstructors(bf); } catch { }

            return null;
        }

        #endregion

        #region public static Object? Invoke()

        public static ObjectType? Invoke<ObjectType>(ConstructorInfo? c, params Object[]? a)
        {
            return ObjectUtils.Cast<ObjectType>(Invoke(c, a));
        }

        public static Object? Invoke(ConstructorInfo? c, params Object[]? a)
        {
            if (c != null)
                try { return c.Invoke(a); } catch { }

            return null;
        }

        #endregion

        #region public static Boolean CheckAndGenerateCompatibleParametersArray()

        public static Boolean CheckAndGenerateCompatibleParametersArray
        (
            ConstructorInfo? c, 
            out Object[] cps,
            Object[]? ips
        )
        {
            if (c == null)
            {
                cps = null;
                return false;
            }

            ParameterInfo[] pis = c.GetParameters();

            if(pis == null)
            {
                cps = pis;
                return true;
            }
            else if(pis.Length < 1)
            {
                cps = new object[0];
                return true;
            }
            else if(ips == null || ips.Length < 1)
            {
                cps = null;
                return false;
            }

            cps = new Object[pis.Length];

            for(int i=0; i<pis.Length; i++)
            {
                if (pis[i] == null) continue;

                for(int j=0; j<ips.Length; j++)
                {
                    if 
                    (
                        ips[j] == null 
                        || !ips[j].GetType().Equals(pis[i].ParameterType)
                    ) 
                        continue;

                    cps[i] = ips[j];
                    break;
                }

                if (cps[i] == null)
                {
                    cps = null;
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
