using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Models;
using Kudos.Crypters.Models.SALTs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes
{
    public abstract class AStandardHash : AHash<HashAlgorithm, CrypterPreferencesModel<SALTPreferencesModel>, SALTPreferencesModel>
    {
        protected AStandardHash(EHashAlgorithm eHashAlgorithm) : base(eHashAlgorithm) { }
    }
}