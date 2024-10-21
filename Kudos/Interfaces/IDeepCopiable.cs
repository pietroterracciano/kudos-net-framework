using System;
namespace Kudos.Interfaces
{
	public interface IDeepCopiable<T>
	{
		public T DeepCopy();
	}
}

