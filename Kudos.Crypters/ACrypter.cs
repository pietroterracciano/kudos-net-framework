﻿using Kudos.Crypters.Models;
using Kudos.Crypters.Models.SALTs;
using Kudos.Enums;
using Kudos.Utils;
using Kudos.Utils.Texts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Crypters
{
    public abstract class ACrypter<CrypterPreferencesModelType, SALTPreferencesModelType> : IDisposable
        where CrypterPreferencesModelType : CrypterPreferencesModel<SALTPreferencesModelType>, new()
        where SALTPreferencesModelType : SALTPreferencesModel, new()
    {
        public CrypterPreferencesModelType Preferences
        {
            get;
            private set;
        }

        public ACrypter()
        {
            Preferences = new CrypterPreferencesModelType();
        }

        protected void GenerateSALT(out Byte[] aSALT)
        {
            aSALT = null;
            //Preferences.SALTPreferences.Use
              //  ? BytesUtils.Random(Preferences.SALTPreferences.Length, Preferences.SALTPreferences.CharType)
                //: null;
        }

        protected void GenerateSALT(out String sSALT)
        {
            Byte[] aSALT;
            GenerateSALT(out aSALT);
            Internal_ToString(ref aSALT, out sSALT);
        }

        protected void Internal_ToString(ref Byte[] aBytes, out String oString)
        {
            if(Preferences.Encoding == null)
            {
                oString = null;
                return;
            }

            oString = StringUtils.Parse( aBytes, Preferences.Encoding );
        }

        //protected void Internal_ToBytes(ref Object oInput, out Byte[] aBytes)
        //{
        //    String sInput = JSONUtils.Serialize(oInput);
        //    Internal_ToBytes(ref sInput, out aBytes);
        //}

        protected void Internal_ToBytes(ref String oString, out Byte[] aBytes)
        {
            if (Preferences.Encoding == null)
            {
                aBytes = null;
                return;
            }

            aBytes = BytesUtils.Parse(oString, Preferences.Encoding);
        }

        protected void External_ToString(ref Byte[] aBytes, out String oString)
        {
            oString = Preferences.BinaryEncoding == EBinaryEncoding.Base64
                ? StringUtils.ConvertToBase64(aBytes)
                : StringUtils.ConvertToBase16(aBytes);
        }

        protected void External_ToBytes(ref String oString, out Byte[] aBytes)
        {
            aBytes = Preferences.BinaryEncoding == EBinaryEncoding.Base64
                ? BytesUtils.ConvertFromBase64(oString)
                : BytesUtils.ConvertFromBase16(oString);
        }

        public abstract void Dispose();
    }
}