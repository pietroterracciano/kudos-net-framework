using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA512 : AStandardHash
    {
        public SHA512() : base(EHashAlgorithm.SHA512) { }
    }
}