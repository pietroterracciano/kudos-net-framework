using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Utils
{
    internal static class DynamicUtils
    {
        public static Boolean IsNull(dynamic? dnm)
        {
            Object? o = dnm as Object;
            if (o != null) 
                return false;

            Enum? e = dnm as Enum;
            if (e != null) 
                return false;

            return true;
        }
    }
}