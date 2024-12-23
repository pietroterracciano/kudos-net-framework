﻿using Kudos.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using Kudos.Databasing.ORMs.GefyraModule.Constants;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Descriptors;
using Kudos.Databasing.ORMs.GefyraModule.Utils;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kudos.Databasing.ORMs.GefyraModule.Descriptors
{
    public sealed class
        GefyraTableDescriptor
    :
        AGefyraDescriptor,//<DatabaseTableDescriptor>,
        IGefyraTableDescriptor
    {
        #region ... static ...

        private static readonly string
            __sgcdPrefix,
            __sgtdPrefix,
            __snPrefix,
            __ssnPrefix;

        private static readonly StringBuilder
            __sb;
        // Type -> IGefyraTableDescriptor
        private static readonly Dictionary<Type, GefyraTableDescriptor?>
            __d;
        // TableDescriptorHashKey -> IGefyraTableDescriptor
        private static readonly Metas
            __m;

        static GefyraTableDescriptor()
        {
            __sgcdPrefix = "gcd";
            __sgtdPrefix = "gtd";
            __ssnPrefix = "sn";
            __snPrefix = "n";

            __d = new Dictionary<Type, GefyraTableDescriptor?>();
            __sb = new StringBuilder();
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
        }

        #region internal static void Get<...>(...)

        internal static void Get<T>(out GefyraTableDescriptor? gtd)
        {
            Type t = typeof(T);
            Get(ref t, out gtd);
        }
        internal static void Get(ref Type? t, out GefyraTableDescriptor? gtd)
        {
            if (t == null) 
            { 
                gtd = null; 
                return; 
            }

            string? ssn, sn;

            lock (__d)
            {
                if (__d.TryGetValue(t, out gtd))
                    return;

                GefyraTableAttribute?
                    gta;

                #region Recupero l'Attribute per la Class

                gta = ReflectionUtils.GetCustomAttribute<GefyraTableAttribute>(t);

                #endregion

                if(gta == null)
                {
                    __d[t] = gtd = null;
                    return;
                }

                #region Recupero i dati dall'Attribute (se possibile)

                if (gta != null)
                {
                    ssn = gta.SchemaName;
                    sn = gta.Name;
                }
                else
                    ssn = sn = null;

                #endregion

                #region Effettuo un override di sn (se necessario)

                if (string.IsNullOrWhiteSpace(sn))
                    sn = CGefyraConventionalPrefix.Class + t.Name;

                #endregion

                _Get(ref t, ref ssn, ref sn, out gtd);
                __d[t] = gtd;
            }

            //#region Aggiungo alla Cache anche il TableDescriptor base 

            //GefyraTableDescriptor gtd0;
            //ssn = null;
            //sn = t.FullName;
            //Get(ref t, ref ssn, ref sn, out gtd0);

            //#endregion
        }

        internal static void Get
        (
            ref String? sn,
            out GefyraTableDescriptor? gtd
        )
        {
            String? ssn = null;
            Get(ref ssn, ref sn, out gtd);
        }
        internal static void Get
        (
            ref String? ssn,
            ref String? sn,
            out GefyraTableDescriptor? gtd
        )
        {
            Type? t = null;
            _Get(ref t, ref ssn, ref sn, out gtd);
        }
        private static void _Get
        (
            ref Type? t,
            ref String? sn,
            out GefyraTableDescriptor? gtd
        )
        {
            String? ssn = null;
            _Get(ref t, ref ssn, ref sn, out gtd);
        }
        private static void _Get
        (
            ref Type? t,
            ref String? ssn, 
            ref String? sn,
            out GefyraTableDescriptor? gtd
        )
        {
            __OverrideNamesIfPossible(ref ssn, ref ssn, ref sn);
            __OverrideNamesIfPossible(ref sn, ref ssn, ref sn);

            string? s;
            __CalculateHashKey(ref ssn, ref sn, out s);
            if (s == null)
            {
                gtd = null;
                return;
            }

            lock (__m)
            {
                gtd = __m.Get<GefyraTableDescriptor>(s);

                if (gtd != null && !gtd.HasDeclaringType && t != null)
                    gtd = null;

                if (gtd == null)
                    __m.Set(s, gtd = new GefyraTableDescriptor(ref s, ref t, ref ssn, ref sn));
            }
        }

        #endregion

        #region private static void __CalculateHashKey(...)

        private static void __CalculateHashKey(ref string? ssn, ref string? sn, out string? s)
        {
            if (string.IsNullOrWhiteSpace(sn))
            {
                s = null;
                return;
            }

            lock (__sb)
            {
                __sb.Clear();

                if (!string.IsNullOrWhiteSpace(ssn))
                    __sb
                        .Append(__sgtdPrefix).Append(CCharacter.Dot).Append(__ssnPrefix).Append(CCharacter.DoubleDot).Append(ssn)
                        .Append(CCharacter.Pipe);

                __sb
                    .Append(__sgtdPrefix).Append(CCharacter.Dot).Append(__snPrefix).Append(CCharacter.DoubleDot).Append(sn);

                //if (!String.IsNullOrWhiteSpace(sa))
                //{
                //    if (__sb.Length > 0)
                //        __sb.Append(CCharacter.Pipe);

                //    __sb
                //        .Append(__sgtPrefix).Append(CCharacter.Dot).Append(__saPrefix).Append(CCharacter.DoubleDot).Append(sa);
                //}

                s = __sb.ToString();
            }
        }

        #endregion

        #region private static void __OverrideNamesIfPossible(...)

        private static void __OverrideNamesIfPossible(ref string? s, ref string? ssn, ref string? stn)
        {
            if
            (
                s == null
                || !s.Contains(CCharacter.Dot)
            )
                return;

            string[]
                a = s.Split(CCharacter.Dot);

            if (CollectionUtils.IsValidIndex(a, 1))
            {
                ssn = a[0]; stn = a[1];
            }
            else
                ssn = a[0];
        }

        #endregion

        #endregion

        #region SchemaName

        private String? _sSchemaName;
        public String? SchemaName { get { return _sSchemaName; } private set { HasSchemaName = !String.IsNullOrWhiteSpace(_sSchemaName = value); } }
        public Boolean HasSchemaName { get; private set; }

        #endregion

        #region DeclaringType

        public Type? DeclaringType { get; private set; }
        public Boolean HasDeclaringType { get; private set; }

        #endregion

        // MemberInfo -> IGefyraColumnDescriptor
        private readonly Dictionary<MemberInfo, GefyraColumnDescriptor?>
            _d;
        // ColumnDescriptorHashKey -> IGefyraColumnDescriptor
        private readonly Metas
            _m;

        private GefyraColumnDescriptor[]?
            _gcda;

        private readonly List<GefyraColumnDescriptor>
            _l;

        private GefyraTableDescriptor
            _this;

        private GefyraTableDescriptor(ref String shk, ref Type dt, ref String? ssn, ref String sn) : base(ref shk, ref sn)
        {
            _this = this;
            _d = new Dictionary<MemberInfo, GefyraColumnDescriptor?>();
            _m = new Metas(StringComparison.OrdinalIgnoreCase);
            _l = new List<GefyraColumnDescriptor>();

            HasDeclaringType = (DeclaringType = dt) != null;
            SchemaName = ssn;

            if (!HasDeclaringType) return;

            MemberInfo[]? mia;

            #region Recupero tutti i Members del DeclaringType

            mia = ReflectionUtils.GetMembers(DeclaringType, CBindingFlags.Instance);
            if (mia == null) return;

            #endregion

            #region Calcolo i ColumnDescriptor dei Members recuperati in precedenza e li inserisco nella cache interna

            GefyraColumnDescriptor?
                gcdi;

            for (int i=0; i<mia.Length; i++)
                GetColumnDescriptor(ref mia[i], out gcdi);

            #endregion
        }

        #region ColumnDescriptors

        #region internal void GetColumnsDescritors(...)

        internal void GetColumnsDescriptors(out GefyraColumnDescriptor[]? gcda)
        {
            lock (_l) { gcda = _gcda != null ? _gcda : (_gcda = _l.ToArray()); }
        }

        #endregion

        #region internal void GetColumnDescriptor(...)

        internal void GetColumnDescriptor
        (
            ref MemberInfo? mi, 
            out GefyraColumnDescriptor? gcd
        )
        {
            if
            (
                mi == null
                || mi.DeclaringType != DeclaringType
            )
            {
                gcd = null;
                return;
            }

            string? sn;

            lock (_d)
            {
                if (_d.TryGetValue(mi, out gcd))
                    return;

                GefyraColumnAttribute?
                    gca;

                #region Recupero l'Attribute per il Member

                gca = ReflectionUtils.GetCustomAttribute<GefyraColumnAttribute>(mi);

                #endregion

                if(gca == null)
                {
                    #region Aggiungo il ColumnDescriptor al dictionary d di cache

                    _d[mi] = gcd = null;

                    #endregion

                    return;
                }

                #region Recupero i dati dall'Attribute (se possibile)

                if (gca != null)
                    sn = gca.Name;
                else
                    sn = null;

                #endregion

                #region Effettuo un override di sn (se necessario)

                if (string.IsNullOrWhiteSpace(sn))
                {
                    Type? t = ReflectionUtils.GetMemberValueType(mi);
                    String? scp;
                    GefyraTypeUtils.GetConventionalPrefix(ref t, out scp);
                    sn = scp + mi.Name;
                }

                #endregion

                #region Richiedo un ColumnDescriptor per mi,sn

                _GetColumnDescriptor(ref mi, ref sn, out gcd);

                #endregion

                #region Aggiungo il ColumnDescriptor al dictionary d di cache

                _d[mi] = gcd;

                #endregion
            }

            //#region Richiedo un ColumnDescriptor per sn (forzo la sua aggiunta alla cache interna)

            //GefyraColumnDescriptor? gcd0;
            //sn = mi.Name;
            //_GetColumnDescriptor(ref mi, ref sn, out gcd0);

            //#endregion
        }

        internal void GetColumnDescriptor
        (
            ref String? sn,
            out GefyraColumnDescriptor? gcd
        )
        {
            MemberInfo? mi = ReflectionUtils.GetMember(DeclaringType, sn, CBindingFlags.Instance);
            if (mi != null) GetColumnDescriptor(ref mi, out gcd);
            else _GetColumnDescriptor(ref mi, ref sn, out gcd);
        }

        private void _GetColumnDescriptor
        (
            ref MemberInfo? mi,
            ref String? sn,
            out GefyraColumnDescriptor? gcd
        )
        {
            String? s;
            _CalculateColumnDescriptorHashKey(ref sn, out s);
            if (s == null)
            {
                gcd = null;
                return;
            }

            lock (_m)
            {
                gcd = _m.Get<GefyraColumnDescriptor>(s);
                if (gcd != null) return;
                _m.Set(s, gcd = new GefyraColumnDescriptor(ref s, ref _this, ref mi, ref sn));
                //if (gcd != null && !gcd.HasDeclaringMember && mi != null)
                //    gcd = null;

                //if (gcd == null)
                //{
                lock (_l)
                {
                    _l.Add(gcd);
                    _gcda = null;
                }
                //}
            }
        }

        #endregion

        #region private void _CalculateColumnDescriptorHashKey(...)

        private void _CalculateColumnDescriptorHashKey(ref string? sn, out string? s)
        {
            if (string.IsNullOrWhiteSpace(sn))
            {
                s = null;
                return;
            }

            lock (_StringBuilder)
            {
                _StringBuilder
                    .Clear()
                    .Append(__sgcdPrefix).Append(CCharacter.Dot).Append(__snPrefix).Append(CCharacter.DoubleDot).Append(sn);

                s = _StringBuilder.ToString();
            }
        }

        #endregion

        #endregion

        protected override void _OnGetSQL(ref StringBuilder sb)
        {
            if (HasSchemaName)
                sb
                    .Append(CCharacter.BackTick)
                    .Append(SchemaName)
                    .Append(CCharacter.BackTick)
                    .Append(CCharacter.Dot);

            sb
                .Append(CCharacter.BackTick)
                .Append(Name)
                .Append(CCharacter.BackTick);
        }
    }
}
