using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Constants
{
    internal static class CGefyraConventionalPrefix
    {
        internal static readonly String
            Class = "tbl";

        internal static class Member
        {
            internal static readonly String
                Object = "o",
                Boolean = "b",
                String = "s",
                UInt16 = "ushr",
                UInt32 = "ui",
                UInt64 = "ul",
                Int16 = "shr",
                Int32 = "i",
                Int64 = "l",
                Single = "f",
                Double = "d",
                Decimal = "dcm";
        }
    }
}
