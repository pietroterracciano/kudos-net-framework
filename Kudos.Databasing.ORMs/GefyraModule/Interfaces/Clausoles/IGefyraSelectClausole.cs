
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;


namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface IGefyraSelectClausole
    {
        public IGefyraSelectClausoleBuilder Select(params IGefyraColumn?[]? aColumns);
    }
}
