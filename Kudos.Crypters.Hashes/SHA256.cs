using Kudos.Crypters.Hashes.Enums;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA256 : AHash
    {
        public SHA256() : base(EHashAlgorithm.SHA256) { }
    }
}