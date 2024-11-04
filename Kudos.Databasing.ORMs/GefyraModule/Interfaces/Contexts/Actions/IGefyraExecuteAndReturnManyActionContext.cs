using System;
using System.Threading.Tasks;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
	public interface IGefyraExecuteAndReturnManyActionContext<R>
	{
        public Task<R[]?> ExecuteAndReturnManyAsync();
    }
}

