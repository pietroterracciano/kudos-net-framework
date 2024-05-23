using System;
namespace Kudos.Databases.Descriptors
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

