using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes
{
    public class MD5 : AStandardHash
    {
        public MD5() : base(EHashAlgorithm.MD5) { }
    }
}