using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandJoinClausole
    {
        public IGefyraCommandJoinClausoleBuilder Join(EGefyraJoin eType, GefyraTable mTable);
    }
}
