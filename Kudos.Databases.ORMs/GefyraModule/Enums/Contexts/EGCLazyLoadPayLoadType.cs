using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Enums.Contexts
{
    [Flags]
    internal enum EGCLazyLoadPayLoadType
    {
        None,
        Member,
        Object
    }
}