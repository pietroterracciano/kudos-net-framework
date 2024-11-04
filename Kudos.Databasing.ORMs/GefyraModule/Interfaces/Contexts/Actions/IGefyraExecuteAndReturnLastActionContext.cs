using System;
using System.Threading.Tasks;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts.Actions
{
    public interface IGefyraExecuteAndReturnLastActionContext<R>
    {
        public Task<R?> ExecuteAndReturnLastAsync();
    }
}