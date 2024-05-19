using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface IGefyraFromClausole
    {
        public IGefyraFromClausoleBuilder From(IGefyraTable? gt);
    }
}
