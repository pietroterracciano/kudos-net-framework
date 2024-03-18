using System;
using System.Text;

namespace Kudos.Servers.KaronteModule.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class KaronteControllerAttribute : Attribute
    {
        private static readonly Char __cDot = '.';

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
                Version = "" + iMajor + __cDot + iMinor;
            else
                Version = "" + iMajor + __cDot + iMinor + __cDot + iPatch;
        }
    }
}