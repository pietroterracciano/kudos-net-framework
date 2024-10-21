using System;
namespace Kudos.Interfaces
{
	public interface IShallowCopiable<T>
	{
		public T ShallowCopy();
	}
}