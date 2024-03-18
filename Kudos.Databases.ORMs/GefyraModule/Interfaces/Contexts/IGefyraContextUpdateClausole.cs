using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextUpdateClausole<ModelType>
    {
        IGefyraContextUpdateClausoleBuilder<ModelType, ModelType> Update(Action<ModelType>? oAction);
        IGefyraContextUpdateClausoleBuilder<ModelType, ModelType> Update(ModelType? oModel);
    }
}
