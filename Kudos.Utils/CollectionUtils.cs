using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    public static class CollectionUtils
    {
        public static Boolean IsValidIndex(ICollection oCollection, Int32 i32Index)
        {
            return
                oCollection != null
                && i32Index > -1
                && i32Index < oCollection.Count;
        }
    }
}
