using Kudos.Crypters.Hashes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes.MACs
{
    public class HMACSHA1 : AHMAC
    {
        public HMACSHA1() : base(EHashAlgorithm.HMACSHA1) { }
    }
}
