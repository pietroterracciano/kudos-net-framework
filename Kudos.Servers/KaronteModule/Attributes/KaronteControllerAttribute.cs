using System;
using System.Text;
using Kudos.Constants;

namespace Kudos.Servers.KaronteModule.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class KaronteControllerAttribute : Attribute
    {
        public readonly String? Version;

        public KaronteControllerAttribute() : this(null, null, null, null) { }
        public KaronteControllerAttribute(Int32 iMajor) : this(iMajor, null, null, null) { }
        public KaronteControllerAttribute(Int32 iMajor, Int32 iMinor) : this(iMajor, iMinor, null, null) { }
        public KaronteControllerAttribute(Int32 iMajor, Int32 iMinor, Int32 iPatch) : this(iMajor, iMinor, iPatch, null) { }

        private KaronteControllerAttribute(Int32? iMajor, Int32? iMinor, Int32? iPatch, Object? o)
        {
            if (iMajor == null)
                Version = null;
            else if (iMinor == null)
                Version = "" + iMajor;
            else if (iPatch == null)
                Version = "" + iMajor + CCharacter.Dot + iMinor;
            else
                Version = "" + iMajor + CCharacter.Dot + iMinor + CCharacter.Dot + iPatch;
        }
    }
}