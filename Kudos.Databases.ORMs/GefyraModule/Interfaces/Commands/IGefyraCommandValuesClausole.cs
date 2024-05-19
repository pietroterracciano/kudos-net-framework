using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandValuesClausole
    {
        public IGefyraCommandValuesClausoleBuilder Values(Object? oObject, params Object[]? aObjects);
    }
}
