using Kudos.Constants;
using Kudos.Servers.KaronteModule.Attributes;
using Kudos.Servers.KaronteModule.Constants;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Kudos.Servers.KaronteModule.Descriptors.Routes
{
    public class KaronteRouteDescriptor
    {
        public readonly MemberInfo Member;
        public readonly String Pattern;
        public readonly Boolean IsRelative;

        internal KaronteRouteDescriptor(MemberInfo mi, String s)
        {
            Member = mi;
            Pattern = s;
            IsRelative = Pattern.Length > 0 && Pattern[0] != CCharacter.BackSlash;
            Pattern = Normalize(Pattern);
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
        
        internal String ResolveMemberName()
        {
            String s = Member.Name;

            if(Member.MemberType == MemberTypes.TypeInfo)
            { 
                if (s.EndsWith(CKaronteGenericKey.Controller, StringComparison.OrdinalIgnoreCase))
                    s = s.Substring(0, s.Length - CKaronteGenericKey.Controller.Length);
            }

            return s;
        }

        internal String ResolvePattern(ref KaronteControllerAttribute? kca)
        {
            String s = Pattern;
            String s0 = ResolveMemberName();

            switch (Member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    s = s.Replace(CKaronteRouteKey.Controller, s0);
                    break;
                case MemberTypes.Method:
                    s = s.Replace(CKaronteRouteKey.Action, s0);
                    break;
            }

            if (kca == null || kca.Version == null)
            {
                s = s.Replace(CKaronteRouteKey.Controller_Version, String.Empty);
                s = Regex.Replace(s, @"\"+CCharacter.BackSlash+"{2,}", "");
            }
            else
                s = s.Replace(CKaronteRouteKey.Controller_Version, "v" + kca.Version, StringComparison.OrdinalIgnoreCase);

            return Normalize(s);
        }
    }
}