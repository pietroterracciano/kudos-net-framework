namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Commands
{
    public interface IGefyraCommandAndOrClausole<BuilderType>
    {
        public BuilderType And();
        public BuilderType Or();
    }
}
