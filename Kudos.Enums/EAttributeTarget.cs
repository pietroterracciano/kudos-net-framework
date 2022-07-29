using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Enums
{
    [Flags]
    public enum EAttributeTarget
    {
        Class = CBinaryFlag.__,
        Member = CBinaryFlag._0
    }
}