using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Enums
{
    public enum EDatabaseDataType
    {
        TinyInteger,
        UnsignedTinyInteger,

        SmallInteger,
        UnsignedSmallInteger,

        MediumInteger,
        UnsignedMediumInteger,

        Integer,
        UnsignedInteger,

        BigInteger,
        UnsignedBigInteger,

        Double,
        UnsignedDouble,

        Boolean,

        VariableChar,
        Text,
        MediumText,
        LongText,

        Json
    }
}
