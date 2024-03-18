using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextSelectClausole<ModelType>
    {
        IGefyraContextSelectClausoleBuilder<ModelType, UInt64> Count();
        IGefyraContextWhereClausoleBuilder<ModelType, UInt64> Count(Expression<Func<ModelType, Boolean>> oExpression);
        IGefyraContextSelectClausoleBuilder<ModelType, ModelType[]> Select();
        IGefyraContextWhereClausoleBuilder<ModelType, ModelType[]> Select(Expression<Func<ModelType, Boolean>> oExpression);
    }
}