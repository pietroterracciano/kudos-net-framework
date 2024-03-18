using Kudos.Databases.Controllers;
using Kudos.Databases.Enums;
using Kudos.Databases.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Models;
using Kudos.Mappings.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule
{
    public static class Gefyra
    {
        //public static IGefyraContext<ModelType> NewContext<ModelType>(IDBController? oDBController = null)
        //{
        //    IDBController? oDBController1 = oDBController != null ? oDBController : Config.DefaultDBController;
        //    return new GefyraContext<ModelType, Object>(ref oDBController1);
        //}

        //public static IGefyraCommandBuilder NewCommandBuilder(EDatabaseType eDBType = EDatabaseType.Unknown)
        //{
        //    EDatabaseType eDBType1 = eDBType != EDatabaseType.Unknown ? eDBType : ( Config.DefaultDBController != null ? Config.DefaultDBController.Type : EDatabaseType.Unknown);
        //    return new NTRGefyraCommandBuilder(eDBType1);
        //}

        public static IGefyraCommandBuilder? NewCommandBuilder(EDatabaseType? edbt)
        {
            return edbt != null
                ? new NTRGefyraCommandBuilder(edbt.Value)
                : null;
        }

        public static GefyraTable? GetTable<ObjectType>()
        {
            GefyraTable? tbl;
            GefyraMapper.GetTable<ObjectType>(out tbl);
            return tbl;
        }

        public static GefyraColumn? GetColumn<ObjectType>(String? s)
        {
            GefyraColumn? clm;
            GefyraMapper.GetColumn<ObjectType>(ref s, out clm);
            return clm;
        }

        public static ObjectType? Parse<ObjectType>(DataRow? dr)
        {
            ObjectType? o;
            GefyraMapper.Parse(ref dr, out o);
            return o;
        }


        //public static GefyraTable TableOf(String sName)
        //{
        //    return TableOf(null, sName);
        //}
        //public static GefyraTable TableOf(String sSchemaName, String sName)
        //{
        //    lock (__oLock)
        //    {
        //        __oStringBuilder.Length = 0;
        //        if (!String.IsNullOrWhiteSpace(sSchemaName)) __oStringBuilder.Append(sSchemaName).Append(CGefyraSeparator.Dot);
        //        if (!String.IsNullOrWhiteSpace(sName)) __oStringBuilder.Append(sName);
        //        String sFullName = __oStringBuilder.ToString();

        //        GefyraTable o;
        //        if (__dTFullNames2Tables.TryGetValue(sFullName, out o) && o != null)
        //            return o;

        //        Type tFakeModel = null;
        //        return __dTFullNames2Tables[sFullName] = new GefyraTable(ref tFakeModel, ref sSchemaName, ref sName);
        //    }
        //}

        //public static GefyraColumn ColumnOf(Type oType, String sMemberName)
        //{
        //    return new GefyraColumn(ref oType, ref sMemberName);
        //}
        //public static GefyraColumn ColumnOf(String sTableName, String sColumnName)
        //{
        //    return ColumnOf(null, sTableName, sColumnName);
        //}
        //public static GefyraColumn ColumnOf(String sSchemaName, String sTableName, String sColumnName)
        //{
        //    return ColumnOf(TableOf(sSchemaName, sTableName), sColumnName);
        //}
        //public static GefyraColumn ColumnOf(GefyraTable mTable, String sColumnName)
        //{
        //    return new GefyraColumn(ref mTable, ref sColumnName);
        //}
    }
}
