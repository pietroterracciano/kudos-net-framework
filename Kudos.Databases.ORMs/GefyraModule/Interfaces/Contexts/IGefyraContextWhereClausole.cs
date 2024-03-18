using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextWhereClausole<ModelType, ExecuteReturnType>
    {
        IGefyraContextWhereClausoleBuilder<ModelType, ExecuteReturnType> Where(Expression<Func<ModelType, Boolean>> oExpression);
    }
}
