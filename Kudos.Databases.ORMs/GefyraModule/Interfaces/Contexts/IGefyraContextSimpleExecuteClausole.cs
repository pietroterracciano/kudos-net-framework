using Kudos.Databases.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextSimpleExecuteClausole<ExecuteReturnType>
    {
        ExecuteReturnType Execute();
        ExecuteReturnType Execute(out ADBCommandResult oDBCommandResult);
    }
}