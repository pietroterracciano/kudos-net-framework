using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes.MD5NS
{
    public class MD5 : AHandler
    {
        public MD5() : base(EHashAlgorithm.MD5) { }
    }
}
