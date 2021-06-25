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
            String sName;
            _oDataController.GetFullName(typeof(ObjectType), out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static String GetSchemaName<ObjectType>()
        {
            String sName;
            _oDataController.GetSchemaName(typeof(ObjectType), out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static String GetTableName<ObjectType>()
        {
            String sName;
            _oDataController.GetTableName(typeof(ObjectType), out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static Dictionary<String, String> GetColumnsNames<ObjectType>()
        {
            Dictionary<String, String> oDictionary;
            _oDataController.GetColumnsNames(typeof(ObjectType), out oDictionary);
            return oDictionary;
        }

        /// <summary>Nullable</summary>
        public static String GetColumnName<ObjectType>(String sMName)
        {
            String sName;
            _oDataController.GetColumnName(typeof(ObjectType), sMName, out sName);
            return sName;
        }

        /// <summary>Nullable</summary>
        public static ObjectType[] From<ObjectType>(DataTable oDataTable) where ObjectType : new()
        {
            return _oDataController.From<ObjectType>(ref oDataTable);
        }

        /// <summary>Nullable</summary>
        public static ObjectType[] From<ObjectType>(DataRowCollection cDataRow) where ObjectType : new()
        {
            return _oDataController.From<ObjectType>(ref cDataRow);
        }

        /// <summary>Nullable</summary>
        public static ObjectType From<ObjectType>(DataRow oDataRow) where ObjectType : new()
        {
            return _oDataController.From<ObjectType>(ref oDataRow);
        }
    }
}