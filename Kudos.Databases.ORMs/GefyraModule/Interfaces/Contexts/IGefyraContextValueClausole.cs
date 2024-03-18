using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextValueClausole<ModelType, ExecuteReturnType>
    {
        IGefyraContextValueClausoleBuilder<ModelType, ExecuteReturnType> Value(ModelType oModel);
        IGefyraContextValueClausoleBuilder<ModelType, ExecuteReturnType> Value(Action<ModelType> oAction);
    }
}