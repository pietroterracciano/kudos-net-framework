using Kudos.Crypters.Models;
using Kudos.Crypters.Models.SALTs;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters.Hashes.MACs.Models
{
    public class HMACCrypterPreferencesMode<SALTPreferencesModelType> : CrypterPreferencesModel<SALTPreferencesModelType>
        where SALTPreferencesModelType : SALTPreferencesModel, new()
    {
        private byte[] _aKey;

        public byte[] GetKey()
        {
            return _aKey;
        }

        public void SetKey(byte[] aKey)
        {
            _aKey = aKey;
        }

        public void SetKey(String sKey)
        {
            _aKey = BytesUtils.From(sKey, Encoding);
        }
    }
}