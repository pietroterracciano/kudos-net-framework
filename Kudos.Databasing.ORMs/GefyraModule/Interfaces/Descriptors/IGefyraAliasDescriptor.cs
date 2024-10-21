using System;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraAliasDescriptor
    {
        #region Alias

        String? Alias { get; }
        Boolean HasAlias { get; }
    
        #endregion
    }
}
