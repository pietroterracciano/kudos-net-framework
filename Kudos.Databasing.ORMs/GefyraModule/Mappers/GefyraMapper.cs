using Kudos.Databasing.ORMs.GefyraModule.Attributes;
using Kudos.Databasing.ORMs.GefyraModule.Types;
using Kudos.Databasing.ORMs.GefyraModule.Utils;
using Kudos.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Mappings.Controllers
{
    internal static class GefyraMapper
    {
        private static readonly String
            __sTableConventionPrefix = "tbl";

        private static readonly Object
            __oLock = new Object();

        private static readonly BindingFlags
            __eBFConstructor =
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance,
            __eBFGetMembers =
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.SetProperty
                | BindingFlags.SetField
                | BindingFlags.DeclaredOnly;

        private static readonly MemberTypes
            __eMTGetMembers =
                MemberTypes.Field
                | MemberTypes.Property;

        private static Dictionary<String, GefyraTable>
            __dCFullNames2Tables = new Dictionary<String, GefyraTable>();

        private static Dictionary<String, Dictionary<MemberInfo, GefyraColumn>>
            __dCFullNames2CMembers2Columns = new Dictionary<String, Dictionary<MemberInfo, GefyraColumn>>();

        private static Boolean __GetValue<InnerValueKey, InnerValueType>
        (
            ref Dictionary<InnerValueKey, InnerValueType>? d,
            ref InnerValueKey? ik,
            out InnerValueType? iv
        )
        {
            if (d == null || ik == null || !d.TryGetValue(ik, out iv))
            {
                iv = default(InnerValueType); 
                return false; 
            }
            return iv != null;
        }

        #region Tables

        internal static Boolean GetTable<ObjectType>(out GefyraTable? tbl)
        {
            Type t = typeof(ObjectType);
            return GetTable(ref t, out tbl);
        }

        internal static Boolean GetTable(ref Type? t, out GefyraTable? tbl)
        {
            if (!Analyze(ref t)) { tbl = null; return false; }
            String? s = t != null ? t.FullName : null;
            return __GetValue(ref __dCFullNames2Tables, ref s, out tbl);
        }

        #endregion

        #region Columns

        internal static Boolean GetColumn<ObjectType>(ref String? s, out GefyraColumn? clm)
        {
            Type t = typeof(ObjectType);
            return GetColumn(ref t, ref s, out clm);
        }

        internal static Boolean GetColumn(ref Type? t, ref String? s, out GefyraColumn? clm)
        {
            MemberInfo? mi = MemberUtils.Get(t, s, __eBFGetMembers, __eMTGetMembers);
            return GetColumn(ref t, ref mi, out clm);
        }

        internal static Boolean GetColumn<ObjectType>(ref MemberInfo? mi, out GefyraColumn? clm)
        {
            Type t = typeof(ObjectType);
            return GetColumn(ref t, ref mi, out clm);
        }
        internal static Boolean GetColumn(ref Type? t, ref MemberInfo? mi, out GefyraColumn? clm)
        {
            if (!Analyze(ref t)) { clm = null; return false; }
            String? s0 = t != null ? t.FullName : null;
            Dictionary<MemberInfo, GefyraColumn>? d;
            if (!__GetValue(ref __dCFullNames2CMembers2Columns, ref s0, out d)) { clm = null; return false; }
            return __GetValue(ref d, ref mi, out clm);
        }

        #endregion

        internal static Boolean Parse<ObjectType>(ref DataRow dr, out ObjectType? o)
        {
            Type t = typeof(ObjectType);
            
            Object? o1;
            if (Parse(ref t, ref dr, out o1))
                o = ObjectUtils.Cast<ObjectType>(o1);
            else
                o = default(ObjectType);

            return o != null;
        }
        internal static Boolean Parse(ref Type? t, ref DataRow dr, out Object? o)
        {
            o = ConstructorUtils.Invoke(ConstructorUtils.Get(t, __eBFConstructor));

            if (o == null)
                return false;

            GefyraTable tbl;
            if 
            (
                dr == null 
                || !GetTable(ref t, out tbl)
            )
                return true;

            DataColumn dc;
            for(int i=0; i< dr.Table.Columns.Count; i++)
            {
                dc = dr.Table.Columns[i];

                GefyraColumn?
                    clm = tbl.GetColumn(dc.ColumnName);

                if (clm == null || clm.Member == null)
                    continue;

                MemberUtils.SetValue(o, clm.Member, dr[dc.ColumnName]);
            }

            return true;
        }

        #region Analyze(...)

        private static Boolean Analyze(ref Type? oType)
        {
            if (oType == null) 
                return false;

            lock (__oLock)
            {
                GefyraTable oTable;
                if (__dCFullNames2Tables.TryGetValue(oType.FullName, out oTable))
                    return true;

                GefyraTableAttribute? attTable;

                #region Recupero l'Attribute per la Class

                attTable = MemberUtils.GetAttribute<GefyraTableAttribute>(oType, true);

                #endregion

                if (attTable == null) return false;

                #region Aggiungo l'Attribute recupera in precedenza ai Dictionaries corrispondenti

                String? sTSchemaName, sTName;

                if (attTable.IsWhole)
                {
                    sTSchemaName = attTable.SchemaName;
                    sTName = attTable.Name;
                }
                else
                {
                    sTSchemaName = null;
                    sTName = __sTableConventionPrefix + oType.Name;
                }

                oTable = __dCFullNames2Tables[oType.FullName] = new GefyraTable(ref sTSchemaName, ref sTName, ref oType);

                #endregion

                MemberInfo[]? aMembers;

                #region Recupero tutti i Members della Class e popolo i Dictionaries corrispondenti

                aMembers = MemberUtils.Get(oType, __eBFGetMembers, __eMTGetMembers);

                if (aMembers != null)
                {
                    //__dCFullNames2CMembers[oType.FullName] = aMembers;
                    //__dCFullNames2CMembersNames2CMembers[oType.FullName] = new Dictionary<String, MemberInfo>(aMembers.Length);
                    __dCFullNames2CMembers2Columns[oType.FullName] = new Dictionary<MemberInfo, GefyraColumn>(aMembers.Length);

                    GefyraColumnAttribute?
                        attDataRow;

                    String
                        sColumnName;

                    String?
                        sConventionPrefix;

                    for (int i = 0; i < aMembers.Length; i++)
                    {
                        if (aMembers[i] == null) continue;

                        #region Recupero l'Attribute del Member i-esimo e lo aggiungo ai Dictionaries corrispondenti

                        attDataRow = MemberUtils.GetAttribute<GefyraColumnAttribute>(aMembers[i], true);
                        if (attDataRow != null && attDataRow.IsWhole)
                            sColumnName = attDataRow.Name;
                        else
                        {
                            Type tMemberi = MemberUtils.GetValueType(aMembers[i]);
                            if(!GefyraTypeUtils.GetConventionPrefix(ref tMemberi, out sConventionPrefix))
                                sColumnName = aMembers[i].Name;
                            else
                                sColumnName = sConventionPrefix + aMembers[i].Name;
                        }

                        __dCFullNames2CMembers2Columns[oType.FullName][aMembers[i]] = oTable.GetColumn(ref sColumnName, ref aMembers[i]);

                        #endregion
                    }

                }

                #endregion

                return true;
            }
        }

        #endregion
    }
}