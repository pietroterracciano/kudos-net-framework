using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    [Flags]
    public enum EGefyraColumnAttribute
    {
        None = CBinaryFlag._0,
        AutoIncremental = CBinaryFlag._1,
        NotNullable = CBinaryFlag._2
    }
}
