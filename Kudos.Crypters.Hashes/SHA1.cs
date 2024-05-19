using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes
{
    public sealed class SHA1 : AStandardHash
    {
        public SHA1() : base(EHashAlgorithm.SHA1) { }
    }
}