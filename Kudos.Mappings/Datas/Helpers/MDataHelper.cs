using Kudos.Mappings.Datas.Attributes;
using Kudos.Mappings.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Datas.Helpers
{
    public static class MDataHelper
    {
        private static MappingHelper
            _hMapping = new MappingHelper();

        private static readonly HashSet<String>
            _hsAnalyzedClassesFullNames = new HashSet<String>();

        private static Dictionary<String, String>
            _dClassesFullNames2Names = new Dictionary<String, String>(),
            _dNames2ClassesFullNames = new Dictionary<String, String>();

        private static Dictionary<String, Dictionary<String, String>>
            _dClassesFullNames2MembersNames2Names = new Dictionary<String, Dictionary<String, String>>>(),
            _dClassesFullNames2Names2MembersNames = new Dictionary<String, Dictionary<String, String>>>();

        #region TableName

        #region public static String GetDataTableTableName()

        public static String GetDataTableTableName(Object oObject)
        {
            String sTableName;
            _hMapping.(oObject, DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME, out sTableName);
            return sTableName;
        }

        public static String GetDataTableTableName<ObjectType>()
        {
            String sTableName;
            GetTableName<ObjectType>(DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME, out sTableName);
            return sTableName;
        }

        public static String GetDataTableTableName(Type oType)
        {
            String sTableName;
            GetTableName(oType, DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME, out sTableName);
            return sTableName;
        }

        #endregion

        #endregion

    }
}