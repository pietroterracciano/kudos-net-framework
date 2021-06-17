using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Handlers;

namespace Kudos.Crypters.Hashes.SHA1NS
{
    public sealed class SHA1 : AHandler
    {
        public SHA1() : base(EHashAlgorithm.SHA1) { }
    }
}