using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    [Flags]
    internal enum EGefyraMethod
    {
        Equals = CBinaryFlag._0,
        NotEquals = CBinaryFlag._1,
        Contains = CBinaryFlag._2,
        NotContains = CBinaryFlag._3
    }
}
