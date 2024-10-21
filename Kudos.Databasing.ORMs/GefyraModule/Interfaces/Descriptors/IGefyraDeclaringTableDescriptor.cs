using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraDeclaringTableDescriptor
    {
        #region DeclaringTable

        IGefyraTable DeclaringTable { get; }

        #endregion
    }
}