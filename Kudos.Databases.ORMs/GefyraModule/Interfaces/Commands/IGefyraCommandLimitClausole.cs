using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandLimitClausole
    {
        public IGefyraCommandLimitClausoleBuilder Limit(int iRows2Read);
    }
}
