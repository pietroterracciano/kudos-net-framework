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

        private static readonly StringBuilder
            __sb;
        // Type.Module.MetadataToken -> Type.MetadataToken -> EpikyrosiEntity
        private static readonly Dictionary<int, Dictionary<int, EpikyrosiEntity>>
            __d;

        static EpikyrosiEntity()
        {
            __d = new Dictionary<int, Dictionary<int, EpikyrosiEntity>>();
            __sb = new StringBuilder();
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

            string? sn;

            lock (__d)
            {
                Dictionary<int, EpikyrosiEntity>? d;
                if (!__d.TryGetValue(mi.Module.MetadataToken, out d) || d == null)
                    __d[mi.Module.MetadataToken] = d = new Dictionary<int, EpikyrosiEntity>();
                else if (d.TryGetValue(mi.MetadataToken, out ee))
                    return;

                ee = new EpikyrosiEntity(ref mi);
                d[mi.MetadataToken] = ee;
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