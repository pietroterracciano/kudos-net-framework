using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Controllers
{
    abstract class MappingController<ClassAttributeType, MemberAttributeType> 
        where ClassAttributeType : Attribute 
        where MemberAttributeType : Attribute
    {
        private static readonly Object
            _oLock = new Object();

        private static readonly HashSet<String>
            _hsAnalyzedClassesFullNames = new HashSet<String>();

        private static Dictionary<String, Dictionary<String, String>>
            _dAttributesNames2ClassFullNames2Names = new Dictionary<String, Dictionary<String, String>>(),
            _dAttributesNames2Names2ClassFullNames = new Dictionary<String, Dictionary<String, String>>();

        private static Dictionary<String, Dictionary<String, Dictionary<String, String>>>
            _dAttributes2ClassFullNames2MembersNames2Names = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>(),
            _dAttributes2ClassFullNames2Names2MembersNames = new Dictionary<String, Dictionary<String, Dictionary<String, String>>>();

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

                Type
                    tCAttribute = typeof(ClassAttributeType);

                ClassAttributeType
                    oCAttribute = ObjectUtils.GetClassAttribute<ClassAttributeType>(oType);

                String
                    sCRule =
                        oCAttribute != null
                            ? GetRuleFromClassAttribute(oCAttribute)
                            : oType.FullName;

                PopulateDictionary(
                    ref _dAttributesNames2ClassFullNames2Names,
                    tCAttribute.FullName,
                    oType.FullName,
                    sCRule
                );

                PopulateDictionary(
                    ref _dAttributesNames2Names2ClassFullNames,
                    tCAttribute.FullName,
                    sCRule,
                    oType.FullName
                );

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

                #region Analizzo l'Attribute per tutti i Members recuperati in precedenza

                for (int i = 0; i < lMembers.Count; i++)
                {
                    if (lMembers[i] == null)
                        continue;

                    #region Recupero l'Attribute del Member i-esimo e lo aggiungo ai Dictionaries corrispondenti

                    Type
                        tMAttribute = typeof(MemberAttributeType);

                    MemberAttributeType
                        oMAttribute = ObjectUtils.GetMemberAttribute<MemberAttributeType>(lMembers[i], true);

                    String
                        sMRule =
                            oMAttribute != null
                                ? GetRuleFromMemberAttribute(oMAttribute)
                                : lMembers[i].Name;

                    #region Popolo il Dictionary normale

                    PopulateDictionary(
                        ref _dAttributes2ClassFullNames2MembersNames2Names,
                        tMAttribute.FullName,
                        oType.FullName,
                        lMembers[i].Name,
                        sMRule
                    );

                    #endregion

                    #region Popolo il Dictionary inverso

                    PopulateDictionary(
                        ref _dAttributes2ClassFullNames2Names2MembersNames,
                        tMAttribute.FullName,
                        oType.FullName,
                        sMRule,
                        lMembers[i].Name
                    );

                    #endregion

                    #endregion
                }

                #endregion

                return true;
            }
        }

        /// <summary>Nullable</summary>
        private void GetClassName2Name(Type oType, String sAttributeFullName, out String sTableName)
        {
            if (!Analyze(ref oType))
            {
                sTableName = null;
                return;
            }

            TryGetValueFromDictionary(ref _dClassesFullNames2AttributesFullNames2TablesNames, oType.FullName, sAttributeFullName, out sTableName);
        }

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

            if (sKey0 == null)
                return false;

            Dictionary<String, Dictionary<String, String>>
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
    }
}
