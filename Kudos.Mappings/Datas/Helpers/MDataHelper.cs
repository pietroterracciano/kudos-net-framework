using Kudos.Mappings.Datas.Attributes;
using Kudos.Mappings.Datas.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Datas.Helpers
{
    public static class MDataHelper
    {
        private static MDataController
            _oDataController = new MDataController();

        /// <summary>Nullable</summary>
        public static String GetFullName<ObjectType>()
        {
            Type oType = typeof(ObjectType);
            String sName;
            _oDataController.GetFullName(ref oType, out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static String GetSchemaName<ObjectType>()
        {
            Type oType = typeof(ObjectType);
            String sName;
            _oDataController.GetSchemaName(ref oType, out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static String GetTableName<ObjectType>()
        {
            Type oType = typeof(ObjectType);
            String sName;
            _oDataController.GetTableName(ref oType, out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static Dictionary<String, String> GetColumnsNames<ObjectType>()
        {
            Type oType = typeof(ObjectType);
            Dictionary<String, String> oDictionary;
            _oDataController.GetColumnsNames(ref oType, out oDictionary);
            return oDictionary;
        }

        /// <summary>Nullable</summary>
        public static String GetColumnName<ObjectType>(String sMName)
        {
            Type oType = typeof(ObjectType);
            String sName;
            _oDataController.GetColumnName(ref oType, sMName, out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static ObjectType[] From<ObjectType>(DataTable oDataTable) where ObjectType : new()
        {
            ObjectType[] aObjects;
            _oDataController.From<ObjectType>(ref oDataTable, out aObjects);
            return aObjects;
        }

        /// <summary>Nullable</summary>
        public static ObjectType[] From<ObjectType>(DataRowCollection cDataRow) where ObjectType : new()
        {
            ObjectType[] aObjects;
            _oDataController.From<ObjectType>(ref cDataRow, out aObjects);
            return aObjects;
        }

        /// <summary>Nullable</summary>
        public static ObjectType From<ObjectType>(DataRow oDataRow) where ObjectType : new()
        {
            ObjectType oObject;
            _oDataController.From<ObjectType>(ref oDataRow, out oObject);
            return oObject;
        }
    }
}