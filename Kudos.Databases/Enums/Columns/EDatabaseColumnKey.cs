using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Enums.Columns
{
    [Flags]
    public enum EDatabaseColumnKey
    {
        None = CBinaryFlag._0,
        Primary = CBinaryFlag._1,
        Unique = CBinaryFlag._2
    }
}
