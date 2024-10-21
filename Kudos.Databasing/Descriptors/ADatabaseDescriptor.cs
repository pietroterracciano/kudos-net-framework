using System;
using Kudos.Types;

namespace Kudos.Databasing.Descriptors
{
	public abstract class ADatabaseDescriptor
	{
		public readonly String HashKey;

		protected ADatabaseDescriptor(ref String shc)
		{
			HashKey = shc;
		}
	}
}

