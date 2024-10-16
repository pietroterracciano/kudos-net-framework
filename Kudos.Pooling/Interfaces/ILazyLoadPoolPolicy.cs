namespace Kudos.Pooling.Policies
{
	public interface ILazyLoadPoolPolicy<T>
	{
        public T OnCreateObject();
		public bool OnReturnObject(T? o);
	}
}

