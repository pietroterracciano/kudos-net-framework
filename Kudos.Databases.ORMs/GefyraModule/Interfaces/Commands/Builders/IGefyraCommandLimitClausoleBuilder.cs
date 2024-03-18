using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandLimitClausoleBuilder
    :
        IGefyraCommandOffsetClausole,
        IGefyraCommandBuild
    {
    }
}
