using Kudos.Constants;
namespace Kudos.Databases.ORMs.GefyraModule.Enums
{
    [Flags]
	public enum EGefyraCompare
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