namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandAndOrClausole<BuilderType>
    {
        public BuilderType And();
        public BuilderType Or();
    }
}
