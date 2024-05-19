using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA384 : AStandardHash
    {
        public SHA384() : base(EHashAlgorithm.SHA384) { }
    }
}