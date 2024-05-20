﻿using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;

namespace Kudos.Databases.ORMs.GefyraModule
{
    public static class Gefyra
    {
        #region Builder

        #region public static IGefyraBuilder RequestBuilder()

        public static IGefyraBuilder RequestBuilder()
        {
            return new GefyraBuilder();
        }

        #endregion

        #endregion

        #region Table

        #region public static GefyraTable GetTable<...>(...)

        public static IGefyraTable GetTable<T>() { GefyraTable gt; GefyraTable.Get<T>(out gt); return gt; }
        public static IGefyraTable GetTable(Type? t) { GefyraTable gt; GefyraTable.Get(ref t, out gt); return gt; }

        //public static GefyraTable RequestTable(String? sName) { GefyraTable gt; GefyraTable.Request(ref sName, out gt); return gt; }
        //public static GefyraTable RequestTable(String? sSchemaName, String? sName) { GefyraTable gt; GefyraTable.Request(ref sSchemaName, ref sName, out gt); return gt; }\

        #endregion

        #endregion

        //#region Column

        //internal static void __GetColumn(ref GefyraTable? gt, ref String? sn, ref String? sa, out GefyraColumn gc)
        //{
        //    if (gt == null)
        //    {
        //        gc = GefyraColumn.Invalid;
        //        return;
        //    }

        //    String? s;
        //    __CalculateColumnHashKey(ref sn, ref sa, out s);

        //    if(s == null)
        //    {
        //        gc = GefyraColumn.Invalid;
        //        return; 
        //    }

        //    lock (__lck)
        //    {
        //        gc = __m.Get<GefyraColumn>(s, StringComparison.OrdinalIgnoreCase);
        //        if (gc != null) return gc;

        //        MemberInfo? mi = ReflectionUtils.GetMember(DeclaringType, s);
        //        if (mi != null)
        //        {
        //            StringBuilder sb;
        //            _GetStringBuilder(out sb);

        //            GefyraColumnAttribute?
        //                gca = ReflectionUtils.GetCustomAttribute<GefyraColumnAttribute>(mi);

        //            if (
        //                gca != null
        //                && gca.Name != null
        //                && gca.Name.Length > 0
        //            )
        //                sb.Append(gca.Name);
        //            else
        //            {
        //                String scp; Type? tmvt = ReflectionUtils.GetMemberValueType(mi);
        //                GefyraTypeUtils.GetConventionalPrefix(ref tmvt, out scp);
        //                sb.Append(scp).Append(mi.Name);
        //            }

