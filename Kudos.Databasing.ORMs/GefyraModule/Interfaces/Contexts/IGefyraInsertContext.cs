using System;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface IGefyraInsertContext<T>
	{
        IGefyraExecuteContext<T> Insert(T? o);
        IGefyraExecuteContext<T> Insert(Action<T?> o);
    }
}