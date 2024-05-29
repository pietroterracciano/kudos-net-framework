
using System;
using Kudos.Utils;

namespace Kudos.Servers.KaronteModule.Descriptors.Authorizatings
{
	public class KaronteAuthorizationDescriptor
	{
		public readonly String? Code;
		public readonly Boolean HasCode;
		internal readonly Enum Type;

		internal KaronteAuthorizationDescriptor(String? s, Enum t)
		{
			HasCode = (Code = s) != null;
			Type = t;
		}

		public T? GetType<T>()
		{
			return ObjectUtils.Cast<T>(Type);
		}

		public T RequireType<T>()
		{
			T? t = GetType<T>();
			if (t == null) throw new InvalidOperationException();
			return t;
		}
	}
}