using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.MACs.Models;
using Kudos.Crypters.Models.SALTs;
using System;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes.MACs
{
    public abstract class AHMAC : AHash<KeyedHashAlgorithm, HMACCrypterPreferencesMode<SALTPreferencesModel>, SALTPreferencesModel>
    {
        public AHMAC(EHashAlgorithm eHashAlgorithm) : base(eHashAlgorithm) { }

        protected override void OnBeforeCompute()
        {
            if (Algorithm == null)
                return;

            byte[] 
                aKAKey = Algorithm.Key,
                aPKey = Preferences.GetKey();

            if (aKAKey != aPKey)
                try { Algorithm.Key = aPKey; } catch { }
        }
    }
}