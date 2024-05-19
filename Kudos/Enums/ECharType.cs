using Kudos.Constants;
using System;

namespace Kudos.Enums
{
    [Flags]
    public enum ECharType
    {
        StandardLowerCase = CBinaryFlag._0,
        StandardUpperCase = CBinaryFlag._1,
        Numeric = CBinaryFlag._2,
        Punctuation = CBinaryFlag._3,
        Special = CBinaryFlag._4,
        Math = CBinaryFlag._5,
        AccentedLowerCase = CBinaryFlag._6,
        AccentedUpperCase = CBinaryFlag._7
    }
}