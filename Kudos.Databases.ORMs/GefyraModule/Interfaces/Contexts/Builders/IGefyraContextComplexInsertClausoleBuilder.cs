using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders
{
    public interface
        IGefyraContextComplexInsertClausoleBuilder<ModelType, ExecuteReturnType>
    :
        IGefyraContextValueClausole<ModelType, ExecuteReturnType>
    {
    }
}