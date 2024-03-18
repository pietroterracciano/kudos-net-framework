using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Enums.Columns
{
    [Flags]
    public enum EDatabaseColumnExtra
    {
        None = CBinaryFlag._0,
        AutoIncrement = CBinaryFlag._1
    }
}
