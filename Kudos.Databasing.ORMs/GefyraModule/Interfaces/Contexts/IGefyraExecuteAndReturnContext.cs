using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface
		IGefyraExecuteAndReturnContext<R>
	:
		IGefyraExecuteAndReturnManyActionContext<R>,
		IGefyraExecuteAndReturnFirstActionContext<R>,
        IGefyraExecuteAndReturnLastActionContext<R>
    {
	}
}

