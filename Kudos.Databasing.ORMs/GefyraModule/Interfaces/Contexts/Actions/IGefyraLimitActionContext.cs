using System;
using System.Linq.Expressions;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
	public interface IGefyraLimitActionContext<R>
	{
        IGefyraExecuteAndReturnContext<R> Limit(UInt32 i);
    }
}

