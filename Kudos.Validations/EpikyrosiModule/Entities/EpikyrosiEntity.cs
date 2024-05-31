using System;
using Kudos.Constants;
using Kudos.Reflection.Utils;
using System.Collections.Generic;
using System.Text;
using Kudos.Types;
using Kudos.Utils.Collections;
using System.Reflection;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Utils;

namespace Kudos.Validations.EpikyrosiModule.Entities
{
	public class
        EpikyrosiEntity
    :
        IEpikyrosiEntity
    {
        #region ... static ...

        internal static readonly EpikyrosiEntity
            Invalid;

        // Type -> EpikyrosiEntity
        private static readonly Dictionary<MemberInfo, EpikyrosiEntity>
            __d;

        static EpikyrosiEntity()
        {
            __d = new Dictionary<MemberInfo, EpikyrosiEntity>();
        }

        #region internal static void Get<...>(...)

        internal static void Get<T>(ref String? s, out EpikyrosiEntity ee)
        {
            Type t = typeof(T);
            Get(ref t, ref s, out ee);
        }
        internal static void Get(ref Type? t, ref String? s, out EpikyrosiEntity ee)
        {
            MemberInfo? mi = ReflectionUtils.GetMember(t, s, CBindingFlags.Public);
            Get(ref mi, out ee);
        }
        internal static void Get(ref MemberInfo? mi, out EpikyrosiEntity ee)
        {
            MemberTypes? mt = ReflectionUtils.GetMemberType(mi);

            if
            (
                mt == null
                || !CMemberTypes.FieldProperty.HasFlag(mt)
            )
            {
                ee = Invalid;
                return;
            }

            lock (__d)
            {
                if (__d.TryGetValue(mi, out ee) && ee != null)
                    return;

                ee = new EpikyrosiEntity(ref mi);
                __d[mi] = ee;
            }
        }

        #endregion

        #endregion

        #region DeclaringMember

        public MemberInfo DeclaringMember { get; private set; }

        #endregion

        internal EpikyrosiEntity(ref MemberInfo mi)
		{
            DeclaringMember = mi;
		}
	}
}