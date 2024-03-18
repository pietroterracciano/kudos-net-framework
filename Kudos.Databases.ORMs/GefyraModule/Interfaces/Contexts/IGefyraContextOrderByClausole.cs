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
    public interface IGefyraContextOrderByClausole<ModelType, ExecuteReturnType>
    {
        public IGefyraContextOrderByClausoleBuilder<ModelType, ExecuteReturnType> OrderBy(Expression<Func<ModelType, Object>> oExpression, EGefyraOrdering eOrdering);
    }
}
