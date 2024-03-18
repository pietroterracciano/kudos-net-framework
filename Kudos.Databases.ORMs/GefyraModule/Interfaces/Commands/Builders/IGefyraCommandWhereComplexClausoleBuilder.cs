using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders
{
    public interface
        IGefyraCommandWhereComplexClausoleBuilder
    :
        IGefyraCommandCloseBlockClausole<IGefyraCommandWhereCloseBlockClausoleBuilder>,
        IGefyraCommandAndOrClausole<IGefyraCommandWhereAndOrClausoleBuilder>,
        IGefyraCommandGroupByClausole,
        IGefyraCommandHavingClausole,
        IGefyraCommandOrderByClausole,
        IGefyraCommandOffsetClausole,
        IGefyraCommandLimitClausole,
        IGefyraCommandBuild
    {
    }
}
