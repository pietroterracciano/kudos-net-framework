using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class ArrayUtils
    {
        public static Boolean IsValidIndex(Array oArray, Int32 i32Index)
        {
            return
                oArray != null
                && i32Index > -1
                && i32Index < oArray.Length;
        }

        /// <summary>Nullable</summary>
        public static Object GetObject(Array oArray, Int32 i32Index)
        {
            return
                IsValidIndex(oArray, i32Index)
                    ? oArray.GetValue(i32Index)
                    : null;
        }
    }
}
