using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        // Type -> PoolName -> EpikyrosiBuilt
        private static readonly Dictionary<Type, Dictionary<String, EpikyrosiBuilt?>>
            __d;

        static Epikyrosi()
        {
            __d = new Dictionary<Type, Dictionary<string, EpikyrosiBuilt?>>();
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

        #region IsValid

        public static Task<Boolean> IsValidMemberAsync(Object? o, [CallerMemberName] String? sMemberName = null) { return Task.Run(() => IsValidMember(o, sMemberName)); }
        public static Boolean IsValidMember(Object? o, [CallerMemberName] String? sMemberName = null) { return ValidateMember(o, sMemberName).IsValid; }

        public static Task<Boolean> IsValidAsync(Object? o) { return Task.Run(() => IsValid(o)); }
        public static Boolean IsValid(Object? o) { return Validate(o).IsValid; }

        public static Task<Boolean> IsValidAsync(Object? o, String? sPoolName) { return Task.Run(() => IsValid(o, sPoolName)); }
        public static Boolean IsValid(Object? o, String? sPoolName) { return Validate(o, sPoolName).IsValid; }

        public static Task<Boolean> IsValidAsync(Object? o, String? sPoolName, Boolean bStopOnFirstNotValid) { return Task.Run(() => IsValid(o, sPoolName, bStopOnFirstNotValid)); }
        public static Boolean IsValid(Object? o, String? sPoolName, Boolean bStopOnFirstNotValid) { return Validate(o, sPoolName, bStopOnFirstNotValid).IsValid; }

        #endregion

        #region Validate

        public static Task<EpikyrosiResult> ValidateMemberAsync(Object? o, [CallerMemberName] String? sMemberName = null) { return Task.Run(() => Validate(o, sMemberName, true)); }
        public static EpikyrosiResult ValidateMember(Object? o, [CallerMemberName] String? sMemberName = null)
        {
            return !String.IsNullOrWhiteSpace(sMemberName)
                ? __Validate(o, sMemberName, null, true)
                : EpikyrosiResult.NotValidOnMemberName;
        }

        public static Task<EpikyrosiResult> ValidateAsync(Object? o) { return Task.Run(() => Validate(o)); }
        public static EpikyrosiResult Validate(Object? o) { return __Validate(o, null, null, true); }

        public static Task<EpikyrosiResult> ValidateAsync(Object? o, String? sPoolName) { return Task.Run(() => Validate(o, sPoolName)); }
        public static EpikyrosiResult Validate(Object? o, String? sPoolName) { return __Validate(o, null, sPoolName, true); }

        public static Task<EpikyrosiResult> ValidateAsync(Object? o, String? sPoolName, Boolean bStopOnFirstNotValid) { return Task.Run(() => Validate(o, sPoolName, bStopOnFirstNotValid)); }
        public static EpikyrosiResult Validate(Object? o, String? sPoolName, Boolean bStopOnFirstNotValid) { return __Validate(o, null, sPoolName, bStopOnFirstNotValid); }

        private static EpikyrosiResult __Validate(Object? o, String? sMemberName, String? sPoolName, Boolean bStopOnFirstNotValid)
        {
            if (o == null)
                return EpikyrosiResult.NotValidOnObject;

            Type t = o.GetType();

            lock(__d)
            {
                Dictionary<String, EpikyrosiBuilt?>? d0;
                if (!__d.TryGetValue(t, out d0) || d0 == null)
                    __d[t] = d0 = new Dictionary<string, EpikyrosiBuilt?>();

                #region Normalizzo sPoolName

                Normalize(ref sPoolName, out sPoolName);

                #endregion

                EpikyrosiBuilt? eb;

            d0:

                if (d0.TryGetValue(sPoolName, out eb))
                    return
                        eb != null
                            ?
                            (
                                !String.IsNullOrWhiteSpace(sMemberName)
                                    ? eb.ValidateMember(o, sMemberName)
                                    : eb.Validate(o, bStopOnFirstNotValid)
                            )
                            : EpikyrosiResult.Valid;

                MemberInfo[]? mia;

                #region Recupero tutti i Members dell'Object "o" che ha "t" come Type

                mia = ReflectionUtils.GetMembers(t, CBindingFlags.PublicInstance | BindingFlags.GetField | BindingFlags.GetProperty);

                #endregion

                if (mia == null)
                {
                    d0[sPoolName] = null;
                    goto d0;
                }

                Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>> d1;

                #region Genero il Dictionary d1 di supporto che servirà per creare gli EpiroskyBuilt (se possibile)

                d1 = new Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>>();

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

                            if (!d1.TryGetValue(pnk, out dj) || dj == null)
                                d1[pnk] = dj = new Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>();

                            if (!dj.TryGetValue(eei, out lj) || lj == null)
                                dj[eei] = lj = new List<AEpikyrosiRule>();

                            eraa[j].ParseToRule(out erj);
                            lj.Add(erj);
                        }
                    }
                }

                #endregion

                #region Genero gli EpikyrosiBuilt a partire dal Dictionary d1 di supporto creato in precedenza

                Dictionary<String, Dictionary<EpikyrosiEntity, List<AEpikyrosiRule>>>.Enumerator
                    enmi = d1.GetEnumerator();

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

                    d0[enmi.Current.Key] = ebi.Build();
                }

                if (!d0.TryGetValue(sPoolName, out eb))
                    d0[sPoolName] = null;

                #endregion

                goto d0;
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

