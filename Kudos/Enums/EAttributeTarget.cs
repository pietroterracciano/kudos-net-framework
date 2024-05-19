using Kudos.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.Enums
{
    [Flags]
    public enum EAttributeTarget
    {
        Class = CBinaryFlag._0,
        Member = CBinaryFlag._1
    }
}