using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA256 : AStandardHash
    {
        public SHA256() : base(EHashAlgorithm.SHA256) { }
    }
}