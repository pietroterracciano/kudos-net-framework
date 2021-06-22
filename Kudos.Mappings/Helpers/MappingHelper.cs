using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Helpers
{
    class MappingHelper
    {
        private static readonly String
            DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME = typeof(DataTableMappingAttribute).FullName,
            DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME = typeof(DataRowMappingAttribute).FullName;

        private static readonly Object
            _oLock = new Object();

        private static readonly HashSet<String>
            _hsAnalyzedClassesFullNames = new HashSet<String>();

        private static readonly Dictionary<String, Dictionary<String, String>>
            _dClassesFullNames2AttributesFullNames2TablesNames = new Dictionary<String, Dictionary<String,String>>();

        private static Dictionary<String, Dictionary<String, Dictionary<String, String>>>
            _dClassesFullNames2AttributesFullNames2MembersNames2Names = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>(),
            _dClassesFullNames2AttributesFullNames2Names2MembersNames = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

        #region private static Boolean TryGetValueFromDictionary()

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            String sKey,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings
        )
        {
            if (dStrings2Strings2Strings2Strings != null && sKey != null)
                return dStrings2Strings2Strings2Strings.TryGetValue(sKey, out dStrings2Strings2Strings);

            dStrings2Strings2Strings = null;
            return false;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            String sKey0,
            String sKey1,
            out Dictionary<String, String> dStrings2Strings
        )
        {
            Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings;

            if ( !TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, sKey0, out dStrings2Strings2Strings) )
            {
                dStrings2Strings = null;
                return false;
            }
            else if(!TryGetValueFromDictionary(ref dStrings2Strings2Strings, sKey1, out dStrings2Strings))
                return false;

            return true;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            String sKey0,
            String sKey1,
            String sKey2,
            out String oString
        )
        {
            Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings;
            Dictionary<String, String> dStrings2Strings;

            if (
                !TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, sKey0, out dStrings2Strings2Strings)
                || !TryGetValueFromDictionary(ref dStrings2Strings2Strings, sKey1, out dStrings2Strings)
            )
            {
                oString = null;
                return false;
            }
            else if (!TryGetValueFromDictionary(ref dStrings2Strings, sKey2, out oString))
                return false;

            return true;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            String sKey,
            out Dictionary<String, String> dStrings2Strings
        )
        {
            if (dStrings2Strings2Strings != null && sKey != null)
                return dStrings2Strings2Strings.TryGetValue(sKey, out dStrings2Strings);

            dStrings2Strings = null;
            return false;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            String sKey0,
            String sKey1,
            out String sValue
        )
        {
            Dictionary<String, String> dStrings2Strings;

            if (!TryGetValueFromDictionary(ref dStrings2Strings2Strings, sKey0, out dStrings2Strings))
            {
                sValue = null;
                return false;
            }

            return TryGetValueFromDictionary(ref dStrings2Strings, sKey1, out sValue);
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, String> dStrings2Strings,
            String sKey,
            out String sValue
        )
        {
            if (dStrings2Strings != null && sKey != null)
                return dStrings2Strings.TryGetValue(sKey, out sValue);

            sValue = null;
            return false;
        }


        #endregion

        #region private static Boolean PopulateDictionary()

        private static Boolean PopulateDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings, 
            String sKey0, 
            String sKey1, 
            String sKey2,
            String sValue
        )
        {
            if (dStrings2Strings2Strings2Strings == null)
                dStrings2Strings2Strings2Strings = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

            if ( sKey0 == null)
                return false;

            Dictionary<String, Dictionary<String,String>>
                dStrings2Strings2Strings;

            if (
                !dStrings2Strings2Strings2Strings.TryGetValue(sKey0, out dStrings2Strings2Strings)
                || dStrings2Strings2Strings == null
            )
                dStrings2Strings2Strings2Strings[sKey0] = dStrings2Strings2Strings = new Dictionary<String, Dictionary<String, String>>();

            return PopulateDictionary(ref dStrings2Strings2Strings, sKey1, sKey2, sValue);
        }

        private static Boolean PopulateDictionary(
            ref Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings, 
            String sKey0, 
            String sKey1, 
            String sValue
        )
        {
            if (dStrings2Strings2Strings == null)
                dStrings2Strings2Strings = new Dictionary<String, Dictionary<String, String>>();

            if (dStrings2Strings2Strings == null || sKey0 == null || sKey1 == null)
                return false;

            Dictionary<String, String>
                dStrings2Strings;

            if (
                !dStrings2Strings2Strings.TryGetValue(sKey0, out dStrings2Strings)
                || dStrings2Strings == null
            )
                dStrings2Strings2Strings[sKey0] = dStrings2Strings = new Dictionary<String, String>();

            dStrings2Strings[sKey1] = sValue;
            return true;
        }

        #endregion

        #region private static bool Analyze()

        private static bool Analyze(ref Type oOType)
        {
            if (oOType == null)
                return false;
            else if (_hsAnalyzedClassesFullNames.Contains(oOType.FullName))
                return true;

            lock (_oLock)
            {
                if (_hsAnalyzedClassesFullNames.Contains(oOType.FullName))
                    return true;

                _hsAnalyzedClassesFullNames.Add(oOType.FullName);

                #region Recupero il DataTableAttribute della Class e lo aggiungo al Dictionary corrispondente

                DataTableMappingAttribute
                    oDTAttribute = ObjectUtils.GetClassAttribute<DataTableMappingAttribute>(oOType);

                PopulateDictionary(
                    ref _dClassesFullNames2AttributesFullNames2TablesNames,
                    oOType.FullName,
                    DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME,
                    oDTAttribute != null
                        ? oDTAttribute.TableName
                        : oOType.FullName
                );

                #endregion

                List<MemberInfo>
                   lMembers;

                #region Recupero tutti i Members della Class
                
                lMembers = new List<MemberInfo>();

                PropertyInfo[]
                    aProperties = ObjectUtils.GetProperties(oOType);

                if (aProperties != null)
                    lMembers.AddRange(aProperties);

                FieldInfo[]
                    aFields = ObjectUtils.GetFields(oOType);

                if (aFields != null)
                    lMembers.AddRange(aFields);

                #endregion

                #region Analizzo il DataRowAttribute per tutti i Members recuperati in precedenza

                for(int i=0; i<lMembers.Count; i++)
                {
                    if (lMembers[i] == null)
                        continue;

                    #region Recupero il DataRowAttribute del Member i-esimo e lo aggiungo ai Dictionaries corrispondenti
                    
                    DataRowMappingAttribute
                        oDRAttribute = ObjectUtils.GetMemberAttribute<DataRowMappingAttribute>(lMembers[i], true);

                    String
                        sDRColumnName =
                            oDRAttribute != null
                                ? oDRAttribute.ColumnName
                                : lMembers[i].Name;

                    #region Popolo il Dictionary normale

                    PopulateDictionary(
                        ref _dClassesFullNames2AttributesFullNames2MembersNames2Names,
                        oOType.FullName,
                        DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME,
                        lMembers[i].Name,
                        sDRColumnName
                    );

                    #endregion

                    #region Popolo il Dictionary inverso

                    PopulateDictionary(
                        ref _dClassesFullNames2AttributesFullNames2Names2MembersNames,
                        oOType.FullName,
                        DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME,
                        sDRColumnName,
                        lMembers[i].Name
                    );

                    #endregion

                    #endregion
                }

                #endregion

                return true;
            }
        }

        #endregion

        #region TableName

        #region public static String GetDataTableTableName()

        public static String GetDataTableTableName(Object oObject)
        {
            String sTableName;
            GetTableName(oObject, DATA_TABLE_MAPPING_ATTRIBUTE__FULL_NAME, out sTableName);
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

        #region private static String GetTableName()

        /// <summary>Nullable</summary>
        private static void GetTableName(Object oObject, String sAttributeFullName, out String sTableName)
        {
            if(oObject == null)
            {
                sTableName = null;
                return;
            }

            GetTableName(oObject.GetType(), sAttributeFullName, out sTableName);
        }

        /// <summary>Nullable</summary>
        private static void GetTableName<ObjectType>(String sAttributeFullName, out String sTableName)
        {
            GetTableName(typeof(ObjectType), sAttributeFullName, out sTableName);
        }

        /// <summary>Nullable</summary>
        private static void GetTableName(Type oType, String sAttributeFullName, out String sTableName)
        {
            if (!Analyze(ref oType))
            {
                sTableName = null;
                return;
            }
            
            TryGetValueFromDictionary(ref _dClassesFullNames2AttributesFullNames2TablesNames, oType.FullName, sAttributeFullName, out sTableName);
        }

        #endregion

        #endregion

        #region Name

        #region public static String GetDataRowColumnName()

        public static String GetDataRowColumnName(Object oObject, String sMemberName)
        {
            String oString;
            GetNameFromAttribute(oObject, DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, sMemberName, out oString);
            return oString;
        }

        public static String GetDataRowColumnName<ObjectType>(String sMemberName)
        {
            String oString;
            GetNameFromAttribute<ObjectType>(DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, sMemberName, out oString);
            return oString;
        }

        public static String GetDataRowColumnName(Type oType, String sMemberName)
        {
            String oString;
            GetNameFromAttribute(oType, DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, sMemberName, out oString);
            return oString;
        }

        #endregion

        #region private static String GetNameFromAttribute()

        /// <summary>Nullable</summary>
        private static Boolean GetNameFromAttribute(Object oObject, String sAttributeFullName,  String sMemberName, out String oString)
        {
            return GetAllNameFromAttribute(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, oObject, sAttributeFullName, sMemberName, out oString);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetNameFromAttribute<ObjectType>(String sAttributeFullName, String sMemberName, out String oString)
        {
            return GetAllNameFromAttribute<ObjectType>(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, sAttributeFullName, sMemberName, out oString);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetNameFromAttribute(Type oType, String sAttributeFullName, String sMemberName, out String oString)
        {
            return GetAllNameFromAttribute(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, oType, sAttributeFullName, sMemberName, out oString);
        }

        #endregion

        #region private static String GetAllNameFromAttribute()

        /// <summary>Nullable</summary>
        private static Boolean GetAllNameFromAttribute(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            Object oObject, 
            String sAttributeFullName, 
            String sMemberName, 
            out String oString
        )
        {
            if (oObject == null)
            {
                oString = null;
                return false;
            }

            return GetAllNameFromAttribute(ref dStrings2Strings2Strings2Strings, oObject.GetType(), sAttributeFullName, sMemberName, out oString);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetAllNameFromAttribute<ObjectType>(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            String sAttributeFullName, 
            String sMemberName, 
            out String oString
            )
        {
            return GetAllNameFromAttribute(ref dStrings2Strings2Strings2Strings, typeof(ObjectType), sAttributeFullName, sMemberName, out oString);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetAllNameFromAttribute(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings, 
            Type oType,
            String sAttributeFullName, 
            String sMemberName, 
            out String oString
        )
        {
            if (!Analyze(ref oType))
            {
                oString = null;
                return false;
            }

            return TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, oType.FullName, sAttributeFullName, sMemberName, out oString);
        }

        #endregion

        #endregion

        #region Names

        #region public static String GetDataRowColumnsNames()

        public static Dictionary<String, String> GetDataRowColumnsNames(Object oObject)
        {
            Dictionary<String, String> dStrings2Strings;
            GetNamesFromAttribute(oObject, DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, out dStrings2Strings);
            return dStrings2Strings;
        }

        public static Dictionary<String, String> GetDataRowColumnsNames<ObjectType>()
        {
            Dictionary<String, String> dStrings2Strings;
            GetNamesFromAttribute<ObjectType>(DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, out dStrings2Strings);
            return dStrings2Strings;
        }

        public static Dictionary<String, String> GetDataRowColumnsNames(Type oType)
        {
            Dictionary<String, String> dStrings2Strings;
            GetNamesFromAttribute(oType, DATA_ROW_MAPPING_ATTRIBUTE__FULL_NAME, out dStrings2Strings);
            return dStrings2Strings;
        }

        #endregion

        #region private static String GetMembersNamesFromAttribute()

        /// <summary>Nullable</summary>
        private static Boolean GetMembersNamesFromAttribute(Object oObject, String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute(ref _dClassesFullNames2AttributesFullNames2Names2MembersNames, oObject, sAttributeFullName, out dStrings2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetMembersNamesFromAttribute<ObjectType>(String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute(ref _dClassesFullNames2AttributesFullNames2Names2MembersNames, typeof(ObjectType), sAttributeFullName, out dStrings2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetMembersNamesFromAttribute(Type oType, String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute(ref _dClassesFullNames2AttributesFullNames2Names2MembersNames, oType, sAttributeFullName, out dStrings2Strings);
        }

        #endregion

        #region private static String GetNamesFromAttribute()

        /// <summary>Nullable</summary>
        private static Boolean GetNamesFromAttribute(Object oObject, String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, oObject, sAttributeFullName, out dStrings2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetNamesFromAttribute<ObjectType>(String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute<ObjectType>(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, sAttributeFullName, out dStrings2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetNamesFromAttribute(Type oType, String sAttributeFullName, out Dictionary<String, String> dStrings2Strings)
        {
            return GetAllNamesFromAttribute(ref _dClassesFullNames2AttributesFullNames2MembersNames2Names, oType, sAttributeFullName, out dStrings2Strings);
        }

        #endregion

        #region private static String GetAllNamesFromAttribute()

        /// <summary>Nullable</summary>
        private static Boolean GetAllNamesFromAttribute(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            Object oObject, 
            String sAttributeFullName, 
            out Dictionary<String, String> dString2Strings
        )
        {
            if (oObject == null)
            {
                dString2Strings = null;
                return false;
            }

            return GetAllNamesFromAttribute(ref dStrings2Strings2Strings2Strings, oObject.GetType(), sAttributeFullName, out dString2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetAllNamesFromAttribute<ObjectType>(
            ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
            String sAttributeFullName, 
            out Dictionary<String, String> dStrings2Strings
        )
        {
            return GetAllNamesFromAttribute(ref dStrings2Strings2Strings2Strings, typeof(ObjectType), sAttributeFullName, out dStrings2Strings);
        }

        /// <summary>Nullable</summary>
        private static Boolean GetAllNamesFromAttribute(
            ref Dictionary<String, Dictionary<String, Dictionary<String,String>>> dStrings2Strings2Strings2Strings,
            Type oType, 
            String sAttributeFullName, 
            out Dictionary<String, String> dStrings2Strings
        )
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings = null;
                return false;
            }

            return TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, oType.FullName, sAttributeFullName, out dStrings2Strings);
        }

        #endregion

        #endregion

    }
}
