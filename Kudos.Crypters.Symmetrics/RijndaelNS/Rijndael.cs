using Kudos.Crypters.Symmetrics.RijndaelNS.Enums;
using Kudos.Crypters.Symmetrics.RijndaelNS.Models.Cryptions;
using Kudos.Crypters.Symmetrics.Utils;
using Kudos.Enums;
using Kudos.Utils;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Kudos.Crypters.Symmetrics.RijndaelNS
{
    public sealed class Rijndael : IDisposable
    {
        private RijndaelManaged 
            _oRijndaelManaged;

        private Boolean
            _bIsRijndaelReadyToEncryptDecrypt;

        public RCrypterPreferencesModel Preferences
        {
            get; private set;
        }

        public Rijndael()
        {
            _oRijndaelManaged = new RijndaelManaged();
            Preferences = new RCrypterPreferencesModel();
        }

        public Boolean ImportKey(String sKey)
        {
            _bIsRijndaelReadyToEncryptDecrypt = false;

            return
                Preferences.Encoding != null
                    ? ImportKey(BytesUtils.From(sKey, Preferences.Encoding))
                    : false;
        }

        public Boolean ImportKey(Byte[] aKey)
        {
            _bIsRijndaelReadyToEncryptDecrypt = false;

            if (
                _oRijndaelManaged == null
                || aKey == null
            )
                return false;

            try
            {
                _oRijndaelManaged.KeySize = EnumUtils.GetValue(Preferences.KeySize);
            }
            catch
            {
                return false;
            }

            Int32 iRMKeySizeInBytes = _oRijndaelManaged.KeySize / 8;

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
                _oRijndaelManaged.Key = aPaddedKey;
                _bIsRijndaelReadyToEncryptDecrypt = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ApplyPreferences()
        {
            _oRijndaelManaged.Mode = Preferences.CipherMode;
            _oRijndaelManaged.Padding = Preferences.PaddingMode;
        }

        private Boolean CanEncryptDecrypt()
        {
            return
                _bIsRijndaelReadyToEncryptDecrypt
                && Preferences.Encoding != null;
        }

        public String Encrypt(Object oInput)
        {
            if (!CanEncryptDecrypt())
                return null;

            ApplyPreferences();
 
            String sInput = JSONUtils.Serialize(oInput);

            if (Preferences.SALTPreferences.Use)
                sInput = CSymmetricUtils.DirtyWithSALT(sInput, Preferences.SALTPreferences);

            Byte[] aInput = BytesUtils.From(sInput, Preferences.Encoding);
            if (aInput == null)
                return null;

            try
            {
                _oRijndaelManaged.GenerateIV();
            }
            catch
            {
                return null;
            }

            ICryptoTransform oRMEncryptor;
            try
            {
                oRMEncryptor = _oRijndaelManaged.CreateEncryptor();
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

            aOutput = CSymmetricUtils.AppendIV(aOutput, _oRijndaelManaged.IV);

            return Preferences.BinaryEncoding == EBinaryEncoding.Base64
                ? StringUtils.ToBase64(aOutput)
                : StringUtils.ToBase16(aOutput);
        }

        public ObjectType Decrypt<ObjectType>(String sInput)
        {
            if (!CanEncryptDecrypt())
                return default(ObjectType);

            ApplyPreferences();

            Byte[] aInputWithIV =
                Preferences.BinaryEncoding == EBinaryEncoding.Base64
                    ? BytesUtils.FromBase64(sInput)
                    : BytesUtils.FromBase16(sInput);

            if (aInputWithIV == null)
                return default(ObjectType);

            Byte[] aIV, aInput;
            CSymmetricUtils.RemoveIV(aInputWithIV, out aInput, out aIV);
            if (aIV == null || aInput == null)
                return default(ObjectType);

            try
            {
                _oRijndaelManaged.IV = aIV;
            }
            catch
            {
                return default(ObjectType);
            }

            ICryptoTransform oRMDecryptor;
            try
            {
                oRMDecryptor = _oRijndaelManaged.CreateDecryptor();
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

            String sOutput = StringUtils.From(aOutput, Preferences.Encoding);
            if (sOutput == null)
                return default(ObjectType);

            if (Preferences.SALTPreferences.Use)
                sOutput = CSymmetricUtils.CleanSALT(sOutput, Preferences.SALTPreferences);

            return sOutput != null
                ? JSONUtils.Deserialize<ObjectType>(sOutput)
                : default(ObjectType);
        }

        public static Byte[] GenerateIV()
        {
            RijndaelManaged oRijndaelManaged = new RijndaelManaged();

            if (oRijndaelManaged == null)
                return null;

            Byte[] aIV;
            try
            {
                oRijndaelManaged.BlockSize = 128;
                oRijndaelManaged.GenerateIV();
                aIV = oRijndaelManaged.IV;
            }
            catch
            {
                aIV = null;
            }

            try
            {
                oRijndaelManaged.Dispose();
            }
            catch
            {

            }

            return aIV;
        }

        public static Byte[] GenerateKey(ERKeySize eKeySize = ERKeySize._128bit)
        {
            RijndaelManaged oRijndaelManaged = new RijndaelManaged();

            if (oRijndaelManaged == null)
                return null;

            Byte[] aKey;
            try
            {
                oRijndaelManaged.KeySize = EnumUtils.GetValue(eKeySize);
                oRijndaelManaged.GenerateKey();
                aKey = oRijndaelManaged.Key;
            }
            catch
            {
                aKey = null;
            }

            try
            {
                oRijndaelManaged.Dispose();
            }
            catch
            {

            }

            return aKey;
        }

        public void Dispose()
        {
            if (_oRijndaelManaged != null)
                try
                {
                    _oRijndaelManaged.Dispose();
                }
                catch
                { 
                }
        }
    }
}
