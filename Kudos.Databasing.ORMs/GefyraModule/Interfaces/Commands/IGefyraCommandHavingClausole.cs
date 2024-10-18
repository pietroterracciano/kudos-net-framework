using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandHavingClausole
    {
        public IGefyraCommandHavingClausoleBuilder Having();
    }
}
