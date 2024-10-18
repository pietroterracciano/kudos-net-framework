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

namespace Kudos.Databasing.Descriptors
{
	public class DatabaseTableDescriptor : ADatabaseDescriptor
	{
        #region ... static ...

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

        static DatabaseTableDescriptor()
        {
            __sTableName = "TABLE_NAME";
            __sTableSchema = "TABLE_SCHEMA";
            __sEngine = "ENGINE";

            __stdPrefix = "td";
            __ssnPrefix = "sn";
            __snPrefix = "n";
            __m = new Metas(StringComparison.OrdinalIgnoreCase);
            __sb = new StringBuilder();

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

        #region internal void static Get(...)

        internal static void Get(ref IDatabaseHandler? dbh, ref String? ssn, ref String? sn, out DatabaseTableDescriptor? dbtd)
        {
            if (dbh == null)
            {
                dbtd = null;
                return;
            }

            String?[]?
                sa = DatabaseTableUtils.NormalizeNames(ssn, sn);

            if (sa == null || String.IsNullOrWhiteSpace(sa[1]))
            {
                dbtd = null;
                return;
            }

            String? shk;
            __CalculateHashKey(ref sa[0], ref sa[1], out shk);
            if (shk == null)
            {
                dbtd = null;
                return;
            }

            lock (__m)
            {
                dbtd = __m.Get<DatabaseTableDescriptor>(shk);

                if (dbtd != null)
                    return;

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
                    dbqr = dbh.ExecuteQuery(sSQL, 1, kvpa);

                if (dbqr.HasError)
                    return;

                DataRow? dr = DataTableUtils.GetFirstRow(dbqr.Data);

                ssn = DataRowUtils.GetValue<String>(dr, __sTableSchema);
                sn = DataRowUtils.GetValue<String>(dr, __sTableName);

                if(!String.IsNullOrWhiteSpace(ssn) && !String.IsNullOrWhiteSpace(sn))
                    dbtd = new DatabaseTableDescriptor(ref ssn, ref sn, ref shk);

                __m.Set(shk, dbtd);
            }
        }

        #endregion

        #endregion

        public readonly String
            SchemaName;
        public readonly String
            TableName;

        internal DatabaseTableDescriptor(ref String ssn, ref String sn, ref String shk) : base(ref shk)
        {
            SchemaName = ssn;
            TableName = sn;
        }
    }
}