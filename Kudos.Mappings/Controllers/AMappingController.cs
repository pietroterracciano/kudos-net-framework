using Kudos.Constants;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Controllers
{
    public abstract class AMappingController<ObjectType, AttributeType>
        where AttributeType : Attribute
    {
        [Flags]
        private enum EDirection
        {
            Original2NotOriginal = CBinaryFlag._0,
            NotOriginal2Original = CBinaryFlag._1
        }

        private static readonly Object
            SRO__oLock = new Object();

        private static readonly HashSet<String>
            SRO__hsAnalyzed = new HashSet<String>();

        private static Dictionary<String, Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>>>
            SRO__dCFullNames2AFullNames2Directions2Names2Names = new Dictionary<String, Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>>>();

        private readonly Type
            _tAttribute,
            _tObject;

        private readonly String
            _sAnalyzeKey;

        public AMappingController()
        {
            _tAttribute = typeof(AttributeType);
            _tObject = typeof(ObjectType);
            _sAnalyzeKey = _tObject.FullName + _tAttribute.FullName;

            lock (SRO__oLock)
            {
                if (SRO__hsAnalyzed.Contains(_sAnalyzeKey)) return;

                SRO__hsAnalyzed.Add(_sAnalyzeKey);

                AttributeType
                   oAttribute;

                String
                    sRule;

                Dictionary<String, String>
                    dONames2NONames,
                    dNONames2ONames;

                #region Popolo delle variabili di supporto

                Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>> dAFullNames2Directions2Names2Names;
                AddGetValueFromDictionary(ref SRO__dCFullNames2AFullNames2Directions2Names2Names, _tObject.FullName, out dAFullNames2Directions2Names2Names);
                Dictionary<EDirection, Dictionary<String, String>> dDirections2Names2Names;
                AddGetValueFromDictionary(ref dAFullNames2Directions2Names2Names, _tAttribute.FullName, out dDirections2Names2Names);
                AddGetValueFromDictionary(ref dDirections2Names2Names, EDirection.Original2NotOriginal, out dONames2NONames);
                AddGetValueFromDictionary(ref dDirections2Names2Names, EDirection.NotOriginal2Original, out dNONames2ONames);

                #endregion

                #region Recupero l'Attribute per la Class e lo aggiungo ai Dictionaries corrispondenti

                oAttribute = ObjectUtils.GetClassAttribute<AttributeType>(_tObject, true);

                if (oAttribute != null)
                {
                    sRule = GetRuleFromAttribute(oAttribute);
                    dONames2NONames[_tObject.Name] = sRule;
                    dNONames2ONames[sRule] = _tObject.Name;
                }

                #endregion

                MemberInfo[]
                   aMembers;

                #region Recupero tutti i Members della Class

                int
                    iPLength = 0,
                    iFLength = 0;

                PropertyInfo[]
                    aProperties =
                        ObjectUtils.GetProperties(
                            _tObject,
                            BindingFlags.Public
                            | BindingFlags.Instance
                            | BindingFlags.Static
                            | BindingFlags.SetProperty
                        );

                if (aProperties != null)
                    iPLength += aProperties.Length;

                FieldInfo[]
                    aFields =
                        ObjectUtils.GetFields(
                            _tObject,
                            BindingFlags.Public
                            | BindingFlags.Instance
                            | BindingFlags.Static
                            | BindingFlags.SetField
                        );

                if (aFields != null)
                    iFLength += aFields.Length;

                aMembers = new MemberInfo[iPLength + iFLength];

                if (aMembers.Length > 0)
                {
                    for (int i = 0; i < iPLength; i++)
                        aMembers[i] = aProperties[i];

                    for (int i = 0; i < iFLength; i++)
                        aMembers[i + iPLength] = aFields[i];
                }

                #endregion

                #region Analizzo l'Attribute per tutti i Members recuperati in precedenza e popolo i Dictionaries corrispondenti

                for (int i = 0; i < aMembers.Length; i++)
                {
                    if (aMembers[i] == null)
                        continue;

                    #region Recupero l'Attribute del Member i-esimo e lo aggiungo ai Dictionaries corrispondenti

                    oAttribute = ObjectUtils.GetMemberAttribute<AttributeType>(aMembers[i], true);

                    if (oAttribute == null) continue;

                    sRule = GetRuleFromAttribute(oAttribute);

                    dONames2NONames[aMembers[i].Name] = sRule;
                    dNONames2ONames[sRule] = aMembers[i].Name;

                    #endregion
                }

                #endregion
            }
        }

        protected abstract String GetRuleFromAttribute(AttributeType oCAttribute);

        #region private static void AddGetValueFromDictionary()

        private static void AddGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>>> dInput,
            String sKey,
            out Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>> dOutput
        )
        {
            if (!TryGetValueFromDictionary(ref dInput, sKey, out dOutput))
                dInput[sKey] = dOutput = new Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>>();
        }

        private static void AddGetValueFromDictionary(
            ref Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>> dInput,
            String sKey,
            out Dictionary<EDirection, Dictionary<String, String>> dOutput
        )
        {
            if (!TryGetValueFromDictionary(ref dInput, sKey, out dOutput))
                dInput[sKey] = dOutput = new Dictionary<EDirection, Dictionary<String, String>>();
        }

        private static void AddGetValueFromDictionary(
            ref Dictionary<EDirection, Dictionary<String, String>> dInput,
            EDirection eDirection,
            out Dictionary<String, String> dOutput
        )
        {
            if (!TryGetValueFromDictionary(ref dInput, eDirection, out dOutput))
                dInput[eDirection] = dOutput = new Dictionary<String, String>();
        }

        #endregion

        #region private static Boolean TryGetValueFromDictionary()

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>>> dInput,
            String sKey,
            out Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>> dOutput
        )
        {
            return dInput.TryGetValue(sKey, out dOutput) && dOutput != null;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<String, Dictionary<EDirection, Dictionary<String, String>>> dInput,
            String sKey,
            out Dictionary<EDirection, Dictionary<String, String>> dOutput
        )
        {
            return dInput.TryGetValue(sKey, out dOutput) && dOutput != null;
        }

        private static Boolean TryGetValueFromDictionary(
            ref Dictionary<EDirection, Dictionary<String, String>> dInput,
            EDirection eDirection,
            out Dictionary<String, String> dOutput
        )
        {
            return dInput.TryGetValue(eDirection, out dOutput) && dOutput != null;
        }

        #endregion
    }
}