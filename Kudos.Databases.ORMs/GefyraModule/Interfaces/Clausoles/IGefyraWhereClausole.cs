using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Types;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles
{
    public interface 
        IGefyraWhereClausole
    {
        IGefyraWhereClausoleBuilder Where(Action<IGefyraWhereActionClausoleBuilder>? gc);
    }
}
