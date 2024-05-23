using System;
using Kudos.Databases.Constants;
using System.Data;
using Kudos.Utils.Collections;
using Kudos.Constants;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Results;
using Kudos.Databases.Utils;
using Kudos.Types;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Intrinsics.X86;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System.ComponentModel.DataAnnotations;
using Kudos.Databases.Constants.Columns;
using Kudos.Databases.Enums;
using static Mysqlx.Expect.Open.Types.Condition.Types;
using Kudos.Utils;
using System.Data.SqlTypes;

namespace Kudos.Databases.Descriptors
{
	public class DatabaseColumnDescriptor
        : ADatabaseDescriptor
	{
        #region ... static ...

        private static readonly Int32
          __iInt8__MinValue = -128,
          __iInt8__MaxValue = 127,
          __iInt24_MinValue = -8388607,
          __iInt24_MaxValue = 8388607;

        private static readonly UInt32
           __iUInt8__MaxValue = 255,
           __iUInt24_MaxValue = UInt32Utils.NNParse(Math.Pow(2, 24)) - 1,
           __iUInt30_MaxValue = UInt32Utils.NNParse(Math.Pow(2, 30)) - 1;

        private static readonly Metas
            __m;
        private static readonly StringBuilder
            __sb;
        private static readonly String
            __sTableSchema,
            __sTableName,

            __sColumnName,
            __sOrdinalPosition,
            __sColumnDefault,
            __sIsNullable,
            __sDataType,
            __sCharacterMaximumLength,
            //__sCharacterOctetLength,
            //__sNumericPrecision,
            //__sNumericScale,
            //__sDateTimePrecision,
            //__sCharacterSetName,
            //__sCollationName,
            __sColumnType,
            __sColumnKey,
            __sExtra,


            __scdPrefix,
            __snPrefix,
            __sSQL;

        static DatabaseColumnDescriptor()
        {
            __sTableName = "TABLE_NAME";
            __sTableSchema = "TABLE_SCHEMA";

            __sColumnName = "COLUMN_NAME";
            __sOrdinalPosition = "ORDINAL_POSITION";
            __sColumnDefault = "COLUMN_DEFAULT";
            __sIsNullable = "IS_NULLABLE";
            __sDataType = "DATA_TYPE";
            __sCharacterMaximumLength = "CHARACTER_MAXIMUM_LENGTH";
            //__sCharacterOctetLength = "CHARACTER_OCTET_LENGTH";
            //__sNumericPrecision = "NUMERIC_PRECISION";
            //__sNumericScale = "NUMERIC_SCALE";
            __sColumnType = "COLUMN_TYPE";
            __sColumnKey = "COLUMN_KEY";
            __sExtra = "EXTRA";

            __scdPrefix = "cd";
            __snPrefix = "n";

            __m = new Metas(StringComparison.OrdinalIgnoreCase);
            __sb = new StringBuilder();

            __sSQL =
                "SELECT * FROM information_schema.COLUMNS WHERE " + __sTableName + " = @" + __sTableName + " AND " + __sTableSchema + " = @" + __sTableSchema;
        }

        #region private void __CalculateHashKey(...)

        private static void __CalculateHashKey(ref String? sn, out String? s)
        {
            if (String.IsNullOrWhiteSpace(sn))
            {
                s = null;
                return;
            }

            lock (__sb)
            {
                s =
                    __sb
                        .Clear()
                        .Append(__scdPrefix).Append(CCharacter.Dot).Append(sn)
                        .ToString();
            }
        }

        #endregion

        #region internal void static Get(...)

        internal static void Get(ref IDatabaseHandler? dbh, ref DatabaseTableDescriptor? dbtd, ref String? sn, out DatabaseColumnDescriptor? dbcd)
        {
            DatabaseColumnDescriptor[]? dbcda;
            Get(ref dbh, ref dbtd, out dbcda);
            if (dbtd == null) { dbcd = null; return; }

            String? shk;
            __CalculateHashKey(ref sn, out shk);

            lock(__m)
            {
                Metas?
                    m = __m.Get<Metas>(dbtd.HashKey);

                if(m == null) { dbcd = null; return; }

                dbcd = m.Get<DatabaseColumnDescriptor>(shk);
            }
        }

        internal static void Get(ref IDatabaseHandler? dbh, ref DatabaseTableDescriptor? dbtd, out DatabaseColumnDescriptor[]? dbcda)
        {
            if (dbh == null || dbtd == null)
            {
                dbcda = null;
                return;
            }

            lock (__m)
            {
                Metas?
                    m = __m.Get<Metas>(dbtd.HashKey);

                if (m != null)
                {
                    dbcda = m.Get<DatabaseColumnDescriptor[]>(String.Empty);
                    return;
                }

                KeyValuePair<String, Object?>[]
                   kvpa = new KeyValuePair<string, object?>[2];

                kvpa[0] = new KeyValuePair<string, object?>(__sTableName, dbtd.TableName);
                kvpa[1] = new KeyValuePair<string, object?>(__sTableSchema, dbtd.SchemaName);

                DatabaseQueryResult
                    dbqr = dbh.ExecuteQuery(__sSQL, kvpa);

                if (dbqr.HasError)
                {
                    dbcda = null;
                    return;
                }

                m = new Metas(StringComparison.OrdinalIgnoreCase);
                __m.Set(dbtd.HashKey, m);

                if (dbqr.HasData)
                {
                    DatabaseColumnDescriptor? dbcdi;
                    List<DatabaseColumnDescriptor> li = new List<DatabaseColumnDescriptor>(dbqr.Data.Rows.Count);
                    for (int i = 0; i < dbqr.Data.Rows.Count; i++)
                    {
                        __Get(ref dbtd, dbqr.Data.Rows[i], out dbcdi);
                        if (dbcdi == null) continue;
                        li.Add(dbcdi); m.Set(dbcdi.HashKey, dbcdi);
                    }

                    dbcda = li.ToArray();
                }
                else
                    dbcda = null;

                m.Set(String.Empty, dbcda);

                dbcda = null;
            }
        }

        private static void __Get(ref DatabaseTableDescriptor dbtd, DataRow? dr, out DatabaseColumnDescriptor? dbcd)
        {
            String?
                sColumnName,
                shk;

            #region ColumnName, HashKey

            sColumnName = DataRowUtils.GetValue<String>(dr, __sColumnName);

            __CalculateHashKey(ref sColumnName, out shk);

            if (String.IsNullOrWhiteSpace(shk))
            {
                dbcd = null;
                return;
            }

            #endregion

            UInt16?
                uiOrdinalPosition;

            #region OrdinalPosition

            uiOrdinalPosition = DataRowUtils.GetValue<UInt16>(dr, __sOrdinalPosition);

            if (uiOrdinalPosition == null || uiOrdinalPosition.Value < 1)
            {
                dbcd = null;
                return;
            }

            #endregion

            Boolean
                bIsNullable;

            #region IsNullable

            String?
                sIsNullable = DataRowUtils.GetValue<String>(dr, __sIsNullable);

            if (String.IsNullOrWhiteSpace(sIsNullable))
            {
                dbcd = null;
                return;
            }

            bIsNullable = sIsNullable.Trim().Equals(CDatabaseIsNullable.Yes, StringComparison.OrdinalIgnoreCase);

            #endregion

            DatabaseDataTypeDescriptor?
                dbdtd;

            UInt32?
                uiMaxLength;

            #region DataType

            String?
                sDataType = DataRowUtils.GetValue<String>(dr, __sDataType);

            if(String.IsNullOrWhiteSpace(sDataType))
            {
                dbcd = null;
                return;
            }

            sDataType = sDataType.ToLower().Trim();

            switch (sDataType)
            {
                case CDatabaseDataType.Tinyint:
                case CDatabaseDataType.Smallint:
                case CDatabaseDataType.Mediumint:
                case CDatabaseDataType.Int:
                case CDatabaseDataType.Bigint:
                case CDatabaseDataType.Double:
                    uiMaxLength = null;

                    String?
                        sColumnType = DataRowUtils.GetValue<String>(dr, __sColumnType);

                    Boolean
                        bIsColumnTypeUnsigned = !String.IsNullOrWhiteSpace(sColumnType) && sColumnType.ToLower().Trim().Contains(CDatabaseColumnType.Unsigned);

                    switch (sDataType)
                    {
                        case CDatabaseDataType.Tinyint:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedTinyInteger
                                    : DatabaseDataTypeDescriptor.TinyInteger;
                            break;
                        case CDatabaseDataType.Smallint:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedSmallInteger
                                    : DatabaseDataTypeDescriptor.Integer;
                            break;
                        case CDatabaseDataType.Mediumint:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedMediumInteger
                                    : DatabaseDataTypeDescriptor.MediumInteger;
                            break;
                        case CDatabaseDataType.Int:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedInteger
                                    : DatabaseDataTypeDescriptor.Integer;
                            break;
                        case CDatabaseDataType.Bigint:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedBigInteger
                                    : DatabaseDataTypeDescriptor.BigInteger;
                            break;
                        case CDatabaseDataType.Double:
                            dbdtd =
                                bIsColumnTypeUnsigned
                                    ? DatabaseDataTypeDescriptor.UnsignedDouble
                                    : DatabaseDataTypeDescriptor.Double;
                            break;
                        default:
                            dbdtd = null;
                            break;
                    }
                    break;
                case CDatabaseDataType.Json:
                    uiMaxLength = null;
                    dbdtd = DatabaseDataTypeDescriptor.Json;
                    break;
                case CDatabaseDataType.Varchar:
                case CDatabaseDataType.Text:
                case CDatabaseDataType.Mediumtext:
                case CDatabaseDataType.Longtext:
                    uiMaxLength = DataRowUtils.GetValue<UInt32>(dr, __sCharacterMaximumLength);

                    switch (sDataType)
                    {
                        case CDatabaseDataType.Varchar:
                            dbdtd = DatabaseDataTypeDescriptor.VariableChar;
                            break;
                        case CDatabaseDataType.Text:
                            dbdtd = DatabaseDataTypeDescriptor.Text;
                            break;
                        case CDatabaseDataType.Mediumtext:
                            dbdtd = DatabaseDataTypeDescriptor.MediumText;
                            break;
                        case CDatabaseDataType.Longtext:
                            dbdtd = DatabaseDataTypeDescriptor.LongText;
                            break;
                        default:
                            dbdtd = null;
                            break;
                    }

                    break;
                default:
                    uiMaxLength = null;
                    dbdtd = null;
                    break;
            }

            if (dbdtd == null)
            {
                dbcd = null;
                return;
            }

            #endregion

            EDatabaseExtra
                eExtra;

            #region Extra

            String?
                sExtra = DataRowUtils.GetValue<String>(dr, __sExtra);

            if (CDatabaseExtra.AutoIncrement.Equals(sExtra, StringComparison.OrdinalIgnoreCase))
                eExtra = EDatabaseExtra.AutoIncrement;
            else
                eExtra = EDatabaseExtra.None;

            #endregion

            EDatabaseKey
                eKey;

            #region Key

            String?
                sColumnKey = DataRowUtils.GetValue<String>(dr, __sColumnKey);

            if (CDatabaseKey.Primary.Equals(sColumnKey, StringComparison.OrdinalIgnoreCase))
                eKey = EDatabaseKey.Primary;
            else if (CDatabaseKey.Unique.Equals(sColumnKey, StringComparison.OrdinalIgnoreCase))
                eKey = EDatabaseKey.Unique;
            else if (CDatabaseKey.Multiple.Equals(sColumnKey, StringComparison.OrdinalIgnoreCase))
                eKey = EDatabaseKey.Multiple;
            else
                eKey = EDatabaseKey.None;

            #endregion

            Object?
                oDefaultValue;

            #region DefaultValue

            oDefaultValue = DataRowUtils.GetValue(dr, __sColumnDefault);
            oDefaultValue = ObjectUtils.Parse(dbdtd.SimplexType, oDefaultValue);

            #endregion

            Boolean
                bIsRequired;

            #region IsRequired

            bIsRequired =
                !bIsNullable
                && oDefaultValue == null
                && !eExtra.HasFlag(EDatabaseExtra.AutoIncrement);

            #endregion

            dbcd =
                new DatabaseColumnDescriptor
                (
                    ref dbtd,
                    ref sColumnName,
                    ref bIsNullable,
                    ref uiOrdinalPosition,
                    ref dbdtd,
                    ref uiMaxLength,
                    ref eExtra,
                    ref eKey,
                    ref oDefaultValue,
                    ref bIsRequired,
                    ref shk
                );
        }

        #endregion

        #endregion

        public readonly String Name;
        public readonly Boolean IsNullable, IsRequired;
        public readonly UInt16 OrdinalPosition;
        public readonly DatabaseTableDescriptor TableDescriptor;
        public readonly DatabaseDataTypeDescriptor DataTypeDescriptor;
        public readonly Object? DefaultValue;

        //public readonly Type Type;
        //public readonly Double? MinValue, MaxValue;
        public readonly UInt32? CurrentMaxLength;

        public readonly EDatabaseExtra Extra;
        public readonly EDatabaseKey Key;

        private DatabaseColumnDescriptor
            (
                ref DatabaseTableDescriptor dbtd,
                ref String sn,
                ref Boolean bis,
                ref UInt16? uiop,
                ref DatabaseDataTypeDescriptor dbdtd,
                ref UInt32? uimxl,
                ref EDatabaseExtra edbe,
                ref EDatabaseKey edbk,
                ref Object? odf,
                ref Boolean bir,
                ref String shk
            )
        :
            base(ref shk)
		{
            TableDescriptor = dbtd;
            Name = sn;
            IsNullable = bis;
            OrdinalPosition = uiop.Value;
            DataTypeDescriptor = dbdtd;
            //Type = dbdtd.Type;
            //MinValue = dbdtd.MinValue;
            //MaxValue = dbdtd.MaxValue;
            CurrentMaxLength = uimxl != null ? uimxl.Value : dbdtd.MaxLength;
            Extra = edbe;
            Key = edbk;

            DefaultValue = odf;
            IsRequired = bir;
        }
	}
}

