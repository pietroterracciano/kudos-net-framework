using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandSelectClausole
    {
        public IGefyraCommandSelectClausoleBuilder Select(params GefyraColumn[] aColumns);
        public IGefyraCommandSelectClausoleBuilder Count();
    }
}
