using Kudos.Reflection.Utils;
using System;

namespace Kudos.Types
{
    public class TokenizedObject 
    {
        private static readonly Object _lck;
        private static Int32 __i;

        static TokenizedObject()
        {
            _lck = new object();
            __i = 0;
        }

        public readonly Int32 Token;
        
        public TokenizedObject()
        {
            lock(_lck)
            {
                Token = ++__i;
                ReflectionUtils.RegisterTokenizedObject(this);
            }
        }

        public override bool Equals(object? o)
        {
            if (o == this) return true;

            TokenizedObject?
                o0 = o as TokenizedObject;

            return
                (
                    o0 != null
                    && o0.Token == Token
                )
                || base.Equals(o);
        }
    }
}