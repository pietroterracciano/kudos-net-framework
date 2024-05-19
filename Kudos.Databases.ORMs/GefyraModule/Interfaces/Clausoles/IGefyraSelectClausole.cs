
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface IGefyraSelectClausole
    {
        public IGefyraSelectClausoleBuilder Select(params IGefyraColumn?[]? aColumns);
    }
}
