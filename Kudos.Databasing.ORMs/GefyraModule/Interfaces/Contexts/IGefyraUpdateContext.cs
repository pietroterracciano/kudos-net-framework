using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface IGefyraUpdateContext<T>
	{
        IGefyraExecuteAndReturnContext<T> Update(T? o);
        IGefyraExecuteAndReturnContext<T> Update(Action<T?> o);
    }
}

