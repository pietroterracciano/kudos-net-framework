using Kudos.Databases.ORMs.GefyraModule.Interfaces.Clausoles;
using Kudos.Databases.ORMs.GefyraModule.Types;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders
{
    public interface
        IGefyraBuilder
    :
        IGefyraInsertClausole,  //C
        IGefyraSelectClausole,  //R
        IGefyraUpdateClausole,  //U
        IGefyraDeleteClausole   //D
    {
    }
}
