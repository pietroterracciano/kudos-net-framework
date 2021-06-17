using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Handlers;

namespace Kudos.Crypters.Hashes.SHA512NS
{
    public sealed class SHA512 : AHandler
    {
        public SHA512() : base(EHashAlgorithm.SHA512) { }
    }
}