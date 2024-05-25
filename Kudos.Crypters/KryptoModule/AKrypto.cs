using System;
using Kudos.Constants;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Enums;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Texts;

namespace Kudos.Crypters.KryptoModule
{
	public abstract class
        AKrypto<DescriptorType>
    :
        IDisposable
    where
        DescriptorType
    :
        AKryptoDescriptor<DescriptorType>, new()

    {
        private static readonly byte __bySALTSeparator;
        private static Byte[] __baSALTSeparator;
        private static readonly ECharType __eSALTCharType;

        static AKrypto()
        {
            // Non posso aggiungere caratteri particolari come $ perchè viene usato per separare il SALT dall'Object
            __eSALTCharType = ECharType.StandardLowerCase | ECharType.StandardUpperCase | ECharType.Numeric;
            __bySALTSeparator = ByteUtils.NNParse(CCharacter.Dollar);
            __baSALTSeparator = new byte[] { __bySALTSeparator };
        }

        private readonly DescriptorType _kd;
        public AKrypto(ref DescriptorType kd) { _kd = kd; }

        protected void _RandomSALT(Boolean bAppendSeparator, out Byte[]? baOut)
        {
            baOut = _kd.SALTDescriptor.Length != null ? BytesUtils.Random(_kd.SALTDescriptor.Length.Value, __eSALTCharType) : null;
            if (baOut == null) return;
            else if (bAppendSeparator) _AppendBytes(ref baOut, ref __baSALTSeparator, out baOut);
        }

        //protected void _FindSALT(ref String? s, out Byte[]? baSALT)
        //{
        //    if (s == null) { baSALT = null; return; }
        //    String[] sa = s.Split(CCharacter.Dollar);
        //    if (!ArrayUtils.IsValidIndex(sa, 0) || sa[0].Length == s.Length) { baSALT = null; return; }
        //    _ParseToBytesArray(ref sa[0], out baSALT);
        //}

        protected void _FindSALTLength(ref Byte[]? ba, out Int32 j)
        {
            j = -1;
            if (ba == null) return;
            for(int i=0; i<ba.Length; i++) { if (ba[i] != __bySALTSeparator) continue; j = i+1; break; }
        }

        //protected void _FindSALT(ref Byte[]? ba, out Byte[]? baSALT)
        //{
        //    if (ba == null) { baSALT = null; return; }
        //    Int32 j = -1;
        //    for (int i = 0; i < ba.Length; i++) { if (ba[i] != __bySALTSeparator) continue; j = i; break; }
        //    if (j < 0) { baSALT = null; return; }
        //    baSALT = new Byte[j];
        //    Array.Copy(ba, 0, baSALT, 0, baSALT.Length);
        //}

        //protected void _RandomSALT(out String? s) { byte[]? ba; _RandomSALT(out ba); _ParseToString(ref ba, out s); }

        protected void _SplitBytes(ref byte[]? baIn, ref Int32 i, out byte[]? baOut0, out byte[]? baOut1) { ArrayUtils.Split(baIn, i, out baOut0, out baOut1); }

        protected void _PrependBytes(ref byte[]? baIn0, ref byte[]? baIn1, out byte[]? baOut) { baOut = ArrayUtils.Prepend(baIn0, baIn1); }
        protected void _AppendBytes(ref byte[]? baIn0, ref byte[]? baIn1, out byte[]? baOut) { baOut = ArrayUtils.Append(baIn0, baIn1); }

        protected void _ParseToString(ref Byte[]? ba, out String? s) { s = StringUtils.Parse(ba, _kd.Encoding); }
        protected void _ParseToBytesArray(ref String? s, out Byte[]? ba) { ba = BytesUtils.Parse(s, _kd.Encoding); }

        protected void _ConvertToBaseXString(ref Byte[]? ba, out String? s)
        {
            switch(_kd.BinaryEncoding)
            {
                case EBinaryEncoding.Base16:
                    s = StringUtils.ConvertToBase16(ba);
                    break;
                case EBinaryEncoding.Base64:
                    s = StringUtils.ConvertToBase64(ba);
                    break;
                default:
                    s = null;
                    break;
            }
        }

        protected void _ConvertToBaseXBytesArray(ref String? s, out Byte[]? ba)
        {
            switch (_kd.BinaryEncoding)
            {
                case EBinaryEncoding.Base16:
                    ba = BytesUtils.ConvertFromBase16(s);
                    break;
                case EBinaryEncoding.Base64:
                    ba = BytesUtils.ConvertFromBase64(s);
                    break;
                default:
                    ba = null;
                    break;
            }
        }

        public abstract void Dispose();
    }
}

