using Kudos.Constants;
using System;

namespace Kudos.Enums
{
    [Flags]
    public enum ECharType
    {
        StandardLowerCase = CBinaryFlag.__,
        StandardUpperCase = CBinaryFlag._0,
        Numeric = CBinaryFlag._1,
        Punctuation = CBinaryFlag._2,
        Special = CBinaryFlag._3,
        Math = CBinaryFlag._4,
        AccentedLowerCase = CBinaryFlag._5,
        AccentedUpperCase = CBinaryFlag._6
    }
}