using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Controllers
{
    abstract class MappingController<FromObjectsType, FromObjectType, ClassAttributeType, MemberAttributeType>
        where ClassAttributeType : Attribute
        where MemberAttributeType : Attribute
    {
        private static readonly Object
            _oLock = new Object();

        private static readonly HashSet<String>
            _hsAnalyzedClassesFullNames = new HashSet<String>();

        private static Dictionary<String, Dictionary<String, MemberInfo>>
            _dClassFullNames2MembersNames2MembersInfos = new Dictionary<String, Dictionary<String, MemberInfo>>();

        private static Dictionary<String, Dictionary<String, String>>
            _dAttributesNames2ClassFullNames2Names = new Dictionary<String, Dictionary<String, String>>(),
            _dAttributesNames2Names2ClassFullNames = new Dictionary<String, Dictionary<String, String>>();

        private static Dictionary<String, Dictionary<String, Dictionary<String, String>>>
            _dAttributes2ClassFullNames2MembersNames2Names = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>(),
            _dAttributes2ClassFullNames2Names2MembersNames = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

        private Type
            _tCAttribute = typeof(ClassAttributeType),
            _tMAttribute = typeof(MemberAttributeType);

        protected abstract String GetRuleFromClassAttribute(ClassAttributeType oCAttribute);
        protected abstract String GetRuleFromMemberAttribute(MemberAttributeType oMAttribute);

        private Boolean Analyze(ref Type oType)
        {
            if (oType == null)
                return false;
            else if (_hsAnalyzedClassesFullNames.Contains(oType.FullName))
                return true;

            lock (_oLock)
            {
                if (_hsAnalyzedClassesFullNames.Contains(oType.FullName))
                    return true;

                _hsAnalyzedClassesFullNames.Add(oType.FullName);

                #region Recupero l'Attribute della Class e lo aggiungo al Dictionaries corrispondenti

                ClassAttributeType
                    oCAttribute = ObjectUtils.GetClassAttribute<ClassAttributeType>(oType);

                String
                    sCRule =
                        oCAttribute != null
                            ? GetRuleFromClassAttribute(oCAttribute)
                            : oType.FullName;

                _dAttributesNames2ClassFullNames2Names[_tCAttribute.FullName] = new Dictionary<String, String>();
                _dAttributesNames2ClassFullNames2Names[_tCAttribute.FullName][oType.FullName] = sCRule;

                _dAttributesNames2Names2ClassFullNames[_tCAttribute.FullName] = new Dictionary<String, String>();
                _dAttributesNames2Names2ClassFullNames[_tCAttribute.FullName][sCRule] = oType.FullName;

                //PopulateDictionary(
                //    ref _dAttributesNames2ClassFullNames2Names,
                //    _tCAttribute.FullName,
                //    oType.FullName,
                //    sCRule
                //);

                //PopulateDictionary(
                //    ref _dAttributesNames2Names2ClassFullNames,
                //    _tCAttribute.FullName,
                //    sCRule,
                //    oType.FullName
                //);

                #endregion

                List<MemberInfo>
                   lMembers;

                #region Recupero tutti i Members della Class

                lMembers = new List<MemberInfo>();

                PropertyInfo[]
                    aProperties = ObjectUtils.GetProperties(oType);

                if (aProperties != null)
                    lMembers.AddRange(aProperties);

                FieldInfo[]
                    aFields = ObjectUtils.GetFields(oType);

                if (aFields != null)
                    lMembers.AddRange(aFields);

                #endregion

                #region Analizzo l'Attribute per tutti i Members recuperati in precedenza e popolo i Dictionaries corrispondenti

                _dClassFullNames2MembersNames2MembersInfos[oType.FullName] = new Dictionary<String, MemberInfo>();

                _dAttributes2ClassFullNames2MembersNames2Names[_tMAttribute.FullName] = new Dictionary<String, Dictionary<String, String>>();
                _dAttributes2ClassFullNames2MembersNames2Names[_tMAttribute.FullName][oType.FullName] = new Dictionary<String, String>();

                _dAttributes2ClassFullNames2Names2MembersNames[_tMAttribute.FullName] = new Dictionary<String, Dictionary<String, String>>();
                _dAttributes2ClassFullNames2Names2MembersNames[_tMAttribute.FullName][oType.FullName] = new Dictionary<String, String>();

                for (int i = 0; i < lMembers.Count; i++)
                {
                    if (lMembers[i] == null)
                        continue;

                    #region Aggiungo il Member i-esimo al Dictionary dei Members

                    _dClassFullNames2MembersNames2MembersInfos[oType.FullName][lMembers[i].Name] = lMembers[i];

                    #endregion

                    #region Recupero l'Attribute del Member i-esimo e lo aggiungo ai Dictionaries corrispondenti

                    MemberAttributeType
                        oMAttribute = ObjectUtils.GetMemberAttribute<MemberAttributeType>(lMembers[i], true);

                    String
                        sMRule =
                            oMAttribute != null
                                ? GetRuleFromMemberAttribute(oMAttribute)
                                : lMembers[i].Name;

                    _dAttributes2ClassFullNames2MembersNames2Names[_tMAttribute.FullName][oType.FullName][lMembers[i].Name] = sMRule;
                    _dAttributes2ClassFullNames2Names2MembersNames[_tMAttribute.FullName][oType.FullName][sMRule] = lMembers[i].Name;

                    //#region Popolo il Dictionary normale

                    //PopulateDictionary(
                    //    ref _dAttributes2ClassFullNames2MembersNames2Names,
                    //    _tMAttribute.FullName,
                    //    oType.FullName,
                    //    lMembers[i].Name,
                    //    sMRule
                    //);

                    //#endregion

                    //#region Popolo il Dictionary inverso

                    //PopulateDictionary(
                    //    ref _dAttributes2ClassFullNames2Names2MembersNames,
                    //    _tMAttribute.FullName,
                    //    oType.FullName,
                    //    sMRule,
                    //    lMembers[i].Name
                    //);

                    //#endregion

                    #endregion
                }

                #endregion

                return true;
            }
        }

        #region protected void GetClassMemberInfo()

        protected void GetClassMemberInfo(Type oType, String sCMName, out MemberInfo oMemberInfo)
        {
            if (!Analyze(ref oType))
            {
                oMemberInfo = null;
                return;
            }

            Dictionary<String, MemberInfo> dClassMembersNames2ClassMembersInfos;
            
            if (
                !_dClassFullNames2MembersNames2MembersInfos.TryGetValue(oType.FullName, out dClassMembersNames2ClassMembersInfos) 
                || dClassMembersNames2ClassMembersInfos == null
                || sCMName == null
            )
            {
                oMemberInfo = null;
                return;
            }

            dClassMembersNames2ClassMembersInfos.TryGetValue(sCMName, out oMemberInfo);
        }

        #endregion

        #region protected void ClassFullName2Name()

        protected void ClassFullName2Name(Object oObject, out KeyValuePair<String, String> oKeyValuePair)
        {
            if(oObject == null)
            {
                oKeyValuePair = new KeyValuePair<String, String>();
                return;
            }

            ClassFullName2Name(oObject.GetType(), out oKeyValuePair);
        }

        protected void ClassFullName2Name<ObjectType>(out KeyValuePair<String, String> oKeyValuePair)
        {
            ClassFullName2Name(typeof(ObjectType), out oKeyValuePair);
        }

        protected void ClassFullName2Name(Type oType, out KeyValuePair<String, String> oKeyValuePair )
        {
            if (!Analyze(ref oType))
            {
                oKeyValuePair = new KeyValuePair<String, String>();
                return;
            }

            String sName;
            TryGetValueFromDictionary(
                ref _dAttributesNames2ClassFullNames2Names, 
                _tCAttribute.FullName, 
                oType.FullName, 
                out sName
            );

            oKeyValuePair = new KeyValuePair<String, String>(oType.FullName, sName);
        }

        #endregion

        #region protected void ClassMembersNames2Names()

        /// <summary>Nullable</summary>
        protected void ClassMembersNames2Names(Object oObject, out Dictionary<String, String> oDictionary)
        {
            if (oObject == null)
            {
                oDictionary = null;
                return;
            }

            ClassMembersNames2Names(oObject.GetType(), out oDictionary);
        }

        /// <summary>Nullable</summary>
        protected void ClassMembersNames2Names<ObjectType>(out Dictionary<String, String> oDictionary)
        {
            ClassMembersNames2Names(typeof(ObjectType), out oDictionary);
        }

        /// <summary>Nullable</summary>
        protected void ClassMembersNames2Names(Type oType, out Dictionary<String, String> oDictionary)
        {
            if (!Analyze(ref oType))
            {
                oDictionary = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2MembersNames2Names, 
                _tMAttribute.FullName, 
                oType.FullName, 
                out oDictionary
            );
        }

        #endregion

        #region protected void ClassMemberName2Name()

        /// <summary>Nullable</summary>
        protected void ClassMemberName2Name(Object oObject, String sCMName, out String oString)
        {
            if (oObject == null)
            {
                oString = null;
                return;
            }

            ClassMemberName2Name(oObject.GetType(), sCMName, out oString);
        }

        /// <summary>Nullable</summary>
        protected void ClassMemberName2Name<ObjectType>(String sCMName, out String oString)
        {
            ClassMemberName2Name(typeof(ObjectType), sCMName, out oString);
        }

        /// <summary>Nullable</summary>
        protected void ClassMemberName2Name(Type oType, String sCMName, out String oString)
        {
            if (!Analyze(ref oType))
            {
                oString = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2MembersNames2Names,
                _tMAttribute.FullName,
                oType.FullName,
                sCMName,
                out oString
            );
        }

        #endregion

        #region protected void Names2ClassMembersNames()

        /// <summary>Nullable</summary>
        protected void Names2ClassMembersNames(Object oObject, out Dictionary<String, String> oDictionary)
        {
            if (oObject == null)
            {
                oDictionary = null;
                return;
            }

            Names2ClassMembersNames(oObject.GetType(), out oDictionary);
        }

        /// <summary>Nullable</summary>
        protected void Names2ClassMembersNames<ObjectType>(out Dictionary<String, String> oDictionary)
        {
            Names2ClassMembersNames(typeof(ObjectType), out oDictionary);
        }

        /// <summary>Nullable</summary>
        protected void Names2ClassMembersNames(Type oType, out Dictionary<String, String> dStrings2Strings)
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2Names2MembersNames,
                _tCAttribute.FullName,
                oType.FullName,
                out dStrings2Strings
            );
        }

        #endregion

        #region protected void GetClassMemberName()

        /// <summary>Nullable</summary>
        protected void GetClassMemberName(Object oObject, String sName, out String sCMemberName)
        {
            if (oObject == null)
            {
                sCMemberName = null;
                return;
            }

            GetClassMemberName(oObject.GetType(), sName, out sCMemberName);
        }

        /// <summary>Nullable</summary>
        protected void GetClassMemberName<ObjectType>(String sName, out String sCMemberName)
        {
            GetClassMemberName(typeof(ObjectType), sName, out sCMemberName);
        }

        /// <summary>Nullable</summary>
        protected void GetClassMemberName(Type oType, String sName, out String sCMemberName)
        {
            if (!Analyze(ref oType))
            {
                sCMemberName = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2Names2MembersNames,
                _tCAttribute.FullName,
                oType.FullName,
                sName,
                out sCMemberName
            );
        }

        #endregion

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

            if (!TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, sKey0, out dStrings2Strings2Strings))
            {
                dStrings2Strings = null;
                return false;
            }
            else if (!TryGetValueFromDictionary(ref dStrings2Strings2Strings, sKey1, out dStrings2Strings))
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

        protected static Boolean TryGetValueFromDictionary(
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

        //#region private static Boolean PopulateDictionary()

        //private static Boolean PopulateDictionary(
        //    ref Dictionary<String, Dictionary<String, Dictionary<String, String>>> dStrings2Strings2Strings2Strings,
        //    String sKey0,
        //    String sKey1,
        //    String sKey2,
        //    String sValue
        //)
        //{
        //    if (dStrings2Strings2Strings2Strings == null)
        //        dStrings2Strings2Strings2Strings = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

        //    if (sKey0 == null)
        //        return false;

        //    Dictionary<String, Dictionary<String, String>>
        //        dStrings2Strings2Strings;

        //    if (
        //        !dStrings2Strings2Strings2Strings.TryGetValue(sKey0, out dStrings2Strings2Strings)
        //        || dStrings2Strings2Strings == null
        //    )
        //        dStrings2Strings2Strings2Strings[sKey0] = dStrings2Strings2Strings = new Dictionary<String, Dictionary<String, String>>();

        //    return PopulateDictionary(ref dStrings2Strings2Strings, sKey1, sKey2, sValue);
        //}

        //private static Boolean PopulateDictionary(
        //    ref Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
        //    String sKey0,
        //    String sKey1,
        //    String sValue
        //)
        //{
        //    if (dStrings2Strings2Strings == null)
        //        dStrings2Strings2Strings = new Dictionary<String, Dictionary<String, String>>();

        //    if (dStrings2Strings2Strings == null || sKey0 == null || sKey1 == null)
        //        return false;

        //    Dictionary<String, String>
        //        dStrings2Strings;

        //    if (
        //        !dStrings2Strings2Strings.TryGetValue(sKey0, out dStrings2Strings)
        //        || dStrings2Strings == null
        //    )
        //        dStrings2Strings2Strings[sKey0] = dStrings2Strings = new Dictionary<String, String>();

        //    dStrings2Strings[sKey1] = sValue;
        //    return true;
        //}

        //#endregion

        public ObjectType[] From<ObjectType>(FromObjectsType oFromObject) where ObjectType : new()
        {
            Type
                oType = typeof(ObjectType);

            return
                Analyze(ref oType)
                    ? InternalFrom<ObjectType>(ref oFromObject)
                    : null;
        }

        public ObjectType From<ObjectType>(FromObjectType oFromObject) where ObjectType : new()
        {
            Type
                oType = typeof(ObjectType);

            return
                Analyze(ref oType)
                    ? InternalFrom<ObjectType>(ref oFromObject)
                    : default(ObjectType);
        }

        protected ObjectType[] InternalFrom<ObjectType>(FromObjectsType oFromObjects) where ObjectType : new()
        {
            return InternalFrom<ObjectType>(ref oFromObjects);
        }
        protected abstract ObjectType[] InternalFrom<ObjectType>(ref FromObjectsType oFromObject) where ObjectType : new();
        protected ObjectType InternalFrom<ObjectType>(FromObjectType oFromObject) where ObjectType : new()
        {
            return InternalFrom<ObjectType>(ref oFromObject);
        }
        protected abstract ObjectType InternalFrom<ObjectType>(ref FromObjectType oFromObject) where ObjectType : new();
    }
}
