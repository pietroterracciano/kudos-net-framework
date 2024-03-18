using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders
{
    public interface
        IGefyraContextSelectClausoleBuilder<ModelType, ExecuteReturnType>
    :
        IGefyraContextWhereClausole<ModelType, ExecuteReturnType>,
        IGefyraContextJoinClausole<ModelType, ExecuteReturnType>,
        IGefyraContextOffsetClausole<ModelType, ExecuteReturnType>,
        IGefyraContextLimitClausole<ModelType, ExecuteReturnType>,
        IGefyraContextSimpleExecuteClausole<ExecuteReturnType>
    {
    }
}
