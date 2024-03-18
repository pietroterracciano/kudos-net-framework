using Kudos.Constants;
namespace Kudos.Databases.ORMs.GefyraModule.Enums
{
    [Flags]
	public enum EGefyraComparator
	{
		Equal,
		NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
		Like,
		NotLike,
		In,
		NotIn
	}
}