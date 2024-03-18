using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders
{
    public interface 
        IGefyraContextOrderByClausoleBuilder<ModelType, ExecuteReturnType>
    :
        IGefyraContextOrderByClausole<ModelType, ExecuteReturnType>,
        IGefyraContextLimitClausole<ModelType, ExecuteReturnType>,
        IGefyraContextSimpleExecuteClausole<ExecuteReturnType>
    {
    }
}
