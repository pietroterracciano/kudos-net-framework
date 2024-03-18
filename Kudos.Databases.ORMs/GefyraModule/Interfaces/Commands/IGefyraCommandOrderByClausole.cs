using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandOrderByClausole
    {
        public IGefyraCommandOrderByClausoleBuilder OrderBy(GefyraColumn mColumn, EGefyraOrdering eOrdering);
    }
}
