using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandOnComplexClausole<BuilderType>
    {
        public BuilderType On(GefyraColumn mColumn, Object oValue);
        public BuilderType On(GefyraColumn mColumn, EGefyraComparator eComparator, Object oValue);
        public BuilderType On(GefyraColumn mColumn0, GefyraColumn mColumn1);
        public BuilderType On(GefyraColumn mColumn0, EGefyraComparator eComparator, GefyraColumn mColumn1);
    }
}
