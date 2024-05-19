using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandFromClausole
    {
        public IGefyraCommandFromClausoleBuilder From(GefyraTable oTable);
    }
}
