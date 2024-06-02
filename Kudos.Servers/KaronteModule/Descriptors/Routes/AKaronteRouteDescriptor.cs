using System;
using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;

namespace Kudos.Servers.KaronteModule.Descriptors.Routes
{
	public abstract class AKaronteRouteDescriptor
	{
        public readonly String Pattern, ResolvedMemberName, ResolvedPattern, HashKey;
        public readonly Boolean IsPatternRelative;

        internal AKaronteRouteDescriptor
        (
            ref String shk,
            ref String sp,
            ref String srp,
            ref String srmn
        )
        {
            Pattern = sp;
            IsPatternRelative = Pattern.Length > 0 && Pattern[0] != CCharacter.BackSlash;
            Pattern = Normalize(Pattern);
            ResolvedMemberName = srmn;
            ResolvedPattern = Normalize(srp);
            HashKey = shk;
        }

        private String Normalize(String s)
        {
            while
            (
                s.StartsWith(CCharacter.BackSlash)
                || s.StartsWith(CCharacter.Dot)
            )
                s = s.Substring(1);

            return CCharacter.BackSlash + s;
        }
    }
}

