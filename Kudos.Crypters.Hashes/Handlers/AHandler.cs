using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Hashes.Models;
using Kudos.Crypters.Hashes.Utils;
using Kudos.Crypters.Utils;
using Kudos.Enums;
using Kudos.Utils;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Crypters.Hashes.Handlers
{
    public abstract class AHandler : IDisposable
    {
        private static readonly String
            SEPARATOR = nameof(AHandler) +"."+nameof(SEPARATOR);

        private HashAlgorithm
            _oHashAlgorithm;

        public HashingPreferencesModel Preferences
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

            Preferences = new HashingPreferencesModel();
        }

        private Boolean CanHash()
        {
            return
                _oHashAlgorithm != null
                && Preferences.Encoding != null;
        }

        public Boolean Verify(Object o2Hash, String sHashed)
        {
            if (sHashed == null)
                return false;

            Byte[]
                aObjectHashed =
                    Hash(
                        o2Hash,
                        CHashUtils.ExtrapolateSALT(sHashed)
                    );

            if (aObjectHashed == null)
                return false;

            return aObjectHashed.SequenceEqual(BytesUtils.From(sHashed, Preferences.Encoding));
        }

        public String Hash(Object oInput)
        {
            Byte[]
                aSALT;

            if (Preferences.SALTPreferences.Use)
                aSALT = CrypterUtils.GenerateSALT(Preferences.SALTPreferences);
            else
                aSALT = null;

            Byte[]
                aOutput = 
                    Hash(
                        oInput, 
                        aSALT
                    );

            if (aOutput == null)
                return null;

            return
                CHashUtils.AppendSALT(
                    Preferences.BinaryEncoding == EBinaryEncoding.Base64
                        ? StringUtils.ToBase64(aOutput)
                        : StringUtils.ToBase16(aOutput),
                    StringUtils.From(aSALT)
                );
        }

        private Byte[] Hash(Object oInput, String sSALT)
        {
            return
                Hash(
                    oInput,
                    BytesUtils.From(sSALT, Preferences.Encoding)
                );
        }

        private Byte[] Hash(Object oInput, Byte[] aSALT)
        {
            return Hash(BytesUtils.From(JSONUtils.Serialize(oInput)), aSALT);
        }

        private Byte[] Hash(Byte[] aInput, Byte[] aSALT = null)
        {
            if (!CanHash() || aInput == null)
                return null;

            try { return _oHashAlgorithm.ComputeHash(CHashUtils.AppendSALT(aInput, aSALT)); } catch { return null; }
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