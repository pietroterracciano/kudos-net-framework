using System;
using Kudos.Constants;

namespace Kudos.Types.TimeStamps.Enums
{
	[Flags]
	public enum ETimeStampKind
	{
		Universal = CBinaryFlag._0,
		Local = CBinaryFlag._1,
		Unspecified = CBinaryFlag._2
	}
}