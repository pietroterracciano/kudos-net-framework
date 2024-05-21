using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Kudos.Constants;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Validations.EpikyrosiModule.Attributes;
using Kudos.Validations.EpikyrosiModule.Builders;
using Kudos.Validations.EpikyrosiModule.Builts;
using Kudos.Validations.EpikyrosiModule.Entities;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builders;
using Kudos.Validations.EpikyrosiModule.Interfaces.Builds;
using Kudos.Validations.EpikyrosiModule.Interfaces.Entities;
using Kudos.Validations.EpikyrosiModule.Results;
using Kudos.Validations.EpikyrosiModule.Rules;

namespace Kudos.Validations.EpikyrosiModule
{
	public static class Epikyrosi
	{
        #region ... static ...

        // Type.Module.MetadataToken -> Type.MetadataToken -> PoolName -> EpikyrosiBuilt
        private static readonly Dictionary<int, Dictionary<int, Dictionary<String, EpikyrosiBuilt?>>>
            __d;

        static Epikyrosi()
        {
            __d = new Dictionary<int, Dictionary<int, Dictionary<string, EpikyrosiBuilt?>>>();
        }

        #endregion

        #region Builder

        #region public static IEpikyrosi RequestBuilder()

        public static IEpikyrosiBuilder RequestBuilder()
        {
            return new EpikyrosiBuilder();
        }

        #endregion

        #endregion

        #region Entity

        #region public static IEpikyrosiEntity GetTable<...>(...)

        public static IEpikyrosiEntity RequestEntity<T>(String? s) { EpikyrosiEntity ee; EpikyrosiEntity.Get<T>(ref s, out ee); return ee; }
        public static IEpikyrosiEntity RequestEntity(Type? t, String? s) { EpikyrosiEntity ee; EpikyrosiEntity.Get(ref t, ref s, out ee); return ee; }

        //public static GefyraTable RequestTable(String? sName) { GefyraTable gt; GefyraTable.Request(ref sName, out gt); return gt; }
        //public static GefyraTable RequestTable(String? sSchemaName, String? sName) { GefyraTable gt; GefyraTable.Request(ref sSchemaName, ref sName, out gt); return gt; }\

        #endregion

        #endregion

        #region Validate

        public static EpikyrosiResult Validate(Object? o, Boolean bStopOnFirstNotValid = false)
        {
            return Validate(o, null, bStopOnFirstNotValid);
        }
        public static EpikyrosiResult Validate(Object? o, String? sPoolName, Boolean bStopOnFirstNotValid = false)
        {
            if (o == null)
                return EpikyrosiResult.NotValidOnObject;

            Type t = o.GetType();

            lock(__d)
            {
                Dictionary<int, Dictionary<String, EpikyrosiBuilt?>>? d0;
                if (!__d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null)
                    __d[t.Module.MetadataToken] = d0 = new Dictionary<int, Dictionary<String, EpikyrosiBuilt?>>();

                Dictionary<String, EpikyrosiBuilt?>? d1;
                if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null)
                    d0[t.MetadataToken] = d1 = new Dictionary<string, EpikyrosiBuilt?>();

                #region Normalizzo sPoolName

                Normalize(ref sPoolName, out sPoolName);

                #endregion

                EpikyrosiBuilt? eb;

                d1:

                if (d1.TryGetValue(sPoolName, out eb))
                    return
                        eb != null
                            ? eb.Validate(o, bStopOnFirstNotValid)
                            : EpikyrosiResult.Valid;

                MemberInfo[]? mia;

                #region Recupero tutti i Members dell'Object "o" che ha "t" come Type

                mia = ReflectionUtils.GetMembers(t, CBindingFlags.PublicInstance | BindingFlags.GetField | BindingFlags.GetProperty);

                #endregion

                if (mia == null)
                {
                    d1[sPoolName] = null;
                    goto d1;
                }

                Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>> d2;

                #region Genero il Dictionary d2 di supporto che servirà per creare gli EpiroskyBuilt (se possibile)

                d2 = new Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>>();

                AEpikyrosiRuleAttribute[]? eraa;
                EpikyrosiEntity eei;

                for (int i = 0; i < mia.Length; i++)
                {
                    if (!CMemberTypes.FieldProperty.HasFlag(mia[i].MemberType)) continue;
                    EpikyrosiEntity.Get(ref mia[i], out eei);
                    if (eei == null) continue;
                    eraa = ReflectionUtils.GetCustomAttributes<AEpikyrosiRuleAttribute>(mia[i], true);
                    if (eraa == null) continue;

                    Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>> dj;
                    List<AEpikyrosiRule>? lj;
                    AEpikyrosiRule erj;
                    String?[]? pnaj;

                    for (int j=0; j<eraa.Length; j++)
                    {
                        pnaj = eraa[j].PoolNames;
                        if (pnaj == null) pnaj = new string?[] { null };

                        String? pnk;
                        for (int k = 0; k < pnaj.Length; k++)
                        {
                            Normalize(ref pnaj[k], out pnk);

                            if (!d2.TryGetValue(pnk, out dj) || dj == null)
                                d2[pnk] = dj = new Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>();

                            if (!dj.TryGetValue(eei, out lj) || lj == null)
                                dj[eei] = lj = new List<AEpikyrosiRule>();

                            eraa[j].ParseToRule(out erj);
                            lj.Add(erj);
                        }
                    }
                }

                #endregion

                #region Genero gli EpikyrosiBuilt a partire dal Dictionary d2 di supporto creato in precedenza

                Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>>.Enumerator
                    enmi = d2.GetEnumerator();

                EpikyrosiBuilder ebi;

                while(enmi.MoveNext())
                {
                    ebi = new EpikyrosiBuilder();

                    Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>.Enumerator
                        enmj = enmi.Current.Value.GetEnumerator();

                    IEpikyrosiEntity eej;

                    while(enmj.MoveNext())
                    {
                        eej = enmj.Current.Key;

                        AEpikyrosiRule erk;
                        for(int k=0; k<enmj.Current.Value.Count; k++)
                        {
                            erk = enmj.Current.Value[k];
                            ebi.AddRule(ref eej, ref erk);
                        }
                    }

                    d1[enmi.Current.Key] = ebi.Build();
                }

                if (!d1.TryGetValue(sPoolName, out eb))
                    d1[sPoolName] = null;

                #endregion

                goto d1;
            }
        }

        #endregion

        private static void Normalize(ref String? si, out String so)
        {
            if(si == null) { so = String.Empty; return; }
            so = si.ToUpper().Trim();
        }
    }
}

