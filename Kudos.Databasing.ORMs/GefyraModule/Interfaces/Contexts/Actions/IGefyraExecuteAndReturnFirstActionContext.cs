using System;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
    public interface IGefyraExecuteAndReturnFirstActionContext<R>
    {
        public Task<R?> ExecuteAndReturnFirstAsync();
    }
}

