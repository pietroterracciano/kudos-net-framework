using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Handlers;

namespace Kudos.Crypters.Hashes.SHA256NS
{
    public sealed class SHA256 : AHandler
    {
        public SHA256() : base(EHashAlgorithm.SHA256) { }
    }
}