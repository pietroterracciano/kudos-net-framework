using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class ListUtils
    {
        public static Boolean IsValidIndex(IList lObjects, Int32 i32Index)
        {
            return
                lObjects != null
                && i32Index < lObjects.Count;
        }
    }
}