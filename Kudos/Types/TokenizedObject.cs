using Kudos.Interfaces;
using Kudos.Reflection.Utils;
using System;
using System.Text.Json.Serialization;

namespace Kudos.Types
{
    public class
        TokenizedObject
    :
        ITokenizedObject
    {
        private static readonly Object _lck;
        private static Int32 __i;

        static TokenizedObject()
        {
            _lck = new object();
            __i = 0;
        }

        [JsonIgnore]
        public Int32 Token { get; private set; }
        
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