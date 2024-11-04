using System;
using Kudos.Utils.Texts;
using System.Collections.Generic;
using System.Data;
using Kudos.Utils.Collections;
using Kudos.Databasing.Utils;
using Kudos.Databasing.Constants;
using Kudos.Constants;
using Kudos.Databasing.Results;
using Kudos.Databasing.Controllers;
using Kudos.Databasing.Interfaces;
using Kudos.Types;
using System.Text;
using System.Threading.Tasks;
using Kudos.Utils;
using System.Threading;

namespace Kudos.Databasing.Descriptors
{
	public class DatabaseTableDescriptor : ADatabaseDescriptor
	{
        #region ... static ...

        private static readonly SemaphoreSlim
            __ss;
        private static readonly Metas
           __m;
        private static readonly StringBuilder
            __sb;
        private static readonly String
            __sTableSchema,
            __sTableName,
            __sEngine,

            __stdPrefix,
            __snPrefix,
            __ssnPrefix,
            __sSQL;

        private static String
            __sNullable;

        static DatabaseTableDescriptor()
        {
            __sNullable = null;

            __sTableName = "TABLE_NAME";
            __sTableSchema = "TABLE_SCHEMA";
            __sEngine = "ENGINE";

            __stdPrefix = "td";
            __ssnPrefix = "sn";
            __snPrefix = "n";
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
            __sb = new StringBuilder();

            __ss = new SemaphoreSlim(1);

            __sSQL =
                "SELECT * FROM information_schema.TABLES WHERE " + __sTableName + " = @" + __sTableName;
        }

        #region private void __CalculateHashKey(...)

        private static void __CalculateHashKey(ref String? ssn, ref String? sn, out String? s)
        {
            if (String.IsNullOrWhiteSpace(sn))
            {
                s = null;
                return;
            }

            lock (__sb)
            {
                __sb.Clear();

                if (!string.IsNullOrWhiteSpace(ssn))
                    __sb
                        .Append(__stdPrefix).Append(CCharacter.Dot).Append(__ssnPrefix).Append(CCharacter.DoubleDot).Append(ssn)
                        .Append(CCharacter.Pipe);

                __sb
                    .Append(__stdPrefix).Append(CCharacter.Dot).Append(__snPrefix).Append(CCharacter.DoubleDot).Append(sn);

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

        #region internal static ... Get...(...)

        internal static void Get(ref IDatabaseHandler? dbh, ref String? sn, out DatabaseTableDescriptor? dtd)
        {
            Get(ref dbh, ref __sNullable, ref sn, out dtd);
        }
        internal static void Get(ref IDatabaseHandler? dbh, ref String? ssn, ref String? sn, out DatabaseTableDescriptor? dtd)
        {
            Task<DatabaseTableDescriptor?> t = GetAsync(dbh, ssn, sn);
            t.Wait();
            dtd = t.Result;
        }
        internal static async Task<DatabaseTableDescriptor?> GetAsync(IDatabaseHandler? dbh, String? sn)
        {
            return await GetAsync(dbh, null, sn);
        }
        internal static async Task<DatabaseTableDescriptor?> GetAsync(IDatabaseHandler? dbh, String? ssn, String? sn)
        {
            if (dbh == null)
                return null;

            String?[]?
                sa = DatabaseTableUtils.NormalizeNames(ssn, sn);

            if (sa == null || String.IsNullOrWhiteSpace(sa[1]))
                return null;

            String? shk;
            __CalculateHashKey(ref sa[0], ref sa[1], out shk);
            if (shk == null)
                return null;

            await SemaphoreUtils.WaitSemaphoreAsync(__ss);

            DatabaseTableDescriptor? dbtd = __m.Get<DatabaseTableDescriptor>(shk);

            if (dbtd != null)
            {
                SemaphoreUtils.ReleaseSemaphore(__ss);
                return dbtd;
            }

            String
                sSQL;

            KeyValuePair<String, Object?>[]
                kvpa;

            lock (__sb)
            {
                __sb
                    .Clear()
                    .Append(__sSQL);

                if (!String.IsNullOrWhiteSpace(sa[0]))
                {
                    kvpa = new KeyValuePair<string, object?>[2];

                    __sb
                        .Append(CCharacter.Space).Append(CDatabaseClausole.And)
                        .Append(CCharacter.Space).Append(__sTableSchema)
                        .Append(CCharacter.Space).Append(CCharacter.Equal)
                        .Append(CCharacter.Space).Append(CCharacter.At).Append(__sTableSchema);
                }
                else
                    kvpa = new KeyValuePair<string, object?>[1];

                __sb
                    .Append(CCharacter.Space).Append(CDatabaseClausole.Limit)
                    .Append(CCharacter.Space).Append(1);

                sSQL = __sb.ToString();
            }


            if (kvpa.Length > 0)
                kvpa[0] = new KeyValuePair<string, object?>(__sTableName, sa[1]);
            if (kvpa.Length > 1)
                kvpa[1] = new KeyValuePair<string, object?>(__sTableSchema, sa[0]);

            DatabaseQueryResult
                dbqr = await dbh.ExecuteQueryAsync(sSQL, 1, kvpa);

            if (dbqr.HasError)
            {
                SemaphoreUtils.ReleaseSemaphore(__ss);
                return dbtd;
            }

            DataRow? dr = DataTableUtils.GetFirstRow(dbqr.Data);

            ssn = DataRowUtils.GetValue<String>(dr, __sTableSchema);
            sn = DataRowUtils.GetValue<String>(dr, __sTableName);

            if(!String.IsNullOrWhiteSpace(ssn) && !String.IsNullOrWhiteSpace(sn))
                dbtd = new DatabaseTableDescriptor(ref ssn, ref sn, ref shk);

            __m.Set(shk, dbtd);

            SemaphoreUtils.ReleaseSemaphore(__ss);

            return dbtd;
        }

        #endregion

        #endregion

        private readonly Metas
            _m;
        private DatabaseColumnDescriptor[]?
            _dcda;
        public readonly String
            SchemaName;
        public readonly String
            TableName;

        internal DatabaseTableDescriptor(ref String ssn, ref String sn, ref String shk) : base(ref shk)
        {
            _m = new Metas(StringComparison.OrdinalIgnoreCase);
            SchemaName = ssn;
            TableName = sn;
        }

        internal void Eject(DatabaseColumnDescriptor[]? dcda)
        {
            if (dcda == null) return;
            lock (_m)
            {
                _dcda = dcda;
                for (int i = 0; i < dcda.Length; i++) _m.Set(dcda[i].Name, dcda[i]);
            }
        }

        public DatabaseColumnDescriptor? GetColumnDescriptor(String? sn)
        { 
            lock (_m) { return _m.Get<DatabaseColumnDescriptor>(sn); }
        }

        public DatabaseColumnDescriptor[]? GetColumnsDescriptors()
        {
            lock (_m) { return _dcda; }
        }
    }
}