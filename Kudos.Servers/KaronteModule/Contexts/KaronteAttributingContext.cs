using System;
using Kudos.Utils;
using Microsoft.Extensions.Primitives;

namespace Kudos.Servers.KaronteModule.Contexts
{
    public sealed class KaronteAttributingContext : AKaronteChildContext
    {
        private Attribute? _att;
        public Boolean HasAttribute;

        internal KaronteAttributingContext(ref Attribute? att, ref KaronteContext kc) : base(ref kc)
        {
            HasAttribute = (_att = att) != null;
        }

        public T? GetAttribute<T>()
        {
            return
                HasAttribute
                    ? ObjectUtils.Cast<T>(_att)
                    : default(T);
        }

        public T? RequestAttribute<T>()
        {
            T? t = GetAttribute<T>();
            if (t == null) throw new InvalidOperationException();
            return t;
        }
    }
}

