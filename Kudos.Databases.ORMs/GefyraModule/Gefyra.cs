using System.Collections;
using System.Data;
using System.Numerics;
using System.Reflection;
using Kudos.Constants;
using Kudos.Databases.Descriptors;
using Kudos.Databases.Enums;
using Kudos.Databases.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Builts;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databases.ORMs.GefyraModule.Types.Entities;
using Kudos.Reflection.Utils;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using Mysqlx.Prepare;

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

        public static IGefyraTable RequestTable<T>() { GefyraTable gt; GefyraTable.Request<T>(out gt); return gt; }
        public static IGefyraTable RequestTable(Type? t) { GefyraTable gt; GefyraTable.Request(ref t, out gt); return gt; }
        //public static Task<IGefyraTable> RequestTableAsync<T>() { return Task.Run(RequestTable<T>); }
        //public static Task<IGefyraTable> RequestTableAsync(Type? t) { return Task.Run(() => RequestTable(t)); }

        //public static GefyraTable RequestTable(String? sName) { GefyraTable gt; GefyraTable.Request(ref sName, out gt); return gt; }
        //public static GefyraTable RequestTable(String? sSchemaName, String? sName) { GefyraTable gt; GefyraTable.Request(ref sSchemaName, ref sName, out gt); return gt; }\

        #endregion

        #endregion

        #region Parse

        //public static Task<T[]?> ParseAsync<T>(DataTable? dt, GefyraBuilt? gb = null) { return Task.Run(() => Parse<T>(dt, gb)); }
        public static T[]? Parse<T>(DataTable? dt, GefyraBuilt? gb = null)
        {
            return Parse(typeof(T), dt, gb) as T[];
        }
        public static Object[]? Parse(Type? t, DataTable? dt, GefyraBuilt? gb = null)
        {
            if (dt == null || dt.Rows.Count < 1)
                return null;

            Object[]?
               oa0 = ArrayUtils.CreateInstance(t, dt.Rows.Count);

            if (oa0 == null)
                return null;

            Int32
                iNonNullableObjects = 0;

            Object? oi;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                oi = Parse(t, dt.Rows[i], gb);
                if (oi == null) continue;
                oa0[i] = oi; iNonNullableObjects++;
            }

            if (iNonNullableObjects < 1)
                return null;
            else if (dt.Rows.Count - iNonNullableObjects == 0)
                return oa0;

            Object[]?
                oa1 = ArrayUtils.CreateInstance(t, iNonNullableObjects);

            if (oa1 == null)
                return null;

            for (int i = 0; i < oa0.Length; i++)
            {
                if (oa0[i] == null) continue;
                oa1[i] = oa0[i];
            }

            return oa1;
        }

        //public static Task<T?> ParseAsync<T>(DataRow? dr, GefyraBuilt? gb = null) { return Task.Run(() => Parse<T>(dr, gb)); }
        public static T? Parse<T>(DataRow? dr, GefyraBuilt? gb = null)
        {
            return ObjectUtils.Cast<T>(Parse(typeof(T), dr, gb));
        }
        public static Object? Parse(Type? t, DataRow? dr, GefyraBuilt? gb = null)
        {
            if (dr == null)
                return null;

            IGefyraTable
                gt = Gefyra.RequestTable(t);

            if (gt == GefyraTable.Invalid)
                return null;

            Object?
                o =
                    ReflectionUtils.InvokeConstructor(ReflectionUtils.GetConstructor(t, CBindingFlags.Instance));

            if (o == null)
                return null;

            Metas? m;

            if (gb != null && gb.OutputColumns.Length > 0)
            {
                m = new Metas(gb.OutputColumns.Length, StringComparison.OrdinalIgnoreCase);

                for (int i = 0; i < gb.OutputColumns.Length; i++)
                {
                    if (!gb.OutputColumns[i].HasAlias) continue;
                    m.Set(gb.OutputColumns[i].Alias, gb.OutputColumns[i]);
                }
            }
            else
                m = null;

            IGefyraColumn? gci;

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                gci =
                    m != null
                        ? m.Get<IGefyraColumn>(dr.Table.Columns[i].ColumnName)
                        : null;

                if (gci == null)
                    gci = gt.RequestColumn(dr.Table.Columns[i].ColumnName);

                if (gci == GefyraColumn.Invalid || !gci.HasDeclaringMember)
                    continue;

                ReflectionUtils.SetMemberValue(o, gci.DeclaringMember, DataRowUtils.GetValue(dr, i), true);
            }

            return o;
        }

        #endregion

        #region Fit

        //public static Task FitAsync<T>(IDatabaseHandler? dbh, ref T? t) { T? t0 = t; return Task.Run(() => {  Fit<T>(dbh, ref t0); }); }
        //public static Task<T?> FitAsync<T>(IDatabaseHandler? dbh, T? t) { return Task.Run(() => Fit<T>(dbh, t)); }
        public static void Fit<T>(IDatabaseHandler? dbh, ref T? t) { T? t0; __Fit(ref dbh, ref t, out t0, true); }
        public static T? Fit<T>(IDatabaseHandler? dbh, T? t) { T? t0; __Fit(ref dbh, ref t, out t0, false); return t0; }
        private static void __Fit<T>(ref IDatabaseHandler? dbh, ref T? t, out T? t0, Boolean bInPlace)
        { 
            if (dbh == null)
            {
                t0 = bInPlace ? t : default(T);
                return;
            }

            IGefyraTable
                gt = Gefyra.RequestTable<T>();

            if (gt == GefyraTable.Invalid)
            {
                t0 = bInPlace ? t : default(T);
                return;
            }
            else if (bInPlace)
                t0 = t;
            else
            {
                t0 = ReflectionUtils.Copy<T>(t, CGefyraBindingFlags.OnGetMembers);
                if (t0 == null) return;
            }

            MemberInfo[]?
                mia = ReflectionUtils.GetMembers<T>(CGefyraBindingFlags.OnGetMembers);

            if (mia == null) return;

            IGefyraColumn? gci;
            DatabaseColumnDescriptor? dbcdi;
            Object? oi;
            for (int i=0; i<mia.Length; i++)
            {
                gci = gt.RequestColumn(mia[i].Name);
                if (gci == GefyraColumn.Invalid || !gci.HasDeclaringMember) continue;
                dbcdi = dbh.GetColumnDescriptor(gci.DeclaringTable.SchemaName, gci.DeclaringTable.Name, gci.Name);
                if (dbcdi == null) continue;
                oi = ReflectionUtils.GetMemberValue(t, mia[i]);
                __Fit(ref dbcdi, ref oi, out oi);
                ReflectionUtils.SetMemberValue(t0, mia[i], oi, true);
            }
        }

        //public static Task<Object?> FitAsync(DatabaseColumnDescriptor dbcd, Object? o) { return Task.Run(() => Fit(dbcd, o)); }
        public static Object? Fit(DatabaseColumnDescriptor? dbcd, Object? o) { Object? oOut; __Fit(ref dbcd, ref o, out oOut); return oOut; }
        private static void __Fit(ref DatabaseColumnDescriptor? dbcd, ref Object? oIn, out Object? oOut)
        {
            if (dbcd == null)
            {
                oOut = oIn;
                return;
            }

            oOut = ObjectUtils.Parse(dbcd.DataTypeDescriptor.SimplexType, oIn);

            #region UInt16
            if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt16)
            {
                UInt16? i = oOut as UInt16?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region UInt32
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt32)
            {
                UInt32? i = oOut as UInt32?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region UInt64
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt64)
            {
                UInt64? i = oOut as UInt64?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region Int16
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int16)
            {
                Int16? i = oOut as Int16?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region Int32
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int32)
            {
                Int32? i = oOut as Int32?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region Int64
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int64)
            {
                Int64? i = oOut as Int64?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region Single
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.Single)
            {
                Single? i = oOut as Single?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region Double
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.Double)
            {
                Double? i = oOut as Double?;
                if (i != null)
                {
                    if (i > dbcd.DataTypeDescriptor.MaxValue) oOut = dbcd.DataTypeDescriptor.MaxValue;
                    else if (i < dbcd.DataTypeDescriptor.MinValue) oOut = dbcd.DataTypeDescriptor.MinValue;
                }
            }
            #endregion
            #region String
            else if (dbcd.DataTypeDescriptor.SimplexType == CType.String)
            {
                Int32? i = Int32Utils.Parse(dbcd.CurrentMaxLength);
                if (i != null) oOut = StringUtils.Truncate(oOut as String, i.Value);
            }
            #endregion

            if (oOut == null)
                oOut = dbcd.DefaultValue;

            if
            (
                !dbcd.IsRequired
                || oOut != null
                || dbcd.IsNullable
            )
                return;

            oOut = ReflectionUtils.CreateInstance(dbcd.DataTypeDescriptor.SimplexType);

            //if (oOut != null)
            //    return;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt16)
            //    oOut = (UInt16)0;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt32)
            //    oOut = 0U;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.UInt64)
            //    oOut = 0UL;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int16)
            //    oOut = (Int16)0;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int32)
            //    oOut = 0;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Int64)
            //    oOut = 0L;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Single)
            //    oOut = 0F;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Double)
            //    oOut = 0D;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.Decimal)
            //    oOut = 0M;
            //else if (dbcd.DataTypeDescriptor.SimplexType == CType.String)
            //    oOut = String.Empty;
        }

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
