using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextJoinClausole<ModelType, ExecuteReturnType>
    {
        IGefyraContextJoinClausoleBuilder<ModelType, ExecuteReturnType> Join<JoinModelType>(EGefyraJoin eType, Expression<Func<ModelType, JoinModelType, Boolean>> oExpression);
    }
}
