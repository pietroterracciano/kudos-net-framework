using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Enums
{
    [Flags]
    public enum EDatabaseType
    {
        MySQL = CBinaryFlag._0,
        MicrosoftSQL = CBinaryFlag._1
    }
}