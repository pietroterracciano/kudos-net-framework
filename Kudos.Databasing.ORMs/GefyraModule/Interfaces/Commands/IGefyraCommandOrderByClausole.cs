using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandOrderByClausole
    {
        public IGefyraCommandOrderByClausoleBuilder OrderBy(GefyraColumn mColumn, EGefyraOrdering eOrdering);
    }
}
