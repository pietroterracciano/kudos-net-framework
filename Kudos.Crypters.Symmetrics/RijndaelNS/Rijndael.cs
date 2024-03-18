using Kudos.Crypters.Symmetrics.RijndaelNS.Enums;
using Kudos.Crypters.Symmetrics.RijndaelNS.Models;
using Kudos.Enums;
using Kudos.Utils;
using Kudos.Utils.Enums;
using Kudos.Utils.Numerics.Integers;
using System;
using System.Security.Cryptography;

namespace Kudos.Crypters.Symmetrics.RijndaelNS
{
    public sealed class Rijndael : ASymmetric<SRCrypterPreferencesModel>
    {
        private RijndaelManaged
            _oManaged;

        private Boolean
            _bIsReadyToEncryptDecrypt;

        public Rijndael()
        {
            _oManaged = new RijndaelManaged();
        }

        public Boolean ImportKey(String sKey)
        {
            _bIsReadyToEncryptDecrypt = false;
            Byte[] aKey;
            Internal_ToBytes(ref sKey, out aKey);
            return ImportKey(aKey);
        }

        public Boolean ImportKey(Byte[] aKey)
        {
            _bIsReadyToEncryptDecrypt = false;

            if (
                _oManaged == null
                || aKey == null
            )
                return false;

            try
            {
                // TODO da rivedere
                _oManaged.KeySize = Int32Utils.From(EnumUtils.GetValue(Preferences.KeySize));
            }
            catch
            {
                return false;
            }

            Int32 iRMKeySizeInBytes = _oManaged.KeySize / 8;

            Byte[] aPaddedKey;

            if (aKey.Length != iRMKeySizeInBytes)
            {
                aPaddedKey = new Byte[iRMKeySizeInBytes];

                Array.Copy(
                    aKey,
                    0,
                    aPaddedKey,
                    0,
                    aKey.Length > iRMKeySizeInBytes
                        ? iRMKeySizeInBytes
                        : aKey.Length
                );
            }
            else
                aPaddedKey = aKey;

            try
            {
                _oManaged.Key = aPaddedKey;
                _bIsReadyToEncryptDecrypt = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ApplyPreferences()
        {
            _oManaged.Mode = Preferences.CipherMode;
            _oManaged.Padding = Preferences.PaddingMode;
        }

        private Boolean CanEncryptDecrypt()
        {
            return
                _bIsReadyToEncryptDecrypt
                && Preferences.Encoding != null;
        }

        public String Encrypt(Object oInput)
        {
            if (!CanEncryptDecrypt())
                return null;

            ApplyPreferences();

            String sInput = JSONUtils.Serialize(oInput);

            DirtyWithSALT(ref sInput);

            Byte[] aInput;
            Internal_ToBytes(ref sInput, out aInput);
            if (aInput == null)
                return null;

            try
            {
                _oManaged.GenerateIV();
            }
            catch
            {
                return null;
            }

            ICryptoTransform oRMEncryptor;
            try
            {
                oRMEncryptor = _oManaged.CreateEncryptor();
            }
            catch
            {
                return null;
            }

            Byte[] aOutput;
            try { aOutput = oRMEncryptor.TransformFinalBlock(aInput, 0, aInput.Length); } catch { aOutput = null; }

            try
            {
                oRMEncryptor.Dispose();
            }
            catch
            {

            }

            if (aOutput == null)
                return null;

            AppendIV(ref aOutput, _oManaged.IV);

            String oString;
            External_ToString(ref aOutput, out oString);
            return oString;
        }

        public ObjectType Decrypt<ObjectType>(String sInput)
        {
            if (!CanEncryptDecrypt())
                return default(ObjectType);

            ApplyPreferences();

            Byte[] aInputWithIV;
            External_ToBytes(ref sInput, out aInputWithIV);

            if (aInputWithIV == null)
                return default(ObjectType);

            Byte[] aIV, aInput;
            RemoveIV(ref aInputWithIV, out aInput, out aIV);
            if (aIV == null || aInput == null)
                return default(ObjectType);

            try
            {
                _oManaged.IV = aIV;
            }
            catch
            {
                return default(ObjectType);
            }

            ICryptoTransform oRMDecryptor;
            try
            {
                oRMDecryptor = _oManaged.CreateDecryptor();
            }
            catch
            {
                return default(ObjectType);

            }

            Byte[] aOutput;
            try { aOutput = oRMDecryptor.TransformFinalBlock(aInput, 0, aInput.Length); } catch { aOutput = null; }

            try
            {
                oRMDecryptor.Dispose();
            }
            catch
            {

            }

            String sOutput;
            Internal_ToString(ref aOutput, out sOutput);
            if (sOutput == null)
                return default(ObjectType);

            CleanSALT(ref sOutput);

            return sOutput != null
                ? JSONUtils.Deserialize<ObjectType>(sOutput)
                : default(ObjectType);
        }

        public override void Dispose()
        {
            if (_oManaged != null)
                try
                {
                    _oManaged.Dispose();
                }
                catch
                {
                }
        }

        public static Byte[] GenerateIV()
        {
            RijndaelManaged oManaged = new RijndaelManaged();

            if (oManaged == null)
                return null;

            Byte[] aIV;
            try
            {
                oManaged.BlockSize = 128;
                oManaged.GenerateIV();
                aIV = oManaged.IV;
            }
            catch
            {
                aIV = null;
            }

            try
            {
                oManaged.Dispose();
            }
            catch
            {

            }

            return aIV;
        }

        public static Byte[] GenerateKey(ESRKeySize eKeySize = ESRKeySize._128bit)
        {
            RijndaelManaged oManaged = new RijndaelManaged();

            if (oManaged == null)
                return null;

            Byte[] aKey;
            try
            {
                // TODO da rivedere
                oManaged.KeySize = Int32Utils.From(EnumUtils.GetValue(eKeySize));
                oManaged.GenerateKey();
                aKey = oManaged.Key;
            }
            catch
            {
                aKey = null;
            }

            try
            {
                oManaged.Dispose();
            }
            catch
            {

            }

            return aKey;
        }

    }
}
