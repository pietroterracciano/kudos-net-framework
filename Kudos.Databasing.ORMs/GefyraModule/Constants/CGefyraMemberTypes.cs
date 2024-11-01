using System;
using System.Reflection;

namespace Kudos.Databasing.ORMs.GefyraModule.Constants
{
	internal static class CGefyraMemberTypes
	{
		internal static readonly MemberTypes
			Property = MemberTypes.Property,
			Field = MemberTypes.Field,
			FieldProperty = Field | Property;
	}
}