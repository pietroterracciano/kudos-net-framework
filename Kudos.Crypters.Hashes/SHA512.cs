using Kudos.Crypters.Hashes.Enums;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA512 : AHash
    {
        public SHA512() : base(EHashAlgorithm.SHA512) { }
    }
}