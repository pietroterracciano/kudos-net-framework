using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Enums
{
    [Flags]
    public enum EDictionaryTryGetValueResult
    {
        NullKey = CBinaryFlag._0,
        KeyNotExists = CBinaryFlag._1,
        KeyExists = CBinaryFlag._2
    }
}
