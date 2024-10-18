
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface IGefyraUpdateClausole
    {
        public IGefyraUpdateClausoleBuilder Update(IGefyraTable? mTable);
    }
}
