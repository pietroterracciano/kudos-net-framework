using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Models;
using Kudos.Utils;
using System;
using System.Security.Cryptography;

namespace Kudos.Crypters.Hashes.Handlers
{
    public abstract class AHandler : IDisposable
    {
        private HashAlgorithm
            _oHashAlgorithm;

        public HashingPreferencesModel HashingPreferences
        {
            get;
            private set;
        }

        public AHandler(EHashAlgorithm eHashAlgorithm)
        {
            switch(eHashAlgorithm)
            {
                default:
                    try { _oHashAlgorithm = SHA1.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA256:
                    try { _oHashAlgorithm = SHA256.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA384:
                    try { _oHashAlgorithm = SHA384.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA512:
                    try { _oHashAlgorithm = SHA512.Create(); } catch { }
                    break;
                case EHashAlgorithm.MD5:
                    try { _oHashAlgorithm = MD5.Create(); } catch { }
                    break;
            }

            HashingPreferences = new HashingPreferencesModel();
        }

        private Boolean CanHash()
        {
            return
                _oHashAlgorithm != null
                && HashingPreferences.Encoding != null;
        }

        public Boolean Verify(Object o2Hash, String sHashed)
        {
            return
                !String.IsNullOrWhiteSpace(sHashed)
                    ? sHashed.Equals(Hash(o2Hash))
                    : false;
        }

        public Boolean Verify(String s2Hash, String sHashed)
        {
            return
                !String.IsNullOrWhiteSpace(sHashed)
                    ? sHashed.Equals(Hash(s2Hash))
                    : false;
        }

        public String Hash(Object oInput)
        {
            if (
                !CanHash()
                || oInput == null
            )
                return null;

            String sInput = JSONUtils.Serialize(oInput);

            if (sInput == null)
                return null;

            Byte[] aInput;
            try { aInput = HashingPreferences.Encoding.GetBytes(sInput); } catch { return null; }

            Byte[] aOutput;
            try { aOutput = _oHashAlgorithm.ComputeHash(aInput); } catch { return null; }

            if (aOutput == null)
                return null;

            try { return StringUtils.ToBase64(aOutput); } catch { return null; }
        }


        public void Dispose()
        {
            if (_oHashAlgorithm != null)
                try
                {
                    _oHashAlgorithm.Dispose();
                }
                catch
                {

                }
        }
    }
}