using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Enums
{
    [Flags]
    public enum EBinaryEncoding
    {
        // Hexadecimal
        Base16 = CBinaryFlag.__,
        Base64 = CBinaryFlag._0,
    }
}
