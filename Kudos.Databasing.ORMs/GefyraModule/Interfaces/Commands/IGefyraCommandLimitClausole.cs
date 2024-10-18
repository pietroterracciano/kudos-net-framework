using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandLimitClausole
    {
        public IGefyraCommandLimitClausoleBuilder Limit(int iRows2Read);
    }
}
