using Kudos.Mappings.Controllers;
using Kudos.Mappings.Datas.Attributes;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Datas.Controllers
{
    sealed class MDataController : MappingController<DataRow, DataTableMappingAttribute, DataRowMappingAttribute>
    {
        private static readonly String
            SEPARATOR = nameof(MDataController) + "." + nameof(SEPARATOR);

        private static Dictionary<String, String>
            _dClassFullNames2FullNames = new Dictionary<String, String>(),
            _dClassFullNames2SchemasNames = new Dictionary<String, String>(),
            _dClassFullNames2TablesNames = new Dictionary<String, String>();

        private static Dictionary<String, Dictionary<String, String>>
            _dClassFullNames2MembersNames2ColumnsNames = new Dictionary<String, Dictionary<String, String>>();

        protected override string GetRuleFromClassAttribute(DataTableMappingAttribute oCAttribute)
        {
            return oCAttribute.SchemaName+SEPARATOR+oCAttribute.TableName;
        }

        protected override string GetRuleFromMemberAttribute(DataRowMappingAttribute oMAttribute)
        {
            return oMAttribute.ColumnName;
        }

        /// <summary>Nullable</summary>
        public void GetFullName(Type oType, out String sName)
        {
            TryGetValueFromDictionary(
                ref _dClassFullNames2FullNames,
                oType.FullName,
                out sName
            );

            if (sName != null)
                return;

            String sSchemaName, sTableName;
            GetSchemaName(oType, out sSchemaName);
            GetTableName(oType, out sTableName);

            Boolean
                bIsSchemaNameNullOrWhiteSpace = String.IsNullOrWhiteSpace(sSchemaName),
                bIsTableNameNullOrWhiteSpace = String.IsNullOrWhiteSpace(sTableName);

            if (bIsSchemaNameNullOrWhiteSpace && bIsTableNameNullOrWhiteSpace)
                sName = null;
            else if (!bIsSchemaNameNullOrWhiteSpace && !bIsTableNameNullOrWhiteSpace)
                sName = sSchemaName + "." + sTableName;
            else if (!bIsSchemaNameNullOrWhiteSpace)
                sName = sSchemaName;
            else
                sName = sTableName;

            _dClassFullNames2FullNames[oType.FullName] = sName;
        }

        /// <summary>Nullable</summary>
        public void GetTableName(Type oType, out String sName)
        {
            TryGetValueFromDictionary(
                ref _dClassFullNames2TablesNames,
                oType.FullName,
                out sName
            );

            if (sName != null)
                return;

            KeyValuePair<String, String> oKeyValuePair;
            ClassFullName2Name(oType, out oKeyValuePair);

            String sSchemaName;
            AnalizeClassFullName2Name(ref oKeyValuePair, out sSchemaName, out sName);

            _dClassFullNames2TablesNames[oType.FullName] = sName;
        }

        /// <summary>Nullable</summary>
        public void GetSchemaName(Type oType, out String sName)
        {
            TryGetValueFromDictionary(
                ref _dClassFullNames2SchemasNames,
                oType.FullName,
                out sName
            );

            if (sName != null)
                return;

            KeyValuePair<String, String> oKeyValuePair;
            ClassFullName2Name(oType, out oKeyValuePair);

            String sTableName;
            AnalizeClassFullName2Name(ref oKeyValuePair, out sName, out sTableName);

            _dClassFullNames2SchemasNames[oType.FullName] = sName;
        }

        private void AnalizeClassFullName2Name(ref KeyValuePair<String, String> oKeyValuePair, out String sSchemaName, out String sTableName)
        {
            if(oKeyValuePair.Value == null)
            {
                sSchemaName = null;
                sTableName = null;
                return;
            }

            String[]
                aKVPPieces = oKeyValuePair.Value.Split(SEPARATOR);

            sSchemaName =
                ArrayUtils.IsValidIndex(aKVPPieces, 0)
                    ? aKVPPieces[0]
                    : null;

            sTableName =
                ArrayUtils.IsValidIndex(aKVPPieces, 1)
                    ? aKVPPieces[1]
                    : null;
        }

        /// <summary>Nullable</summary>
        public void GetColumnsNames(Type oType, out Dictionary<String, String> oDictionary)
        {
            TryGetValueFromDictionary(
                ref _dClassFullNames2MembersNames2ColumnsNames,
                oType.FullName,
                out oDictionary
            );

            if (oDictionary != null)
                return;

            ClassMembersNames2Names(
                oType, 
                out _dClassFullNames2MembersNames2ColumnsNames, 
                out oDictionary
            );
        }

        /// <summary>Nullable</summary>
        public void GetColumnName(Type oType, String sMName, out String sName)
        {
            TryGetValueFromDictionary(
                ref _dClassFullNames2MembersNames2ColumnsNames,
                oType.FullName,
                sMName,
                out sName
            );

            if (sName != null)
                return;

            GetNameFromClassMemberName(
                oType, 
                sMName, 
                out _dClassFullNames2MembersNames2ColumnsNames,
                out sName
            );
        }

        public ObjectType[] From<ObjectType>(ref DataTable oDataTable) where ObjectType : new()
        {
            if (oDataTable == null)
                return null;

            DataRowCollection cDataRow = oDataTable.Rows;
            return From<ObjectType>(ref cDataRow);
        }

        public ObjectType[] From<ObjectType>(ref DataRowCollection cDataRow) where ObjectType : new()
        {
            if (cDataRow == null)
                return null;

            ObjectType[]
                aObjects = new ObjectType[cDataRow.Count];

            for (int i = 0; i < cDataRow.Count; i++)
            {
                DataRow oDataRow = cDataRow[i];
                aObjects[i] = From<ObjectType>(ref oDataRow);
            }

            return aObjects;
        }

        public ObjectType From<ObjectType>(ref DataRow oDataRow) where ObjectType : new()
        {
            Type oType = typeof(ObjectType);

            if (oDataRow == null)
                return default(ObjectType);

            ObjectType
                oObject = new ObjectType();

            foreach (DataColumn oDataColumn in oDataRow.Table.Columns)
            {
                if (
                    oDataColumn == null
                    || String.IsNullOrEmpty(oDataColumn.ColumnName)
                )
                    continue;

                String sCMemberName;
                GetColumnName(oType, oDataColumn.ColumnName, out sCMemberName);

                MemberInfo oMemberInfo;
                GetClassMemberInfo(oType, sCMemberName, out oMemberInfo);

                if (oMemberInfo == null)
                    continue;
                else if (oMemberInfo.MemberType == MemberTypes.Property)
                {
                    PropertyInfo
                        oPropertyInfo = (PropertyInfo)oMemberInfo;

                    if (oPropertyInfo != null)
                    {
                        if (oPropertyInfo.SetMethod != null && oPropertyInfo.SetMethod.IsPublic)
                            try { oPropertyInfo.SetValue(oObject, ObjectUtils.ChangeType(oDataRow[oDataColumn.ColumnName], oPropertyInfo.PropertyType)); } catch { }

                        continue;
                    }
                }
                else if (oMemberInfo.MemberType == MemberTypes.Field)
                {
                    FieldInfo
                        oFieldInfo = (FieldInfo)oMemberInfo;

                    if (oFieldInfo != null)
                        try { oFieldInfo.SetValue(oObject, ObjectUtils.ChangeType(oDataRow[oDataColumn.ColumnName], oFieldInfo.FieldType)); } catch { }
                }
            }

            return oObject;
        }
    }
}
