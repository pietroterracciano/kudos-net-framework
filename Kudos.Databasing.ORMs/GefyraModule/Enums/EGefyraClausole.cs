using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    [Flags]
    internal enum EGefyraClausole
    {
        Insert = CBinaryFlag._0,
        Into = CBinaryFlag._1,
        Values = CBinaryFlag._2,

        Select = CBinaryFlag._3,
        From = CBinaryFlag._4,
        Join = CBinaryFlag._5,

        Update = CBinaryFlag._6,
        Set = CBinaryFlag._7,

        Delete = CBinaryFlag._8,

        Where = CBinaryFlag._9,
        GroupBy = CBinaryFlag._10,
        Having = CBinaryFlag._11,    
        OrderBy = CBinaryFlag._12,
        Limit = CBinaryFlag._13,
        Offset = CBinaryFlag._14,
        On = CBinaryFlag._15,
        And = CBinaryFlag._16,
        Or = CBinaryFlag._17,
        Left = CBinaryFlag._18,
        Right = CBinaryFlag._19,
        Inner = CBinaryFlag._20,
        Outer = CBinaryFlag._21,

        OpenBlock = CBinaryFlag._22,
        CloseBlock = CBinaryFlag._23,

        Rows = CBinaryFlag._24,
        Fetch = CBinaryFlag._25,
        Next = CBinaryFlag._26,

        Count = CBinaryFlag._27,
        As = CBinaryFlag._28,

        Match = CBinaryFlag._29
    }
}
