using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextDeleteClausole<ModelType>
    {
        IGefyraContextDeleteClausoleBuilder<ModelType, Boolean> Delete(Expression<Func<ModelType, Boolean>>? oExpression);
        IGefyraContextDeleteClausoleBuilder<ModelType, Boolean> Delete(ModelType? oModel);
    }
}
