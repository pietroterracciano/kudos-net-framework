using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Constants
{
    public static class CBindingFlags
    {
        public const BindingFlags
            PublicInstance = BindingFlags.Public | BindingFlags.Instance,
            PublicStatic = BindingFlags.Public | BindingFlags.Static,
            Public = PublicInstance | PublicStatic,
            NonPublicInstance = BindingFlags.NonPublic | BindingFlags.Instance,
            NonPublicStatic = BindingFlags.NonPublic | BindingFlags.Static,
            NonPublic = NonPublicInstance | NonPublicStatic;
    }
}