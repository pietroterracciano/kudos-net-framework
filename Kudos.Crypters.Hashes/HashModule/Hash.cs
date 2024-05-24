using System;
using Kudos.Crypters.Hashes.Enums;
using System.Security.Cryptography;
using Kudos.Crypters.Hashes.HashModule.Builders;
using Kudos.Crypters.Hashes.HashModule.Descriptors;
using Kudos.Crypters.KryptoModule;
using Kudos.Utils;
using Kudos.Utils.Collections;
using System.Linq;
using System.Text;

namespace Kudos.Crypters.Hashes.HashModule
{
	public class
        Hash
	:
		AKrypto<HashDescriptor>
	
	{
        #region ... static ...

        public static HashBuilder RequestBuilder()
        {
            return new HashBuilder();
        }

        #endregion

        public Hash(ref HashDescriptor hd) : base(ref hd)
        {
            switch (hd.
            {
                case EHashAlgorithm.MD5:
                    try { Algorithm = System.Security.Cryptography.MD5.Create() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.SHA1:
                    try { Algorithm = System.Security.Cryptography.SHA1.Create() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.SHA256:
                    try { Algorithm = System.Security.Cryptography.SHA256.Create() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.SHA384:
                    try { Algorithm = System.Security.Cryptography.SHA384.Create() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.SHA512:
                    try { Algorithm = System.Security.Cryptography.SHA512.Create() as AlgorithmType; } catch { }
                    break;

                case EHashAlgorithm.HMACMD5:
                    try { Algorithm = new HMACMD5() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.HMACSHA1:
                    try { Algorithm = new HMACSHA1() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.HMACSHA256:
                    try { Algorithm = new HMACSHA256() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.HMACSHA384:
                    try { Algorithm = new HMACSHA384() as AlgorithmType; } catch { }
                    break;
                case EHashAlgorithm.HMACSHA512:
                    try { Algorithm = new HMACSHA512() as AlgorithmType; } catch { }
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
            if (Algorithm != null && aInput != null)
            {
                OnBeforeCompute();
                try { aOutput = Algorithm.ComputeHash(BytesUtils.Prepend(aSALT, aInput)); OnAfterCompute(); return; } catch { }
            }

            aOutput = null;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

