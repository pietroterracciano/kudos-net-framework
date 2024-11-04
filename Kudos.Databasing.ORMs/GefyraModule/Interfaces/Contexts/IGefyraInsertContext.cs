using System;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface IGefyraInsertContext<T>
	{
        IGefyraExecuteAndReturnContext<T> Insert(T? o);
        IGefyraExecuteAndReturnContext<T> Insert(Action<T?> o);
    }
}