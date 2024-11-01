using System;
using System.Threading.Tasks;
using Kudos.Databasing.Results;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts
{
	public interface IGefyraExecuteContext<T>
	{
        public Task<T?> ExecuteAsync();
    }
}

