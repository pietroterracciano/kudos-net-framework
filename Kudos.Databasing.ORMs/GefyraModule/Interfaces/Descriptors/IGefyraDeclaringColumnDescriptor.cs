using System;
using System.Reflection;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraDeclaringColumnDescriptor
    {
        #region DeclaringColumn

        IGefyraColumn DeclaringColumn { get; }

        #endregion
    }
}

