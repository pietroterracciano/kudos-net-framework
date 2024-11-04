using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface IGefyraDeleteContext<T>
	{
        IGefyraExecuteAndReturnContext<T> Delete(T? o);
        IGefyraExecuteAndReturnContext<T> Delete(Action<T?> o);
    }
}