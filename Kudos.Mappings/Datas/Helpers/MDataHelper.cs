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

        #region public String GetTableName()

        /// <summary>Nullable</summary>
        public static String GetTableName(Object oObject)
        {
            return _oDataController.GetTableName(oObject);
        }

        /// <summary>Nullable</summary>
        public static String GetTableName<ObjectType>()
        {
            return _oDataController.GetTableName<ObjectType>();
        }

        /// <summary>Nullable</summary>
        public static String GetTableName(Type oType)
        {
            return _oDataController.GetTableName(oType);
        }

        #endregion

        #region public String GetColumnsNames()

        /// <summary>Nullable</summary>
        public static Dictionary<String, String> GetColumnsNames(Object oObject)
        {
            return _oDataController.GetColumnsNames(oObject);
        }

        /// <summary>Nullable</summary>
        public static Dictionary<String, String> GetColumnsNames<ObjectType>()
        {
            return _oDataController.GetColumnsNames<ObjectType>();
        }

        /// <summary>Nullable</summary>
        public static Dictionary<String, String> GetColumnsNames(Type oType)
        {
            return _oDataController.GetColumnsNames(oType);
        }

        #endregion

        #region public String GetColumnName()

        /// <summary>Nullable</summary>
        public static String GetColumnName(Object oObject, String sMName)
        {
            return _oDataController.GetColumnName(oObject, sMName);
        }

        /// <summary>Nullable</summary>
        public static String GetColumnName<ObjectType>(String sMName)
        {
            return _oDataController.GetColumnName<ObjectType>(sMName);
        }

        /// <summary>Nullable</summary>
        public static String GetColumnsNames(Type oType, String sMName)
        {
            return _oDataController.GetColumnName(oType, sMName);
        }

        #endregion

        #region public ObjectType[] From<ObjectType>()

        public static ObjectType[] From<ObjectType>(DataTable oDataTable) where ObjectType : new()
        {
            return _oDataController.From<ObjectType>(oDataTable);
        }

        #endregion

        #region public ObjectType From<ObjectType>()

        public static ObjectType From<ObjectType>(DataRow oDataRow) where ObjectType : new()
        {
            return _oDataController.From<ObjectType>(oDataRow);
        }

        #endregion


    }
}