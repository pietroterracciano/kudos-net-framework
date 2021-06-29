using Kudos.Crypters.Hashes.Enums;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA1 : AHash
    {
        public SHA1() : base(EHashAlgorithm.SHA1) { }
    }
}