        //            String sgcn = sb.ToString();
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref sgcn, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);
        //        }
        //        else
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref s, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);

        //        return gc;
        //    }
        //}

        //private static void __CalculateColumnHashKey(ref String? sn, ref String? sa, out String? s)
        //{
        //    if (String.IsNullOrWhiteSpace(sn)) { s = null; return; }

        //    lock (__lck)
        //    {
        //        __sb.Clear();

        //        __sb
        //            .Append("gc").Append(CCharacter.Dot).Append("n").Append(CCharacter.DoubleDot).Append(sn);

        //        if (!String.IsNullOrWhiteSpace(sa))
        //        {
        //            if (__sb.Length > 0)
        //                __sb.Append(CCharacter.Pipe);

        //            __sb
        //                .Append("gc").Append(CCharacter.Dot).Append("a").Append(CCharacter.DoubleDot).Append(sa);
        //        }

        //        s = __sb.ToString();
        //    }
        //}



        //#endregion

        //internal static void __GetColumn(ref GefyraTable? gt, ref String? s, out GefyraColumn gc)
        //{
        //    if (gt == null || String.IsNullOrWhiteSpace(s))
        //    {
        //        gc = GefyraColumn.Invalid;
        //        return;
        //    }

        //    lock (__lck)
        //    {
        //        String schk;
        //        CalculateColumnHashKey(ref s, out schk);

        //        GefyraColumn? gc = m.Get<GefyraColumn>(schk, StringComparison.OrdinalIgnoreCase);
        //        if (gc != null) return gc;

        //        MemberInfo? mi = ReflectionUtils.GetMember(DeclaringType, s);
        //        if (mi != null)
        //        {
        //            StringBuilder sb;
        //            _GetStringBuilder(out sb);

        //            GefyraColumnAttribute?
        //                gca = ReflectionUtils.GetCustomAttribute<GefyraColumnAttribute>(mi);

        //            if (
        //                gca != null
        //                && gca.Name != null
        //                && gca.Name.Length > 0
        //            )
        //                sb.Append(gca.Name);
        //            else
        //            {
        //                String scp; Type? tmvt = ReflectionUtils.GetMemberValueType(mi);
        //                GefyraTypeUtils.GetConventionalPrefix(ref tmvt, out scp);
        //                sb.Append(scp).Append(mi.Name);
        //            }

        //            String sgcn = sb.ToString();
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref sgcn, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);
        //        }
        //        else
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref s, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);

        //        return gc;
        //    }
        //}

        //private static void __CalculateColumnHashKey(ref GefyraTable gt, ref String s, out String s)
        //{
        //    __sb.Clear();

        //    __sb
        //        .Append("gt").Append(CCharacter.Dot).Append("sn").Append(CCharacter.DoubleDot).Append(sSchemaName);

        //    if (__sb.Length > 0)
        //        __sb.Append(CCharacter.Pipe);

        //    __sb
        //        .Append("gt").Append(CCharacter.Dot).Append("n").Append(CCharacter.DoubleDot).Append(sName);

        //    sOut = __sb.ToString();
        //}


















        //#region public static GefyraTable GetTable<...>(...)

        //public static GefyraTable GetColumn(GefyraTable? gt, String? s)
        //{
        //    if (t == null)
        //        return GefyraTable.Invalid;

        //    lock (__lck)
        //    {
        //        Dictionary<Int32, GefyraTable> d;
        //        if (!__d.TryGetValue(t.Module.MetadataToken, out d) || d == null)
        //            __d[t.Module.MetadataToken] = d = new Dictionary<int, GefyraTable>();

        //        GefyraTable? gt;
        //        if (d.TryGetValue(t.MetadataToken, out gt) && gt != null)
        //            return gt;

        //        GefyraTableAttribute? gta;

        //        #region Recupero l'Attribute per la Class

        //        gta = ReflectionUtils.GetCustomAttribute<GefyraTableAttribute>(t);

        //        #endregion

        //        String?
        //            sSchemaName;
        //        String?
        //            sName;

        //        #region Recupero i dati dall'Attribute (se possibile)

        //        if (gta != null && gta.Name != null && gta.Name.Length > 0)
        //        {
        //            sSchemaName = gta.SchemaName;
        //            sName = gta.Name;
        //        }
        //        else
        //            sSchemaName = sName = null;

        //        if (String.IsNullOrWhiteSpace(sName))
        //            sName = CGefyraConventionalPrefix.Class + t.Name;

        //        #endregion

        //        String? sAlias = null;
        //        return d[t.MetadataToken] = gt = new GefyraTable(ref t, ref sSchemaName, ref sName, ref sAlias);
        //    }
        //}

        //#endregion

        //internal static void __GetColumn(ref GefyraTable? gt, ref String? s, out GefyraColumn gc)
        //{
        //    if (gt == null || String.IsNullOrWhiteSpace(s))
        //    {
        //        gc = GefyraColumn.Invalid;
        //        return;
        //    }

        //    lock (__lck)
        //    {
        //        String schk;
        //        CalculateColumnHashKey(ref s, out schk);

        //        GefyraColumn? gc = m.Get<GefyraColumn>(schk, StringComparison.OrdinalIgnoreCase);
        //        if (gc != null) return gc;

        //        MemberInfo? mi = ReflectionUtils.GetMember(DeclaringType, s);
        //        if (mi != null)
        //        {
        //            StringBuilder sb;
        //            _GetStringBuilder(out sb);

        //            GefyraColumnAttribute?
        //                gca = ReflectionUtils.GetCustomAttribute<GefyraColumnAttribute>(mi);

        //            if (
        //                gca != null
        //                && gca.Name != null
        //                && gca.Name.Length > 0
        //            )
        //                sb.Append(gca.Name);
        //            else
        //            {
        //                String scp; Type? tmvt = ReflectionUtils.GetMemberValueType(mi);
        //                GefyraTypeUtils.GetConventionalPrefix(ref tmvt, out scp);
        //                sb.Append(scp).Append(mi.Name);
        //            }

        //            String sgcn = sb.ToString();
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref sgcn, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);
        //        }
        //        else
        //            m.Set(schk, gc = new GefyraColumn(ref _this, ref s, ref mi, ref __s), StringComparison.OrdinalIgnoreCase);

        //        return gc;
        //    }
        //}

        //private static void __CalculateColumnHashKey(ref GefyraTable gt, ref String s, out String s)
        //{
        //    __sb.Clear();

        //    __sb
        //        .Append("gt").Append(CCharacter.Dot).Append("sn").Append(CCharacter.DoubleDot).Append(sSchemaName);

        //    if (__sb.Length > 0)
        //        __sb.Append(CCharacter.Pipe);

        //    __sb
        //        .Append("gt").Append(CCharacter.Dot).Append("n").Append(CCharacter.DoubleDot).Append(sName);

        //    sOut = __sb.ToString();
        //}
    }
}