using System;
using System.Text.RegularExpressions;

namespace Kudos.Constants
{
	public static class CRegex
	{
		public static readonly Regex
			Spaces1toN;

		static CRegex()
		{
			Spaces1toN = new Regex(@"\s{1,}");
		}
	}
}

