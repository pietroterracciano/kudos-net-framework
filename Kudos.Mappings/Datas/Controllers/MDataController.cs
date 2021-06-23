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
    sealed class MDataController : MappingController<DataTable, DataRow, DataTableMappingAttribute, DataRowMappingAttribute>
    { 
        protected override string GetRuleFromClassAttribute(DataTableMappingAttribute oCAttribute)
        {
            return oCAttribute.TableName;
        }

        protected override string GetRuleFromMemberAttribute(DataRowMappingAttribute oMAttribute)
        {
            return oMAttribute.ColumnName;
        }

        #region public String GetTableName()

        /// <summary>Nullable</summary>
        public String GetTableName(Object oObject)
        {
            KeyValuePair<String, String> oKeyValuePair;
            ClassFullName2Name(oObject, out oKeyValuePair);
            return oKeyValuePair.Value;
        }

        /// <summary>Nullable</summary>
        public String GetTableName<ObjectType>()
        {
            KeyValuePair<String, String> oKeyValuePair;
            ClassFullName2Name<ObjectType>(out oKeyValuePair);
            return oKeyValuePair.Value;
        }

        /// <summary>Nullable</summary>
        public String GetTableName(Type oType)
        {
            KeyValuePair<String, String> oKeyValuePair;
            ClassFullName2Name(oType, out oKeyValuePair);
            return oKeyValuePair.Value;
        }

        #endregion

        #region public Dictionary<String,String> GetColumnsNames()

        /// <summary>Nullable</summary>
        public Dictionary<String, String> GetColumnsNames(Object oObject)
        {
            Dictionary<String, String> oDictionary;
            ClassMembersNames2Names(oObject, out oDictionary);
            return oDictionary;
        }

        /// <summary>Nullable</summary>
        public Dictionary<String, String> GetColumnsNames<ObjectType>()
        {
            Dictionary<String, String> oDictionary;
            ClassMembersNames2Names<ObjectType>(out oDictionary);
            return oDictionary;
        }

        /// <summary>Nullable</summary>
        public Dictionary<String, String> GetColumnsNames(Type oType)
        {
            Dictionary<String, String> oDictionary;
            ClassMembersNames2Names(oType, out oDictionary);
            return oDictionary;
        }

        #endregion

        #region public Dictionary<String,String> GetColumnName()

        /// <summary>Nullable</summary>
        public String GetColumnName(Object oObject, String sMName)
        {
            String oString;
            ClassMemberName2Name(oObject, sMName, out oString);
            return oString;
        }

        /// <summary>Nullable</summary>
        public String GetColumnName<ObjectType>(String sMName)
        {
            String oString;
            ClassMemberName2Name<ObjectType>(sMName, out oString);
            return oString;
        }

        /// <summary>Nullable</summary>
        public String GetColumnName(Type oType, String sMName)
        {
            String oString;
            ClassMemberName2Name(oType, sMName, out oString);
            return oString;
        }

        #endregion

        protected override ObjectType InternalFrom<ObjectType>(ref DataRow oDataRow)
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
                GetClassMemberName(oType, oDataColumn.ColumnName, out sCMemberName);
                if(String.IsNullOrWhiteSpace(sCMemberName))
                    continue;

                MemberInfo oMemberInfo;
                GetClassMemberInfo(oType, sCMemberName, out oMemberInfo);
                if (oMemberInfo == null)
                    continue;

                PropertyInfo
                    oPropertyInfo = oMemberInfo as PropertyInfo;

                if (oPropertyInfo != null)
                {
                    if (oPropertyInfo.SetMethod != null && oPropertyInfo.SetMethod.IsPublic)
                        try { oPropertyInfo.SetValue(oObject, ObjectUtils.ChangeType(oDataRow[oDataColumn.ColumnName], oPropertyInfo.PropertyType)); } catch { }

                    continue;
                }

                FieldInfo
                    oFieldInfo = oMemberInfo as FieldInfo;

                if (oFieldInfo != null)
                    try { oFieldInfo.SetValue(oObject, ObjectUtils.ChangeType(oDataRow[oDataColumn.ColumnName], oFieldInfo.FieldType)); } catch { }
            }

            return oObject;
        }

        protected override ObjectType[] InternalFrom<ObjectType>(ref DataTable oDataTable)
        {
            if (oDataTable == null)
                return null;

            List<ObjectType> 
                lObjects = new List<ObjectType>();

            for(int i=0; i<oDataTable.Rows.Count; i++)
                lObjects.Add(InternalFrom<ObjectType>(oDataTable.Rows[i]));

            return lObjects.ToArray();
        }
    }
}
