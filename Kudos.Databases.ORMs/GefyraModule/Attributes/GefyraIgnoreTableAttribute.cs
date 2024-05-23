using System;
namespace Kudos.Databases.ORMs.GefyraModule.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class GefyraIgnoreTableAttribute : Attribute { }
}