
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface IGefyraUpdateClausole
    {
        public IGefyraUpdateClausoleBuilder Update(IGefyraTable? mTable);
    }
}
