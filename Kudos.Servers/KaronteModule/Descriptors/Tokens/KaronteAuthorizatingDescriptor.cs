
using System;
using Kudos.Utils;

namespace Kudos.Servers.KaronteModule.Descriptors.Tokens
{
	public class KaronteAuthorizatingDescriptor
	{
		public readonly String? Code;
		public readonly Boolean HasCode;
		internal readonly Enum Type;

		internal KaronteAuthorizatingDescriptor(String? s, Enum t)
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