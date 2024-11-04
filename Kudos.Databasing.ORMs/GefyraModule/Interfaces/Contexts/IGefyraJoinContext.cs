using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface
		IGefyraJoinContext<R>
	:
		IGefyraJoinActionContext<R>,
		IGefyraWhereActionContext<R>,
        IGefyraLimitActionContext<R>,
        IGefyraExecuteAndReturnContext<R>
    {
	}
}

