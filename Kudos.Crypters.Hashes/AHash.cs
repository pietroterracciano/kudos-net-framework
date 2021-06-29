using Kudos.Crypters.Hashes.Enums;
using Kudos.Crypters.Models;
using Kudos.Crypters.Models.SALTs;
using Kudos.Utils;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Crypters.Hashes
{
    public abstract class AHash : ACrypter<CrypterPreferencesModel<SALTPreferencesModel>, SALTPreferencesModel>
    {
        private const String
            SEPARATOR = "$";

        private HashAlgorithm
            _oHashAlgorithm;

        protected AHash(EHashAlgorithm eHashAlgorithm)
        {
            switch (eHashAlgorithm)
            {
                default:
                    try { _oHashAlgorithm = System.Security.Cryptography.SHA1.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA256:
                    try { _oHashAlgorithm = System.Security.Cryptography.SHA256.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA384:
                    try { _oHashAlgorithm = System.Security.Cryptography.SHA384.Create(); } catch { }
                    break;
                case EHashAlgorithm.SHA512:
                    try { _oHashAlgorithm = System.Security.Cryptography.SHA512.Create(); } catch { }
                    break;
                case EHashAlgorithm.MD5:
                    try { _oHashAlgorithm = System.Security.Cryptography.MD5.Create(); } catch { }
                    break;
            }
        }

        public Boolean Verify(Object o2Hash, String sHash)
        {
            if (sHash == null)
                return false;

            Boolean
                bContainsSALT = sHash.Contains(SEPARATOR);

            String
                sSALT;

            if (bContainsSALT)
            {
                String[]
                    aIPieces = sHash.Split(SEPARATOR);

                if (ArrayUtils.IsValidIndex(aIPieces, 1))
                {
                    sSALT = aIPieces[0];
                    sHash = aIPieces[1];
                }
                else if (ArrayUtils.IsValidIndex(aIPieces, 0))
                    sSALT = aIPieces[0];
                else
                    sSALT = null;
            }
            else
                sSALT = null;

            Byte[] aOHashed;
            Compute(ref o2Hash, ref sSALT, out aOHashed);

            if (aOHashed == null)
                return false;

            Byte[] aHash;
            External_ToBytes(ref sHash, out aHash);

            if (aHash == null)
                return false;

            return aOHashed.SequenceEqual(aHash);
        }

        public String Compute(Object oInput)
        {
            Byte[] aSALT;
            GenerateSALT(out aSALT);

            Byte[] aOutput;
            Compute(ref oInput, ref aSALT, out aOutput);

            if (aOutput == null)
                return null;

            StringBuilder
                oStringBuilder = new StringBuilder();

            if (aSALT != null)
            {
                String sSALT;
                Internal_ToString(ref aSALT, out sSALT);

                oStringBuilder
                    .Append(sSALT)
                    .Append(SEPARATOR);
            }

            if (aOutput != null)
            {
                String sOutput;
                External_ToString(ref aOutput, out sOutput);

                oStringBuilder.Append(sOutput);
            }

            return oStringBuilder.ToString();
        }

        private void Compute(ref Object oInput, ref String sSALT, out Byte[] aOutput)
        {
            Byte[] aSALT;
            Internal_ToBytes(ref sSALT, out aSALT);
            Compute(ref oInput, ref aSALT, out aOutput);
        }

        private void Compute(ref Object oInput, ref Byte[] aSALT, out Byte[] aOutput)
        {
            Byte[] aInput;
            String sInput = JSONUtils.Serialize(oInput);
            Internal_ToBytes(ref sInput, out aInput);
            Compute(ref aInput, ref aSALT, out aOutput);
        }

        private void Compute(ref Byte[] aInput, ref Byte[] aSALT, out Byte[] aOutput)
        {
            if( _oHashAlgorithm != null && aInput != null)
                try { aOutput =  _oHashAlgorithm.ComputeHash(BytesUtils.Prepend(aSALT, aInput)); return; } catch { }

            aOutput = null;
        }

        public override void Dispose()
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