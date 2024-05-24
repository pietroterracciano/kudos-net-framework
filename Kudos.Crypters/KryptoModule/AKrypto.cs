using System;
using Kudos.Crypters.KryptoModule.Descriptors;
using Kudos.Enums;
using Kudos.Utils;
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
        private readonly DescriptorType _kd;
        public AKrypto(ref DescriptorType kd) { _kd = kd; }

        protected void _RandomSALT(out Byte[]? baOut)
        {
            baOut = _kd.SALTDescriptor.Length != null && _kd.SALTDescriptor.CharType != null
                ? BytesUtils.Random(_kd.SALTDescriptor.Length.Value, _kd.SALTDescriptor.CharType.Value)
                : null;
        }

        protected void _RandomSALT(out String? s)
        {
            Byte[]? ba;
            _RandomSALT(out ba);
            _ParseToString(ref ba, out s);
        }

        protected void _PrependSALT(ref byte[]? bIn, ref byte[]? baSALT, out byte[] baOut)
        {
            baOut = BytesUtils.Prepend(bIn, baSALT);
        }

        protected void _ParseToString(ref Byte[]? ba, out String? s) { s = StringUtils.NNParse(ba, _kd.Encoding); }
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

