using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandHavingClausole
    {
        public IGefyraCommandHavingClausoleBuilder Having();
    }
}
