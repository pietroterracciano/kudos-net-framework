using System;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraDeclaringTypeDescriptor
    {
        #region DeclaringType

        Type? DeclaringType { get; }
        Boolean HasDeclaringType { get; }

        #endregion
    }
}
