using Kudos.Crypters.Hashes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes.MACs
{
    public class HMACSHA256 : AHMAC
    {
        public HMACSHA256() : base(EHashAlgorithm.HMACSHA256) { }
    }
}