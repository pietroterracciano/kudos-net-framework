using System;
using System.Linq.Expressions;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface
        IGefyraComplexSelectContext<R>
    :
        IGefyraWhereActionContext<R>,
        IGefyraJoinActionContext<R>
    {
    }
}