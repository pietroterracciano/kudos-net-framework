using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Constants
{
    internal static class CGefyraFlags
    {
        internal static readonly BindingFlags
            BFOnConstructor =
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance,
            BFOnGetMembers =
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.SetProperty
                | BindingFlags.SetField
                | BindingFlags.DeclaredOnly;

        internal static readonly MemberTypes
            MTOnGetMembers =
                MemberTypes.Field
                | MemberTypes.Property;
    }
}
