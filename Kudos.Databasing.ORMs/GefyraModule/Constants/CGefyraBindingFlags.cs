using System;
using System.Reflection;

namespace Kudos.Databasing.ORMs.GefyraModule.Constants
{
	internal static class CGefyraBindingFlags
	{
        internal static readonly BindingFlags
            OnGetMembers =
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.SetProperty
                | BindingFlags.SetField
                | BindingFlags.DeclaredOnly;
    }

    /*
     * internal static readonly BindingFlags
            BFOnConstructor =
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance,
            BindingFlagsOnGetMembers =
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.SetProperty
                | BindingFlags.SetField
                | BindingFlags.DeclaredOnly;

        internal static readonly MemberTypes
            MTOnGetMembers =
                MemberTypes.Field
                | MemberTypes.Property;*/
}

