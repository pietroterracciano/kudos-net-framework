using System;
using Kudos.Utils;

namespace Kudos.Servers.KaronteModule.Descriptors.Authenticatings
{
    public class KaronteAuthenticationDescriptor
    {
        internal readonly Enum Type;

        internal KaronteAuthenticationDescriptor(Enum t)
        {
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