using Kudos.Constants;
using Kudos.Databases.Constants;
using Kudos.Databases.Enums.Columns;
using Kudos.Databases.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Datas;
using Kudos.Utils.Members;
using Kudos.Utils.Numerics.Integers;
using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Kudos.Databases.Models.Schemas.Columns
{
    public class DatabaseInformationSchemaColumnDescriptorModel
    {
        private static readonly Int32
           __iInt8__MinValue = -128,
           __iInt8__MaxValue = 127,
           __iInt24_MinValue = -8388607,
           __iInt24_MaxValue = 8388607;

        private static readonly UInt32
           __iUInt8__MaxValue = 255,
           __iUInt24_MaxValue = UInt32Utils.From(Math.Pow(2, 24)) - 1,
           __iUInt30_MaxValue = UInt32Utils.From(Math.Pow(2, 30)) - 1;

        private static readonly String
            __sDefaultNonNullableJSONValue = "{}";

        public readonly String Name;
        public readonly UInt16 OrdinalPosition;
        public UInt32? Length { get; private set; }
        public Double? MaxValue { get; private set; }
        public Double? MinValue { get; private set; }
        public readonly EDBColumnExtra Extras;
        public EDBColumnType Type { get; private set; }
        public readonly EDBColumnKey Key;
        public Type? ValueType { get; private set; }
        public Object? DefaultValue { get; private set; }
        public Boolean IsNullable { get; private set; }
        public Boolean IsRequired { get; private set; }
        public readonly DatabaseInformationSchemaColumnsModel Columns;
        private readonly Boolean _bIsChar;

        private DatabaseInformationSchemaColumnDescriptorModel(ref EDBColumnType eType)
        {
            Type = eType;
            Name = String.Empty;
            Extras = EDBColumnExtra.None;
            Key = EDBColumnKey.None;
            Finalize();
        }

        private DatabaseInformationSchemaColumnDescriptorModel(ref DatabaseInformationSchemaColumnsModel mColumns, ref DataRow oDataRow)
        {
            Columns = mColumns;

            #region Fetch OrdinalPosition

            OrdinalPosition = UInt16Utils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.ORDINAL_POSITION));

            #endregion

            #region Fetch Name

            Name = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.COLUMN_NAME));
            if (Name == null) Name = String.Empty;

            #endregion

            #region Fetch DefaultValue

            DefaultValue = DataRowUtils.GetValue(oDataRow, CDBISColumnName.COLUMN_DEFAULT);

            #endregion

            #region Fetch Type

            String sDATA_TYPE = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.DATA_TYPE));
            if (!String.IsNullOrWhiteSpace(sDATA_TYPE))
            {
                sDATA_TYPE = sDATA_TYPE.ToLower();
                switch (sDATA_TYPE)
                {
                    case CDBISDataType.tinyint:
                    case CDBISDataType.smallint:
                    case CDBISDataType.mediumint:
                    case CDBISDataType._int:
                    case CDBISDataType.bigint:
                    case CDBISDataType._double:
                        String sCOLUMN_TYPE = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.COLUMN_TYPE));
                        Boolean bIsColumnTypeUnsigned = !String.IsNullOrWhiteSpace(sCOLUMN_TYPE) && sCOLUMN_TYPE.ToLower().Contains(CDBISColumnType.unsigned);

                        switch (sDATA_TYPE)
                        {
                            case CDBISDataType.tinyint:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedTinyInteger : EDBColumnType.TinyInteger;
                                break;
                            case CDBISDataType.smallint:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedSmallInteger : EDBColumnType.SmallInteger;
                                break;
                            case CDBISDataType.mediumint:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedMediumInteger : EDBColumnType.MediumInteger;
                                break;
                            case CDBISDataType._int:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedInteger : EDBColumnType.Integer;
                                break;
                            case CDBISDataType.bigint:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedBigInteger : EDBColumnType.BigInteger;
                                break;
                            case CDBISDataType._double:
                                Type = bIsColumnTypeUnsigned ? EDBColumnType.UnsignedDouble : EDBColumnType.Double;
                                break;
                        }
                        break;
                    case CDBISDataType.json:
                        Type = EDBColumnType.Json;
                        break;
                    case CDBISDataType.varchar:
                    case CDBISDataType.text:
                    case CDBISDataType.mediumtext:
                    case CDBISDataType.longtext:
                        _bIsChar = true;

                        String sCHARACTER_MAXIMUM_LENGTH = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.CHARACTER_MAXIMUM_LENGTH));
                        Length = UInt32NUtils.From(sCHARACTER_MAXIMUM_LENGTH);

                        switch (sDATA_TYPE)
                        {
                            case CDBISDataType.varchar:
                                Type = EDBColumnType.VariableChar;
                                break;
                            case CDBISDataType.text:
                                Type = EDBColumnType.Text;
                                break;
                            case CDBISDataType.mediumtext:
                                Type = EDBColumnType.MediumText;
                                break;
                            case CDBISDataType.longtext:
                                Type = EDBColumnType.LongText;
                                break;
                        }

                        break;
                    default:
                        Type = EDBColumnType.Unknown;
                        break;
                }
            }
            else
                Type = EDBColumnType.Unknown;

            #endregion

            #region Fetch IsNullable

            String sIS_NULLABLE = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.IS_NULLABLE));
            IsNullable = String.IsNullOrWhiteSpace(sIS_NULLABLE) || sIS_NULLABLE.ToUpper().Contains(CDBISIsNullable.YES);

            #endregion

            #region Fetch Extras

            String sEXTRA = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.EXTRA));

            if(!String.IsNullOrWhiteSpace(sEXTRA))
            {
                sEXTRA = sEXTRA.ToLower();
                if (sEXTRA.Contains(CDBISColumnExtra.auto_increment))
                    Extras = EDBColumnExtra.AutoIncrement;
                else
                    Extras = EDBColumnExtra.None;
            }
            else
                Extras = EDBColumnExtra.None;

            #endregion

            #region Fetch Key

            String sCOLUMN_KEY = StringUtils.From(DataRowUtils.GetValue(oDataRow, CDBISColumnName.COLUMN_KEY));
            if (!String.IsNullOrWhiteSpace(sCOLUMN_KEY))
            {
                sCOLUMN_KEY = sCOLUMN_KEY.ToUpper();
                if (sCOLUMN_KEY.Contains(CDBISColumnKey.PRI))
                    Key = EDBColumnKey.Primary;
                else if (sCOLUMN_KEY.Contains(CDBISColumnKey.UNI))
                    Key = EDBColumnKey.Unique;
                else
                    Key = EDBColumnKey.None;
            }
            else
                Key = EDBColumnKey.None;

            #endregion

            Finalize();
        }

        private void Finalize()
        {
            switch (Type)
            {
                case EDBColumnType.UnsignedTinyInteger:
                    ValueType = CType.UInt16;
                    MinValue = UInt16.MinValue;
                    MaxValue = __iUInt8__MaxValue;
                    break;
                case EDBColumnType.TinyInteger:
                    ValueType = CType.Int16;
                    MinValue = __iInt8__MinValue;
                    MaxValue = __iInt8__MaxValue;
                    break;
                case EDBColumnType.UnsignedSmallInteger:
                    ValueType = CType.UInt16;
                    MinValue = UInt16.MinValue;
                    MaxValue = UInt16.MaxValue;
                    break;
                case EDBColumnType.SmallInteger:
                    ValueType = CType.Int16;
                    MinValue = Int16.MinValue;
                    MaxValue = Int16.MaxValue;
                    break;
                case EDBColumnType.UnsignedMediumInteger:
                    ValueType = CType.UInt32;
                    MinValue = UInt32.MinValue;
                    MaxValue = __iUInt24_MaxValue;
                    break;
                case EDBColumnType.MediumInteger:
                    ValueType = CType.Int32;
                    MinValue = __iInt24_MinValue;
                    MaxValue = __iInt24_MaxValue;
                    break;
                case EDBColumnType.UnsignedInteger:
                    ValueType = CType.UInt32;
                    MinValue = UInt32.MinValue;
                    MaxValue = UInt32.MaxValue;
                    break;
                case EDBColumnType.Integer:
                    ValueType = CType.Int32;
                    MinValue = Int32.MinValue;
                    MaxValue = Int32.MaxValue;
                    break;
                case EDBColumnType.UnsignedBigInteger:
                    ValueType = CType.UInt64;
                    MinValue = UInt64.MinValue;
                    MaxValue = UInt64.MaxValue;
                    break;
                case EDBColumnType.BigInteger:
                    ValueType = CType.Int64;
                    MinValue = Int64.MinValue;
                    MaxValue = Int64.MaxValue;
                    break;
                case EDBColumnType.UnsignedDouble:
                case EDBColumnType.Double:
                    ValueType = CType.Double;
                    MinValue = Double.MinValue;
                    MaxValue = Double.MaxValue;
                    break;
                case EDBColumnType.Json:
                case EDBColumnType.VariableChar:
                case EDBColumnType.Text:
                case EDBColumnType.MediumText:
                case EDBColumnType.LongText:

                    switch (Type)
                    {
                        case EDBColumnType.VariableChar:
                            ValueType = CType.String;
                            if(Length == null)
                                Length = UInt16.MaxValue;
                            break;
                        case EDBColumnType.Text:
                            ValueType = CType.String;
                            if (Length == null)
                                Length = UInt16.MaxValue;
                            break;
                        case EDBColumnType.MediumText:
                            ValueType = CType.String;
                            if (Length == null)
                                Length = __iUInt24_MaxValue;
                            break;
                        case EDBColumnType.LongText:
                            ValueType = CType.String;
                            if (Length == null)
                                Length = UInt32.MaxValue;
                            break;
                        case EDBColumnType.Json:
                            ValueType = CType.String;
                            if (Length == null)
                                Length = __iUInt30_MaxValue;
                            break;
                    }

                    break;
            }

            DefaultValue =
                ObjectUtils.ChangeType(DefaultValue, ValueType);

            IsRequired =
                !IsNullable
                && DefaultValue == null
                && !Extras.HasFlag(EDBColumnExtra.AutoIncrement);
        }

        public static DatabaseInformationSchemaColumnDescriptorModel New(MemberInfo oMember)
        {
            EDBColumnType eType = DBColumnTypeUtils.ToEnum(MemberUtils.GetValueType(oMember));
            return eType != EDBColumnType.Unknown ? new DatabaseInformationSchemaColumnDescriptorModel(ref eType) : null;
        }

        internal static DatabaseInformationSchemaColumnDescriptorModel New(ref DatabaseInformationSchemaColumnsModel mColumns, ref DataRow? oDataRow)
        {
            return oDataRow != null ? new DatabaseInformationSchemaColumnDescriptorModel(ref mColumns, ref oDataRow) : null;
        }

        //public Object? PrepareValue(Object? oValue)
        //{
        //    Object oPrepared;

        //    switch (Type)
        //    {
        //        case EDBColumnType.Json:
        //            oPrepared = JSONUtils.Serialize(oValue);
        //            break;

        //        default:
        //            if(_bIsChar)
        //            {
        //                ICollection oCollection = CollectionUtils.Cast(oValue);
        //                if (oCollection != null)
        //                    oValue = JSONUtils.Serialize(oValue);
        //            }

        //            oPrepared = ObjectUtils.ChangeType(oValue, ValueType);

        //            if(_bIsChar)
        //            {
        //                if (MaxLength != null)
        //                    oPrepared = StringUtils.Truncate(oPrepared as String, MaxLength.Value);
        //            }
        //            break;
        //    }

        //    if (oPrepared != null)
        //        return oPrepared;
        //    else if (DefaultValue != null)
        //        oPrepared = DefaultValue;
        //    else if (!IsNullable)
        //        switch (Type)
        //        {
        //            case EDBColumnType.Json:
        //                oPrepared = __sDefaultNonNullableJSONValue;
        //                break;
        //            case EDBColumnType.UnsignedTinyInteger:
        //            case EDBColumnType.TinyInteger:
        //            case EDBColumnType.UnsignedSmallInteger:
        //            case EDBColumnType.SmallInteger:
        //            case EDBColumnType.UnsignedMediumInteger:
        //            case EDBColumnType.MediumInteger:
        //            case EDBColumnType.UnsignedInteger:
        //            case EDBColumnType.Integer:
        //            case EDBColumnType.UnsignedBigInteger:
        //            case EDBColumnType.BigInteger:
        //                oPrepared = 0;
        //                break;
        //            case EDBColumnType.UnsignedDouble:
        //            case EDBColumnType.Double:
        //                oPrepared = 0.0d;
        //                break;
        //            case EDBColumnType.Boolean:
        //                oPrepared = false;
        //                break;
        //            case EDBColumnType.VariableChar:
        //            case EDBColumnType.Text:
        //            case EDBColumnType.MediumText:
        //            case EDBColumnType.LongText:
        //                oPrepared = String.Empty;
        //                break;
        //        }

        //    return oPrepared;
        //}
    }
}