using Kudos.Constants;

namespace Kudos.Databases.ORMs.GefyraModule.Enums
{
    [Flags]
	public enum EGefyraJoin
	{
		Left,
        Right,
        Inner,
        Full
    }
}