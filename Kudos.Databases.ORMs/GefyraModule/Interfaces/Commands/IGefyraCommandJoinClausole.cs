using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandJoinClausole
    {
        public IGefyraCommandJoinClausoleBuilder Join(EGefyraJoin eType, GefyraTable mTable);
    }
}
