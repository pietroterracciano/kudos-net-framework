using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Enums
{
    [Flags]
    public enum EGefyraActuator
    {
        None,
        Incremental,
        Decremental
    }
}
