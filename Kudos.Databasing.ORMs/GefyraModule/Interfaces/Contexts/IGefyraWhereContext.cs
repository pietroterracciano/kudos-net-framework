using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface
        IGefyraWhereContext<R>
    :
        IGefyraLimitActionContext<R>,
        IGefyraExecuteAndReturnContext<R>
    {
    }
}

