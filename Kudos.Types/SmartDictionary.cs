using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kudos.Enums;

namespace Kudos.Types
{
    public sealed class SmartDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey k]
        {
            get
            {
                TValue v;
                TryGetValue(k, out v);
                return v;
            }
            set
            {
                if (k != null) 
                    base[k] = value;
            }
        }

        public new Boolean TryAdd(TKey k, TValue v)
        {
            return
                k != null
                && base.TryAdd(k, v);
        }

        public Boolean TryGetValue(TKey k, out TValue v, out EDictionaryTryGetValueResult r)
        {
            if (k == null)
            {
                v = default(TValue);
                r = EDictionaryTryGetValueResult.NullKey;
                return false;
            }

            Boolean b = base.TryGetValue(k, out v);
            r = b ? EDictionaryTryGetValueResult.KeyExists : EDictionaryTryGetValueResult.KeyNotExists;
            return b;
        }

        public new Boolean TryGetValue(TKey k, out TValue v)
        {
            EDictionaryTryGetValueResult r;
            return TryGetValue(k, out v, out r);
        }

        public new Boolean ContainsKey(TKey k)
        {
            TValue v;
            return TryGetValue(k, out v);
        }
    }
}