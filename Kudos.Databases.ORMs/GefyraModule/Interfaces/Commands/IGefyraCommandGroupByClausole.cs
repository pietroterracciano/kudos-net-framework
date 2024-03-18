using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandGroupByClausole
    {
        public IGefyraCommandGroupByClausoleBuilder GroupBy(GefyraColumn mColumn, params GefyraColumn[] aColumns);
    }
}