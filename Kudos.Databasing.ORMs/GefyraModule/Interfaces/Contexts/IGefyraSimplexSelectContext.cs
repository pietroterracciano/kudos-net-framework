using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface
        IGefyraSimplexSelectContext<R>
    :
        IGefyraLimitActionContext<R>,
        IGefyraExecuteAndReturnContext<R>
    {
    }
}

