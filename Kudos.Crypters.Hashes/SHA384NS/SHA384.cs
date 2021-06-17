using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Handlers;

namespace Kudos.Crypters.Hashes.SHA384NS
{
    public sealed class SHA384 : AHandler
    {
        public SHA384() : base(EHashAlgorithm.SHA384) { }
    }
}