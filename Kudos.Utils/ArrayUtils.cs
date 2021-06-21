using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class ArrayUtils
    {
        public static Boolean IsValidIndex(Array aObjects, Int32 i32Index)
        {
            return
                aObjects != null
                && i32Index < aObjects.Length;
        }
    }
}
