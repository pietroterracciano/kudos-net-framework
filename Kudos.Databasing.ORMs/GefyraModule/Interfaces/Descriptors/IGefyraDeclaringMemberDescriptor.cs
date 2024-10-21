using System;
using System.Reflection;

namespace Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors
{
    public interface IGefyraDeclaringMemberDescriptor
    {
        #region DeclaringMember

        MemberInfo? DeclaringMember { get; }
        Boolean HasDeclaringMember { get; }

        #endregion
    }
}