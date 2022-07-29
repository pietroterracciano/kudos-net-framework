using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Controllers
{
    abstract class MappingController<FromObjectType, ClassAttributeType, MemberAttributeType>
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

                Dictionary<String, String> dClassFullNames2Names;

                if (
                    !TryGetValueFromDictionary(
                        ref _dAttributesNames2ClassFullNames2Names,
                        _tCAttribute.FullName,
                        out dClassFullNames2Names
                    )
                    || dClassFullNames2Names == null
                )
                    _dAttributesNames2ClassFullNames2Names[_tCAttribute.FullName] = dClassFullNames2Names = new Dictionary<String, String>();

                dClassFullNames2Names[oType.FullName] = sCRule;
                dClassFullNames2Names[sCRule] = oType.FullName;

                #endregion

                List<MemberInfo>
                   lMembers;

                #region Recupero tutti i Members della Class

                lMembers = new List<MemberInfo>();

                PropertyInfo[]
                    aProperties = 
                        ObjectUtils.GetProperties(
                            oType, 
                            BindingFlags.Public 
                            | BindingFlags.Instance 
                            | BindingFlags.Static
                            | BindingFlags.SetProperty
                        );

                if (aProperties != null)
                    lMembers.AddRange(aProperties);

                FieldInfo[]
                    aFields = 
                        ObjectUtils.GetFields(
                            oType,
                            BindingFlags.Public
                            | BindingFlags.Instance
                            | BindingFlags.Static
                            | BindingFlags.SetField
                        );

                if (aFields != null)
                    lMembers.AddRange(aFields);

                #endregion

                #region Analizzo l'Attribute per tutti i Members recuperati in precedenza e popolo i Dictionaries corrispondenti

                _dClassFullNames2MembersNames2MembersInfos[oType.FullName] = new Dictionary<String, MemberInfo>();

                Dictionary<String, Dictionary<String, String>> dClassFullNames2MembersNames2Names;

                if (
                    !TryGetValueFromDictionary(
                        ref _dAttributes2ClassFullNames2MembersNames2Names,
                        _tMAttribute.FullName,
                        out dClassFullNames2MembersNames2Names
                    )
                    || dClassFullNames2MembersNames2Names == null
                )
                    _dAttributes2ClassFullNames2MembersNames2Names[_tMAttribute.FullName] = dClassFullNames2MembersNames2Names = new Dictionary<String, Dictionary<String,String>>();

                dClassFullNames2MembersNames2Names[oType.FullName] = new Dictionary<String, String>();

                Dictionary<String, Dictionary<String, String>> dClassFullNames2Names2MembersNames;

                if (
                    !TryGetValueFromDictionary(
                        ref _dAttributes2ClassFullNames2Names2MembersNames,
                        _tMAttribute.FullName,
                        out dClassFullNames2Names2MembersNames
                    )
                    || dClassFullNames2Names2MembersNames == null
                )
                    _dAttributes2ClassFullNames2Names2MembersNames[_tMAttribute.FullName] = dClassFullNames2Names2MembersNames = new Dictionary<String, Dictionary<String, String>>();

                dClassFullNames2Names2MembersNames[oType.FullName] = new Dictionary<String, String>();

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

                    #endregion
                }

                #endregion

                return true;
            }
        }

        protected void GetClassMemberInfo(ref Type oType, String sCMName, out MemberInfo oMemberInfo)
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

        protected void ClassFullName2Name(ref Type oType, out KeyValuePair<String, String> oKeyValuePair )
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

        /// <summary>Nullable</summary>
        protected void ClassMembersNames2Names(
            ref Type oType,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out Dictionary<String, String> dStrings2Strings
        )
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings2Strings = null;
                dStrings2Strings = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2MembersNames2Names, 
                _tMAttribute.FullName, 
                oType.FullName, 
                out dStrings2Strings2Strings,
                out dStrings2Strings
            );
        }

        #region protected void GetNameFromClassMemberName()

        /// <summary>Nullable</summary>
        protected void GetNameFromClassMemberName(
            ref Type oType,
            String sCMName,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out String oString
        )
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings2Strings = null;
                oString = null;
                return;
            }

            Dictionary<String, String> dStrings2Strings;
            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2MembersNames2Names,
                _tMAttribute.FullName,
                oType.FullName,
                sCMName,
                out dStrings2Strings2Strings,
                out dStrings2Strings,
                out oString
            );
        }

        /// <summary>Nullable</summary>
        protected void GetNameFromClassMemberName(
            ref Type oType, 
            String sCMName,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out Dictionary<String, String> dStrings2Strings, 
            out String oString
        )
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings2Strings = null;
                dStrings2Strings = null;
                oString = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2MembersNames2Names,
                _tMAttribute.FullName,
                oType.FullName,
                sCMName,
                out dStrings2Strings2Strings,
                out dStrings2Strings,
                out oString
            );
        }

        #endregion

        /// <summary>Nullable</summary>
        protected void Names2ClassMembersNames(
            ref Type oType,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings, 
            out Dictionary<String, String> dStrings2Strings
        )
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings2Strings = null;
                dStrings2Strings = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2Names2MembersNames,
                _tMAttribute.FullName,
                oType.FullName,
                out dStrings2Strings2Strings,
                out dStrings2Strings
            );
        }

        #region protected void GetClassMemberNameFromName()

        /// <summary>Nullable</summary>
        protected void GetClassMemberNameFromName(
            ref Type oType,
            String sName,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out String sCMemberName
            )
        {
            Dictionary<String, String> dStrings2Strings;
            GetClassMemberNameFromName(
                ref oType,
                sName,
                out dStrings2Strings2Strings,
                out dStrings2Strings,
                out sCMemberName
            );
        }

        /// <summary>Nullable</summary>
        protected void GetClassMemberNameFromName(
            ref Type oType,
            String sName,
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out Dictionary<String, String> dStrings2Strings,
            out String sCMemberName)
        {
            if (!Analyze(ref oType))
            {
                dStrings2Strings2Strings = null;
                dStrings2Strings = null;
                sCMemberName = null;
                return;
            }

            TryGetValueFromDictionary(
                ref _dAttributes2ClassFullNames2Names2MembersNames,
                _tMAttribute.FullName,
                oType.FullName,
                sName,
                out dStrings2Strings2Strings,
                out dStrings2Strings,
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
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out Dictionary<String, String> dStrings2Strings
        )
        {
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
            out Dictionary<String, Dictionary<String, String>> dStrings2Strings2Strings,
            out Dictionary<String, String> dStrings2Strings,
            out String oString
        )
        {
            if (!TryGetValueFromDictionary(ref dStrings2Strings2Strings2Strings, sKey0, out dStrings2Strings2Strings))
            {
                dStrings2Strings = null;
                oString = null;
                return false;
            }
            else if(!TryGetValueFromDictionary(ref dStrings2Strings2Strings, sKey1, out dStrings2Strings))
            {
                oString = null;
                return false;
            }
            else if (!TryGetValueFromDictionary(ref dStrings2Strings, sKey2, out oString))
                return false;

            return true;
        }

        protected static Boolean TryGetValueFromDictionary(
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

        protected static Boolean TryGetValueFromDictionary(
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
    }
}