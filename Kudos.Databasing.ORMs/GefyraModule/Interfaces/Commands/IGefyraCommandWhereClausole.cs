using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface
        IGefyraCommandWhereClausole
    :
        IGefyraCommandWhereSimpleClausole,
        IGefyraCommandWhereComplexClausole
    {
    }
}
