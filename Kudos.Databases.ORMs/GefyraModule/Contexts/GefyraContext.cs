using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using Kudos.Constants;
using Kudos.Databases.Enums;
using Kudos.Databases.Enums.Columns;
using Kudos.Databases.Interfaces;
using Kudos.Databases.Models;
using Kudos.Databases.Models.Results;
using Kudos.Databases.Models.Schemas;
using Kudos.Databases.Models.Schemas.Columns;
using Kudos.Databases.ORMs.GefyraModule.Attributes;
using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Constants;
using Kudos.Databases.ORMs.GefyraModule.Entities;
using Kudos.Databases.ORMs.GefyraModule.Enums;
using Kudos.Databases.ORMs.GefyraModule.Interfaces;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts.Builders;
using Kudos.Databases.ORMs.GefyraModule.Models.Commands.Builders;
using Kudos.Databases.ORMs.GefyraModule.Models.Contexts;
using Kudos.Databases.ORMs.GefyraModule.Models.Contexts.LazyLoads;
using Kudos.Databases.ORMs.GefyraModule.Models.Contexts.LazyLoads.Actions;
using Kudos.Databases.ORMs.GefyraModule.Utils;
using Kudos.Enums;
using Kudos.Mappings.Controllers;
using Kudos.Reflection.DumpModule.Controllers;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Members;
using Kudos.Utils.Numerics.Integers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Mysqlx.Crud;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Utilities;
using static Mysqlx.Notice.Warning.Types;

namespace Kudos.Databases.ORMs.GefyraModule.Contexts
{
    internal sealed class
        GefyraContext<ModelType, ExecuteReturnType>
    :
        IGefyraContext<ModelType>,
        IGefyraContextInsertClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextSelectClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextDeleteClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextUpdateClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextJoinClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextWhereClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextLimitClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextOffsetClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextExecuteClausoleBuilder<ModelType, ExecuteReturnType>,
        IGefyraContextRawClausoleBuilder<ModelType>
    {
        private static String
            __sTableAliasPrefix = "GCTable";

        private static Type
            __tModel = typeof(ModelType);

        private static readonly BindingFlags
            __eBFConstructor = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private static readonly Dictionary<Type, ConstructorInfo>
            __dTypes2Constructors = new Dictionary<Type, ConstructorInfo>();

        //private static readonly ConstructorInfo
        //    __ciModel = ObjectUtils.GetConstructor(__tModel, __eBFConstructor);

        private static readonly Object
            __oLock = new Object();

        private IDBController?
            _oDBController;

        private readonly List<Type>
            _lTypes;

        private readonly List<GCLazyLoadModel>
            _lLazyLoads;

        internal Int32
            _iAliasLevel;

        private readonly Dictionary<GefyraTable, Dictionary<Int32, GefyraTable>>
            _dTables2AliasLevels2AliasizedTables;

        private readonly Dictionary<ParameterExpression, GefyraTable>
            _dEParameters2AliasizedTables;

        internal GefyraContext(ref IDBController? oDBController)
        {
            _oDBController = oDBController;
            _lTypes = new List<Type>() { __tModel };
            _lLazyLoads = new List<GCLazyLoadModel>();
            _dTables2AliasLevels2AliasizedTables = new Dictionary<GefyraTable, Dictionary<Int32, GefyraTable>>();
            _dEParameters2AliasizedTables = new Dictionary<ParameterExpression, GefyraTable>();
        }

        private void AppendLazyLoad(EGefyraClausole eClausole, params Object[]? aPayLoads)
        {
            _lLazyLoads.Add(new GCLazyLoadModel(eClausole, aPayLoads));
        }

        #region Insert()

        private GefyraContext<ModelType, ModelType> prv_insert()
        {
            GefyraContext<ModelType, ModelType> oInner = new GefyraContext<ModelType, ModelType>(ref _oDBController);
            oInner.AppendLazyLoad(EGefyraClausole.Insert);
            return oInner;
        }

        public IGefyraContextInsertClausoleBuilder<ModelType, ModelType> Insert(Action<ModelType>? oAction)
        {
            GefyraContext<ModelType, ModelType> oInner = prv_insert();
            oInner.AppendLazyLoad(EGefyraClausole.Into | EGefyraClausole.Values, oAction);
            return oInner;
        }

        public IGefyraContextInsertClausoleBuilder<ModelType, ModelType> Insert(ModelType? oModel)
        {
            GefyraContext<ModelType, ModelType> oInner = prv_insert();
            oInner.AppendLazyLoad(EGefyraClausole.Into | EGefyraClausole.Values, oModel);
            return oInner;
        }

        #endregion

        #region Select()

        private GefyraContext<InnerModelType, InnerExecuteReturnType> prv_Select<InnerModelType, InnerExecuteReturnType>(EGefyraClausole eClausole)
        {
            GefyraContext<InnerModelType, InnerExecuteReturnType> oInner = new GefyraContext<InnerModelType, InnerExecuteReturnType>(ref _oDBController);
            oInner.AppendLazyLoad(eClausole);
            oInner.AppendLazyLoad(EGefyraClausole.From);
            return oInner;
        }

        public IGefyraContextSelectClausoleBuilder<ModelType, ModelType[]> Select()
        {
            return prv_Select<ModelType, ModelType[]>(EGefyraClausole.Select);
        }

        public IGefyraContextWhereClausoleBuilder<ModelType, ModelType[]> Select(Expression<Func<ModelType, Boolean>>? oExpression)
        {
            GefyraContext<ModelType, ModelType[]> oInner = prv_Select<ModelType, ModelType[]>(EGefyraClausole.Select);
            return oInner.Where(oExpression);
        }

        #endregion

        #region Count()

        public IGefyraContextSelectClausoleBuilder<ModelType, UInt64> Count()
        {
            return prv_Select<ModelType, UInt64>(EGefyraClausole.Count);
        }

        public IGefyraContextWhereClausoleBuilder<ModelType, UInt64> Count(Expression<Func<ModelType, Boolean>>? oExpression)
        {
            GefyraContext<ModelType, UInt64> oInner = prv_Select<ModelType, UInt64>(EGefyraClausole.Count);
            return oInner.Where(oExpression);
        }

        #endregion

        #region Delete()

        private GefyraContext<ModelType, Boolean> prv_Delete()
        {
            GefyraContext<ModelType, Boolean> oInner = new GefyraContext<ModelType, Boolean>(ref _oDBController);
            oInner.AppendLazyLoad(EGefyraClausole.Delete);
            oInner.AppendLazyLoad(EGefyraClausole.From, __tModel);
            return oInner;
        }

        public IGefyraContextDeleteClausoleBuilder<ModelType, Boolean> Delete(Expression<Func<ModelType, Boolean>>? oExpression)
        {
            GefyraContext<ModelType, Boolean> oInner = prv_Delete();
            oInner.Where(oExpression);
            return oInner;
        }

        public IGefyraContextDeleteClausoleBuilder<ModelType, Boolean> Delete(ModelType? oModel)
        {
            GefyraContext<ModelType, Boolean> oInner = prv_Delete();
            oInner._Where(oModel);
            return oInner;
        }

        #endregion

        #region Update()

        private GefyraContext<ModelType, ModelType> NewUpdate()
        {
            GefyraContext<ModelType, ModelType> oInner = new GefyraContext<ModelType, ModelType>(ref _oDBController);
            //oInner.RequestCommandBuilder().Update(__tModel.Name);
            return oInner;
        }

        public IGefyraContextUpdateClausoleBuilder<ModelType, ModelType> Update(ModelType? oModel)
        {
            GefyraContext<ModelType, ModelType> oInner = NewUpdate();
            //oInner.PrepareLazyLoad(ref oModel);
            return oInner;
        }

        public IGefyraContextUpdateClausoleBuilder<ModelType, ModelType> Update(Action<ModelType>? oAction)
        {
            GefyraContext<ModelType, ModelType> oInner = NewUpdate();
            //oInner.PrepareLazyLoad(ref oAction);
            return oInner;
        }

        #endregion

        #region Join()

        public IGefyraContextJoinClausoleBuilder<ModelType, ExecuteReturnType> Join<JoinModelType>(EGefyraJoin eType, Expression<Func<ModelType, JoinModelType, Boolean>> oExpression)
        {
            //AppendLazyLoad(EGefyraClausole.Join, eType, oExpression);
            return this;
        }

        #endregion

        #region Where()

        public IGefyraContextWhereClausoleBuilder<ModelType, ExecuteReturnType> Where(Expression<Func<ModelType, Boolean>>? oExpression)
        {
            AppendLazyLoad(EGefyraClausole.Where, oExpression);
            return this;
        }

        private IGefyraContextWhereClausoleBuilder<ModelType, ExecuteReturnType> _Where(ModelType oModel)
        {
            AppendLazyLoad(EGefyraClausole.Where, oModel);
            return this;
        }

        #endregion

        #region GroupBy()

        public IGefyraContextGroupByClausoleBuilder<ModelType, ExecuteReturnType> GroupBy(Expression<Func<ModelType, Object>>? oExpression)
        {
            return null;
        }

        #endregion

        #region OrderBy()

        public IGefyraContextOrderByClausoleBuilder<ModelType, ExecuteReturnType> OrderBy(Expression<Func<ModelType, Object>>? oExpression, EGefyraOrdering eOrdering)
        {
            return null;
        }

        #endregion

        #region Limit()

        public IGefyraContextLimitClausoleBuilder<ModelType, ExecuteReturnType> Limit(Int32 oInteger)
        {
            AppendLazyLoad(EGefyraClausole.Limit, oInteger);
            return this;
        }

        #endregion

        #region Offset()

        public IGefyraContextOffsetClausoleBuilder<ModelType, ExecuteReturnType> Offset(Int32 oInteger)
        {
            AppendLazyLoad(EGefyraClausole.Offset, oInteger);
            return this;
        }

        #endregion

        #region Raw()

        public IGefyraContextRawClausoleBuilder<ModelType> Raw()
        {
            return new GefyraContext<ModelType, ModelType>(ref _oDBController);
        }

        #endregion

        #region Execute()

        public ModelType[] ExecuteQuery(IGefyraCommandBuilder oCommandBuilder)
        {
            return null;
        }

        public Boolean ExecuteNonQuery(IGefyraCommandBuilder oCommandBuilder)
        {
            return false;
        }

        public ExecuteReturnType Execute()
        {
            ADBCommandResult mDBCommandResult;
            return Execute(out mDBCommandResult);
        }

        public ExecuteReturnType Execute(out ADBCommandResult oDBCommandResult)
        {
            oDBCommandResult = null;

            lock (__oLock)
            {
                for (int i = 0; i < _lTypes.Count; i++)
                {
                    Type oTypei = _lTypes[i];
                    if (!FetchInformationSchemaColumnsFromDataBaseAndInjectIntoEntities(ref oTypei))
                        return default(ExecuteReturnType);
                }
            }

            NTRGefyraCommandBuilder 
                oCommandBuilder = new NTRGefyraCommandBuilder(_oDBController.Type);

            for (int i = 0; i < _lLazyLoads.Count; i++)
                PrepareCommandBuilder(ref oCommandBuilder, _lLazyLoads[i]);

            GefyraCommandBuilt 
                oCommandBuilt = oCommandBuilder.Build();

            switch(oCommandBuilt.Action)
            {
                case EGefyraAction.Select:
                case EGefyraAction.Count:
                    oDBCommandResult = _oDBController.ExecuteQueryCommand(oCommandBuilt.Text, oCommandBuilt.Pagination.RowsPerPage, oCommandBuilt.GetParameters());
                    break;
                default:
                    oDBCommandResult = _oDBController.ExecuteNonQueryCommand(oCommandBuilt.Text, oCommandBuilt.GetParameters());
                    break;
            }

            if (!oDBCommandResult.IsDone())
                return default(ExecuteReturnType);

            ModelType[]
                aModels;

            switch (oCommandBuilt.Action)
            {
                case EGefyraAction.Select:
                case EGefyraAction.Count:
                    DBQueryCommandResultModel
                        mDBQueryCommandResult = (DBQueryCommandResultModel)oDBCommandResult;

                    if (mDBQueryCommandResult.Data == null)
                        break;
                    else if (oCommandBuilt.Action == EGefyraAction.Count)
                        return
                            mDBQueryCommandResult.Data.Rows.Count == 1
                                ? ObjectUtils.Cast<ExecuteReturnType>(UInt64Utils.From(mDBQueryCommandResult.Data.Rows[0][0]))
                                : default(ExecuteReturnType);

                    aModels = new ModelType[mDBQueryCommandResult.Data.Rows.Count];

                    for (int i = 0; i < mDBQueryCommandResult.Data.Rows.Count; i++)
                        GefyraMapper.From<ModelType>(mDBQueryCommandResult.Data.Rows[i], out aModels[i]);

                    return ObjectUtils.Cast<ExecuteReturnType>(aModels);
                default:
                    DBNonQueryCommandResultModel
                        mDBNonQueryCommandResult = (DBNonQueryCommandResultModel)oDBCommandResult;

                    switch (oCommandBuilt.Action)
                    {
                        case EGefyraAction.Delete:
                            return ObjectUtils.Cast<ExecuteReturnType>(mDBNonQueryCommandResult.UpdatedRows > 0);

                        case EGefyraAction.Insert:

                            if (mDBNonQueryCommandResult.UpdatedRows < 1)
                                break;

                            GefyraTable oTable;
                            if (!GefyraMapper.GetTable(ref __tModel, out oTable))
                                break;

                            GefyraColumn[]
                                aTColumns = oTable.GetColumns();

                            List<GefyraColumn>
                                lAutoIncrementsColumns = new List<GefyraColumn>(1),
                                lPrimariesColumns = new List<GefyraColumn>(aTColumns.Length),
                                lUniquesColumns = new List<GefyraColumn>(aTColumns.Length),
                                lOtherColumns = new List<GefyraColumn>(aTColumns.Length);

                            for (int i = 0; i < aTColumns.Length; i++)
                            {
                                if (aTColumns[i].DBInformationSchema == null)
                                    continue;
                                if (aTColumns[i].DBInformationSchema.Extras.HasFlag(EDBColumnExtra.AutoIncrement))
                                    lAutoIncrementsColumns.Add(aTColumns[i]);
                                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.Primary))
                                    lPrimariesColumns.Add(aTColumns[i]);
                                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.Unique))
                                    lUniquesColumns.Add(aTColumns[i]);
                                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.None))
                                    lOtherColumns.Add(aTColumns[i]);
                            }

                            List<GefyraColumn>
                                lColumns2Use = new List<GefyraColumn>(aTColumns.Length);

                            if (lAutoIncrementsColumns.Count > 0)
                                lColumns2Use.AddRange(lAutoIncrementsColumns);
                            else if (lPrimariesColumns.Count > 0)
                                lColumns2Use.AddRange(lPrimariesColumns);
                            else if (lUniquesColumns.Count > 0)
                                lColumns2Use.AddRange(lUniquesColumns);
                            else
                                lColumns2Use.AddRange(lOtherColumns);

                            Dictionary<GefyraColumn, GCBParameterModel>
                                dPReferencedColumns2Parameters = new Dictionary<GefyraColumn, GCBParameterModel>(oCommandBuilt._aParameters.Length);

                            for (int i = 0; i < oCommandBuilt._aParameters.Length; i++)
                                dPReferencedColumns2Parameters[oCommandBuilt._aParameters[i].ReferencedColumn] = oCommandBuilt._aParameters[i];

                            ConstantExpression expRightConstant;
                            Expression expLeftPropertyOrField, expLeftEqualRight;
                            Expression expLastLeftEqualRight = null;
                            ParameterExpression expParameter = Expression.Parameter(__tModel, __tModel.Name);

                            for (int i = 0; i < lColumns2Use.Count; i++)
                            {
                                Object oPValue;
                                if (lColumns2Use[i].DBInformationSchema.Extras.HasFlag(EDBColumnExtra.AutoIncrement))
                                    oPValue = mDBNonQueryCommandResult.LastInsertedID;
                                else
                                {
                                    GCBParameterModel mParameter;

                                    oPValue =
                                        dPReferencedColumns2Parameters.TryGetValue(lColumns2Use[i], out mParameter) && mParameter != null
                                            ? mParameter.Value
                                            : null;
                                }

                                expLeftPropertyOrField = Expression.PropertyOrField(expParameter, lColumns2Use[i].Member.Name);

                                try { expRightConstant = Expression.Constant(ObjectUtils.ChangeType(oPValue, MemberUtils.GetValueType(lColumns2Use[i].Member))); }
                                catch { expLastLeftEqualRight = null; break; }

                                expLeftEqualRight = Expression.Equal(expLeftPropertyOrField, expRightConstant);

                                if (expLastLeftEqualRight == null)
                                {
                                    expLastLeftEqualRight = expLeftEqualRight;
                                    continue;
                                }

                                expLastLeftEqualRight = Expression.And(expLastLeftEqualRight, expLeftEqualRight);
                            }

                            if (expLastLeftEqualRight == null)
                                break;
                            
                            aModels =
                                Select(Expression<Func<ModelType, Boolean>>.Lambda(expLastLeftEqualRight, expParameter) as Expression<Func<ModelType, Boolean>>)
                                .Execute();

                            if (aModels == null || aModels.Length != 1)
                                break;

                            return ObjectUtils.Cast<ExecuteReturnType>(aModels[0]);
                    }

                    break;
            }

            return default(ExecuteReturnType);
        }

        #endregion

        #region TryGetConstructor(...)

        private Boolean TryGetConstructor(ref Type? oType, out ConstructorInfo? oConstructor)
        {
            if (oType == null)
            {
                oConstructor = null;
                return false;
            }

            lock (__oLock)
            {
                if (!__dTypes2Constructors.TryGetValue(oType, out oConstructor))
                {
                    oConstructor = ConstructorUtils.Get(oType, __eBFConstructor);
                    if (oConstructor == null) return false;
                    __dTypes2Constructors[oType] = oConstructor;
                }

                return true;
            }
        }

        #endregion


        #region PrepareModel

        private Boolean PrepareModel<InnerModelType>(ref Object? oObject, out InnerModelType? oModel)
        {
            oModel = ObjectUtils.Cast<InnerModelType>(oObject);

            if (oModel != null)
                return true;

            Action<InnerModelType>?
                oAction = oObject as Action<InnerModelType>;

            if (oAction == null)
                return false;

            Type tInnerModel = typeof(InnerModelType);
            ConstructorInfo ciModel;
            if (!TryGetConstructor(ref tInnerModel, out ciModel))
                return false;

            oModel = ConstructorUtils.Invoke<InnerModelType>(ciModel);

            if (oModel == null)
                return false;

            oAction.Invoke(oModel);
            return true;
        }

        #endregion

        #region PrepareLambdaExpression

        private Boolean PrepareLambdaExpression(ref Object? oObject, out LambdaExpression? expLambda)
        {
            expLambda = oObject as LambdaExpression;

            if (expLambda != null)
                return true;

            Type
                tObject = oObject.GetType();

            GefyraTable? oTable;
            if (!GefyraMapper.GetTable(ref tObject, out oTable))
            {
                expLambda = null;
                return false;
            }

            GefyraColumn[]
                aTColumns = oTable.GetColumns();

            if(aTColumns.Length < 1)
            {
                expLambda = null;
                return false;
            }    

            List<GefyraColumn>
                lAutoIncrementsColumns = new List<GefyraColumn>(1),
                lPrimariesColumns = new List<GefyraColumn>(aTColumns.Length),
                lUniquesColumns = new List<GefyraColumn>(aTColumns.Length),
                lOtherColumns = new List<GefyraColumn>(aTColumns.Length);

            for (int i = 0; i < aTColumns.Length; i++)
            {
                if (aTColumns[i].DBInformationSchema == null || aTColumns[i].Member == null)
                    continue;
                if (aTColumns[i].DBInformationSchema.Extras.HasFlag(EDBColumnExtra.AutoIncrement))
                    lAutoIncrementsColumns.Add(aTColumns[i]);
                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.Primary))
                    lPrimariesColumns.Add(aTColumns[i]);
                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.Unique))
                    lUniquesColumns.Add(aTColumns[i]);
                if (aTColumns[i].DBInformationSchema.Key.HasFlag(EDBColumnKey.None))
                    lOtherColumns.Add(aTColumns[i]);
            }

            List<GefyraColumn>
                lColumns2Use = new List<GefyraColumn>(aTColumns.Length);

            if (lAutoIncrementsColumns.Count > 0)
                lColumns2Use.AddRange(lAutoIncrementsColumns);
            else if (lPrimariesColumns.Count > 0)
                lColumns2Use.AddRange(lPrimariesColumns);
            else if (lUniquesColumns.Count > 0)
                lColumns2Use.AddRange(lUniquesColumns);
            else
                lColumns2Use.AddRange(lOtherColumns);

            if (lColumns2Use.Count < 1)
            {
                expLambda = null;
                return false;
            }

            ConstantExpression? expRightConstant;
            Expression? expLeftPropertyOrField, expLeftEqualRight, expLastLeftEqualRight = null;

            ParameterExpression expParameter;
            try { expParameter = Expression.Parameter(tObject, tObject.Name); } catch { expLambda = null; return false; }

            for (int i = 0; i < lColumns2Use.Count; i++)
            {
                try { expLeftPropertyOrField = Expression.PropertyOrField(expParameter, lColumns2Use[i].Member.Name); } catch { expLambda = null; return false; }
                try { expRightConstant = Expression.Constant(MemberUtils.GetValue(oObject, lColumns2Use[i].Member)); } catch { expLambda = null; return false; }
                try { expLeftEqualRight = Expression.Equal(expLeftPropertyOrField, expRightConstant); } catch { expLambda = null; return false; }
                if (expLastLeftEqualRight == null) { expLastLeftEqualRight = expLeftEqualRight; continue; }
                try { expLastLeftEqualRight = Expression.And(expLastLeftEqualRight, expLeftEqualRight); } catch { expLambda = null; return false; }
            }

            if (expLastLeftEqualRight == null)
            {
                expLambda = null;
                return false;
            }

            Type tELDelegate;
            if (!Expression.TryGetFuncType(new Type[] { tObject, CType.Boolean }, out tELDelegate))
            {
                expLambda = null;
                return false;
            }

            try
            {
                expLambda = Expression.Lambda(tELDelegate, expLastLeftEqualRight, expParameter);
            }
            catch
            {
                expLambda = null;
            }

            return expLambda != null;
        }


        #endregion

        #region PrepareCommandBuilder()

        //internal void PrepareCommandBuilder(out NTRGefyraCommandBuilder oCommandBuilder)
        //{
        //    oCommandBuilder = new NTRGefyraCommandBuilder(_oDBController.Type);

        //    if (_lLazyLoads == null)
        //        return;

        //    for (int i = 0; i < _lLazyLoads.Count; i++)
        //        PrepareCommandBuilder(ref oCommandBuilder, _lLazyLoads[i]);
        //}

        internal void PrepareCommandBuilder(
            ref NTRGefyraCommandBuilder oCommandBuilder, 
            GCLazyLoadModel mLazyLoad
        )
        {
            Object oObject;

            Object? oLLPayLoad0;

            GefyraTable oTable, tblAliasized;
            Type oType;
            LambdaExpression? oExpression;

            switch (mLazyLoad.Clausole)
            {
                case EGefyraClausole.Insert:
                    oCommandBuilder.Insert();
                    break;
                case EGefyraClausole.Select:
                    oCommandBuilder.Select();
                    break;
                case EGefyraClausole.Count:
                    oCommandBuilder.Count();
                    break;

                //case EGefyraClausole.Join:

                //    EGefyraJoin?
                //        eJoin = mLazyLoad.GetPayLoad(0) as EGefyraJoin?;

                //    if (eJoin == null)
                //        return false;

                //    oExpression = mLazyLoad.GetPayLoad(1) as LambdaExpression;

                //    if (oExpression == null)// || oExpression.Parameters.Count < 2)
                //        return false;

                //    PrepareAliasizedTablesFrom(ref oExpression);
                //    _oCommandBuilder.Join(eJoin.Value, null);// ArrayUtils.GetLastValue(aAliasizedTables) as GefyraTable);

                //    Prepare(oExpression, oExpression.Body, out oObject);
                //    break;

                case EGefyraClausole.Delete:
                    oCommandBuilder.Delete();
                    break;
                case EGefyraClausole.And:
                    oCommandBuilder.And();
                    break;
                case EGefyraClausole.Or:
                    oCommandBuilder.Or();
                    break;
                case EGefyraClausole.OpenBlock:
                    oCommandBuilder.OpenBlock();
                    break;
                case EGefyraClausole.CloseBlock:
                    oCommandBuilder.CloseBlock();
                    break;
                case EGefyraClausole.From:
                    GefyraMapper.GetTable(ref __tModel, out oTable);
                    //GetAliasizedTableFrom(ref __tModel, 0, out tblAliasized);
                    oCommandBuilder.From(oTable);
                    break;

                case EGefyraClausole.Limit:
                case EGefyraClausole.Offset:
                    Int32? oInteger = mLazyLoad.GetPayLoad(0) as Int32?;

                    if (oInteger == null)
                        break;

                    if (mLazyLoad.Clausole == EGefyraClausole.Limit)
                        oCommandBuilder.Limit(oInteger.Value);
                    else
                        oCommandBuilder.Offset(oInteger.Value);
                    break;

                case EGefyraClausole.Where:
                    oCommandBuilder.Where();
                    oLLPayLoad0 = mLazyLoad.GetPayLoad(0);
                    PrepareLambdaExpression(ref oLLPayLoad0, out oExpression);
                    PrepareCommandBuilder(ref oCommandBuilder, ref oExpression);
                    break;

                case EGefyraClausole.Into | EGefyraClausole.Values:
                    if (!GefyraMapper.GetTable(ref __tModel, out oTable))
                        break;

                    GefyraColumn[]
                        aTColumns = oTable.GetColumns();

                    if (aTColumns.Length < 1)
                        break;

                    oLLPayLoad0 =
                        mLazyLoad.GetPayLoad(0);

                    ModelType? 
                        oModel;

                    if (!PrepareModel(ref oLLPayLoad0, out oModel))
                        break;

                    List<GefyraColumn> lColumns = new List<GefyraColumn>(aTColumns.Length);
                    List<Object> lValues = new List<Object>(aTColumns.Length);

                    for (int i = 0; i < aTColumns.Length; i++)
                    {
                        if (aTColumns[i].DBInformationSchema == null)
                            continue;

                        Object?
                            oMValue = MemberUtils.GetValue(oModel, aTColumns[i].Member);

                        //if (aTColumns[i].DBInformationSchema.Type != EDBColumnType.Json)
                        //{
                        //    ICollection cMValue = ObjectUtils.AsCollection(oMValue);
                        //    if (cMValue != null)
                        //        oMValue = JSONUtils.Serialize(cMValue);
                        //}

                       // oMValue = aTColumns[i].PrepareValue(EGefyraPreparation.Member2DataBase, oMValue);

                        if (!aTColumns[i].DBInformationSchema.IsRequired)
                        {
                            if (
                                Object.Equals(aTColumns[i].DBInformationSchema.DefaultValue, oMValue)
                                ||
                                (
                                    aTColumns[i].DBInformationSchema.Extras.HasFlag(EDBColumnExtra.AutoIncrement)
                                    && Object.Equals(ObjectUtils.ChangeType(0, aTColumns[i].DBInformationSchema.ValueType), oMValue)
                                )
                            )
                                continue;
                        }

                        lColumns.Add(aTColumns[i]);
                        lValues.Add(oMValue);
                    }

                    GefyraColumn[]? aColumns2Use1;
                    GefyraColumn? oColumn2Use0 = ArrayUtils.Shift(lColumns, out aColumns2Use1);
                    Object[]? aValues2Use1;
                    Object? oValues2Use0 = ArrayUtils.Shift(lValues, out aValues2Use1);

                    oCommandBuilder
                        .Into(oTable, oColumn2Use0, aColumns2Use1)
                        .Values(oValues2Use0, aValues2Use1);

                    break;
            }
        }

        private void PrepareCommandBuilder
        (
            ref NTRGefyraCommandBuilder oCommandBuilder,
            ref LambdaExpression expLambda
        )
        {
            if (expLambda == null) return;
            Object? oObject;
            PrepareCommandBuilder(ref oCommandBuilder, expLambda, expLambda.Body, out oObject);
        }

        private void PrepareCommandBuilder
        (
            ref NTRGefyraCommandBuilder oCommandBuilder,
            Expression? expPrevious,
            Expression? expCurrent,
            out Object? oObject
        )
        {
            if (expCurrent == null)
            {
                oObject = null;
                return;
            }   
            
            switch (expCurrent.NodeType)
            {
                case ExpressionType.Not:
                    PrepareCommandBuilder(ref oCommandBuilder, expPrevious, expCurrent as UnaryExpression, out oObject);
                    break;
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                    PrepareCommandBuilder(ref oCommandBuilder, expPrevious, expCurrent as BinaryExpression, out oObject);
                    break;
                case ExpressionType.Call:
                    PrepareCommandBuilder(ref oCommandBuilder, expPrevious, expCurrent as MethodCallExpression, out oObject);
                    break;
                case ExpressionType.MemberAccess:
                    PrepareCommandBuilder(ref oCommandBuilder, expPrevious, expCurrent as MemberExpression, out oObject);
                    break;
                case ExpressionType.Constant:
                    PrepareCommandBuilder(expCurrent as ConstantExpression, out oObject);
                    break;
                case ExpressionType.Parameter:
                    PrepareCommandBuilder(ref expPrevious, expCurrent as ParameterExpression, out oObject);
                    break;
                case ExpressionType.ListInit:
                case ExpressionType.NewArrayBounds:
                case ExpressionType.NewArrayInit:
                    LambdaExpression? expLambda;
                    PrepareLambdaExpression(expCurrent, out expLambda);
                    PrepareCommandBuilder(expLambda, out oObject);
                    break;
                case ExpressionType.Lambda:
                    PrepareCommandBuilder(expCurrent as LambdaExpression, out oObject);
                    break;
                default:
                    oObject = null;
                    break;
            }
        }

        private void PrepareLambdaExpression(
            Expression? oExpression,
            out LambdaExpression? expLambda
        )
        {
            if (oExpression == null)
            {
                expLambda = null;
                return;
            }

            Type? tEFunction;
            if (!Expression.TryGetFuncType(new Type[] { oExpression.Type }, out tEFunction))
            {
                expLambda = null;
                return;
            }

            try
            {
                expLambda = Expression.Lambda(tEFunction, oExpression);
            }
            catch
            {
                expLambda = null;
            }
        }

        private void PrepareCommandBuilder
        (
            ref NTRGefyraCommandBuilder oCommandBuilder,
            Expression? expPrevious,
            UnaryExpression? expUnary,
            out Object? oObject
        )
        {
            if (expUnary == null)
            {
                oObject = null;
                return;
            }

            PrepareCommandBuilder(ref oCommandBuilder, expUnary, expUnary.Operand, out oObject);
        }

        private void PrepareCommandBuilder
        (
            ref NTRGefyraCommandBuilder oCommandBuilder, 
            Expression? expPrevious,
            BinaryExpression? expBinary,
            out Object? oObject
        )
        {
            if (expBinary == null)
            {
                oObject = null;
                return;
            }

            EGefyraComparator? eInnerComparator = GefyraComparatorUtils.From(expBinary.NodeType);
            EGefyraClausole? eInnerClausole = GefyraClausoleUtils.From(expBinary.NodeType);

            if (eInnerClausole == null && eInnerComparator == null)
            {
                oObject = null;
                return;
            }

            Object? oLeft, oRight;

            oCommandBuilder.OpenBlock();

            PrepareCommandBuilder(ref oCommandBuilder, expBinary, expBinary.Left, out oLeft);

            switch (eInnerClausole)
            {
                case EGefyraClausole.And:
                    oCommandBuilder.And();
                    break;
                case EGefyraClausole.Or:
                    oCommandBuilder.Or();
                    break;
            }

            PrepareCommandBuilder(ref oCommandBuilder, expBinary, expBinary.Right, out oRight);

            if (eInnerComparator != null)
                PrepareCommandBuilder(ref oCommandBuilder, ref oLeft, eInnerComparator.Value, ref oRight);

            oCommandBuilder.CloseBlock();

            oObject = null;
        }

        private void PrepareCommandBuilder(
            ref NTRGefyraCommandBuilder oCommandBuilder,
            Expression? expPrevious,
            MethodCallExpression? expMethodCall,
            out Object? oObject
        )
        {
            if (expMethodCall == null)
            {
                oObject = null;
                return;
            }

            EGefyraMethod?
                eEMCMMethod = GefyraMethodUtils.From(expMethodCall.Method);

            if (eEMCMMethod == null)
            {
                oObject = null;
                return;
            }
            else if (
                expPrevious != null
                && expPrevious.NodeType == ExpressionType.Not
            )
                switch (eEMCMMethod.Value)
                {
                    case EGefyraMethod.Equals:
                        eEMCMMethod = EGefyraMethod.NotEquals;
                        break;
                    case EGefyraMethod.NotEquals:
                        eEMCMMethod = EGefyraMethod.Equals;
                        break;
                    case EGefyraMethod.Contains:
                        eEMCMMethod = EGefyraMethod.NotContains;
                        break;
                    case EGefyraMethod.NotContains:
                        eEMCMMethod = EGefyraMethod.Contains;
                        break;
                }

            List<Expression>
                lEMCArguments = new List<Expression>();

            if (expMethodCall.Object != null)
                lEMCArguments.Add(expMethodCall.Object);

            if (expMethodCall.Arguments != null)
            {
                for (int i = 0; i < expMethodCall.Arguments.Count; i++)
                    lEMCArguments.Add(expMethodCall.Arguments[i]);
            }

            if (lEMCArguments.Count < 1 || lEMCArguments.Count > 2)
            {
                oObject = null;
                return;
            }

            Object? o0;
            PrepareCommandBuilder(ref oCommandBuilder, expMethodCall, lEMCArguments[0], out o0);

            if (lEMCArguments.Count < 2)
            {
                oObject = o0;
                return;
            }

            Object? o1;
            PrepareCommandBuilder(ref oCommandBuilder, expMethodCall, lEMCArguments[1], out o1);

            switch (eEMCMMethod.Value)
            {
                case EGefyraMethod.Equals:
                    PrepareCommandBuilder(ref oCommandBuilder, ref o0, EGefyraComparator.Equal, ref o1);
                    break;
                case EGefyraMethod.NotEquals:
                    PrepareCommandBuilder(ref oCommandBuilder, ref o0, EGefyraComparator.NotEqual, ref o1);
                    break;
                case EGefyraMethod.Contains:
                    PrepareCommandBuilder(ref oCommandBuilder, ref o0, EGefyraComparator.Like, ref o1);
                    break;
                case EGefyraMethod.NotContains:
                    PrepareCommandBuilder(ref oCommandBuilder, ref o0, EGefyraComparator.NotLike, ref o1);
                    break;
            }

            oObject = null;
        }

        private void PrepareCommandBuilder(
            ref NTRGefyraCommandBuilder oCommandBuilder,
            Expression? expPrevious,
            MemberExpression? expMember,
            out Object? oObject
        )
        {
            if (expMember == null)
            {
                oObject = null;
                return;
            }

            PrepareCommandBuilder(ref oCommandBuilder, expMember, expMember.Expression, out oObject);
            GefyraColumn? oColumn = oObject as GefyraColumn;

            if (oColumn == null || expPrevious == null)
            {
                oObject = MemberUtils.GetValue(oObject, expMember.Member);
                return;
            }

            MethodCallExpression?
                expMethodCall = expPrevious as MethodCallExpression;
            BinaryExpression?
               expBinary = expPrevious as BinaryExpression;

            if (expMethodCall != null || expBinary != null)
            {
                EGefyraClausole? eInnerClausole = GefyraClausoleUtils.From(expPrevious.NodeType);
                if (eInnerClausole == null)
                    return;
            }

            Type?
                tMember = MemberUtils.GetValueType(oColumn.Member);

            if (tMember != CType.Boolean)
                return;

            EGefyraComparator?
                eComparator = GefyraComparatorUtils.From(expPrevious.NodeType);

            Object
                oValue = eComparator == null || eComparator.Value == EGefyraComparator.Equal;

            PrepareCommandBuilder(ref oCommandBuilder, ref oObject, EGefyraComparator.Equal, ref oValue);
        }

        private void PrepareCommandBuilder(
            ConstantExpression expConstant,
            out Object oObject
        )
        {
            if (expConstant == null)
            {
                oObject = null;
                return;
            }

            oObject = expConstant.Value;
        }

        private void PrepareCommandBuilder(
            ref Expression expPrevious,
            ParameterExpression expParameter,
            out Object oObject
        )
        {
            if (expParameter == null)
            {
                oObject = null;
                return;
            }

            MemberExpression expMember = expPrevious as MemberExpression;

            if (expMember == null)
            {
                oObject = null;
                return;
            }

            MemberInfo infEMMeber = expMember.Member;
            //if (infEMMeber == null)
            //{
            //    oObject = null;
            //    return;
            //}

            GefyraColumn oColumn;
            //GefyraMapper.GetColumn(ref infEMMeber, out oColumn);
            oColumn = null;
            oObject = oColumn;


            //if (!GefyraMapper.GetColumn(ref infEMMeber, out oColumn))
            //{
            //    oObject = null;
            //    return;
            //}

            //GefyraTable tblAliasized;
            //if (!GetAliasizedTableFrom(ref expParameter, out tblAliasized))
            //{
            //    oObject = null;
            //    return;
            //}

            //oObject = tblAliasized.ColumnOf(oColumn.Name);
        }

        private void PrepareCommandBuilder(
           LambdaExpression expLambda,
           out Object oObject
       )
        {
            if (expLambda == null)
            {
                oObject = null;
                return;
            }

            try
            {
                oObject = expLambda.Compile().DynamicInvoke();
            }
            catch
            {
                oObject = null;
            }
        }

        private void PrepareCommandBuilder(
            ref NTRGefyraCommandBuilder oCommandBuilder,
            ref Object? o0,
            EGefyraComparator eComparator,
            ref Object? o1
        )
        {
            GefyraColumn? oColumn, clmKey;
            Object? oValue;
            Int32 iColumnsNumber = 0;

            oColumn = o0 as GefyraColumn;
            if (oColumn != null)
            {
                iColumnsNumber++;
                clmKey = oColumn;
            }
            else
                clmKey = null;

            oValue = o1;

            oColumn = o1 as GefyraColumn;
            if (oColumn != null)
            {
                iColumnsNumber++;

                if (clmKey == null)
                {
                    clmKey = oColumn;
                    oValue = o0;
                }
            }

            if (iColumnsNumber < 1)
                return;

            oCommandBuilder.Where(clmKey, eComparator, oValue);

            //switch (e)
            //{
            //    case EGefyraClausole.Where:
            //        oCommandBuilder.Where(clmKey, eComparator, iColumnsNumber > 1 ? oValue : clmKey.DBInformationSchema.PrepareValue(oValue));
            //        break;
            //    case EGefyraClausole.Join:

            //        break;
            //}
        }

        #endregion

        #region Alias

        private Boolean GetAliasizedTableFrom(ref ParameterExpression expParameter, out GefyraTable tblAliasized)
        {
            //DictionaryUtils.TryGetValue(_dEParameters2AliasizedTables, expParameter, out tblAliasized);
            tblAliasized = null;
            return tblAliasized != null;
        }

        private void PrepareAliasizedTablesFrom(ref LambdaExpression expLambda)
        {
            if (expLambda.Parameters.Count < 1)
                return;

            GefyraTable oTable, tblAliasized;
            Type tLastELParameteri = null;

            for (int i = 0; i < expLambda.Parameters.Count; i++)
            {
                if (expLambda.Parameters[i] == null) return;
                Type tELParameteri = expLambda.Parameters[i].Type;
                if (!GefyraMapper.GetTable(ref tELParameteri, out oTable)) return;
                GetAliasizedTableFrom(ref oTable, tLastELParameteri == tELParameteri ? 1 : 0, out tblAliasized);
                _dEParameters2AliasizedTables[expLambda.Parameters[i]] = tblAliasized;
                tLastELParameteri = tELParameteri;
            }
        }

        private void GetAliasizedTableFrom(ref Type oType, Int32 iAliasLevel, out GefyraTable tblAliasized)
        {
            GefyraTable oTable;
            GefyraMapper.GetTable(ref oType, out oTable);
            GetAliasizedTableFrom(ref oTable, iAliasLevel, out tblAliasized);
        }

        private void GetAliasizedTableFrom(ref GefyraTable oTable, Int32 iAliasLevel, out GefyraTable tblAliasized)
        {
            Dictionary<Int32, GefyraTable> dAliasLevels2AliasizedTables = null;
            EDictionaryTryGetValueResult eResult = EDictionaryTryGetValueResult.NullKey;// DictionaryUtils.TryGetValue(_dTables2AliasLevels2AliasizedTables, oTable, out dAliasLevels2AliasizedTables);

            if(eResult == EDictionaryTryGetValueResult.NullKey)
            {
                tblAliasized = null;
                return;
            }    

            if (dAliasLevels2AliasizedTables == null)
                _dTables2AliasLevels2AliasizedTables[oTable] = dAliasLevels2AliasizedTables = new Dictionary<Int32, GefyraTable>();

            //DictionaryUtils.TryGetValue(dAliasLevels2AliasizedTables, iAliasLevel, out tblAliasized);
            tblAliasized = null;
            if (tblAliasized == null)
            {
                dAliasLevels2AliasizedTables[iAliasLevel] = tblAliasized = oTable.As(__sTableAliasPrefix + _iAliasLevel);
                _iAliasLevel++;
            }
        }


        private void GetAliasizedColumn(ref GefyraColumn oColumn, out GefyraColumn clmAliasized)
        {
            GefyraTable oTable = oColumn.Table, tblAliasized;
            //GetAliasizedTable(ref oTable, out tblAliasized);
            //clmAliasized = tblAliasized.ColumnOf(oColumn.Name);
            clmAliasized = null;
        }

        #endregion

        #region FetchInformationSchemaFromDataBase

        private Boolean FetchInformationSchemaColumnsFromDataBaseAndInjectIntoEntities(
            ref Type? oType
        )
        {
            if (oType == null)
                return false;

            lock (__oLock)
            {
                GefyraTable? oTable;
                if (!GefyraMapper.GetTable(ref oType, out oTable))
                    return false;
                else if (oTable.DBInformationSchema != null)
                    return true;
                else if(
                    _oDBController == null
                    || (!_oDBController.IsConnectionOpened() && !_oDBController.OpenConnection())
                )
                    return false;

                DBISColumnsModel mDBISColumns = _oDBController.FetchInformationSchema(EDBInformationSchemaType.Columns, oTable.SchemaName, oTable.Name) as DBISColumnsModel;
                return oTable.InjectDBInformationSchema(ref mDBISColumns);
            }
        }

        #endregion
    }

    //public class Ownr
    //{
    //    public string Name { get; set; }
    //    public int Qty { get; set; }
    //}


    //public static class Extentions
    //{
    //    public static Expression<Func<T, bool>> strToFunc<T>(MemberInfo oMemberInfo, ExpressionType eType, Object oValue, Expression<Func<T, bool>> expr = null)
    //    {
    //        Expression<Func<T, bool>> func = null;
    //        try
    //        {
    //            var type = typeof(T);
    //            ParameterExpression tpe = Expression.Parameter(typeof(T));
    //            Expression left = Expression.Property(tpe, oMemberInfo.Name);
    //            Expression right = Expression.Convert(oValue);
    //            Expression<Func<T, bool>> innerExpr = Expression.Lambda<Func<T, bool>>(ApplyFilter(opr, left, right), tpe);
    //            if (expr != null)
    //                innerExpr = innerExpr.And(expr);
    //            func = innerExpr;
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        return func;
    //    }
    //    private static Expression ToExprConstant(PropertyInfo prop, string value)
    //    {
    //        object val = null;

    //        try
    //        {
    //            switch (prop.Name)
    //            {
    //                case "System.Guid":
    //                    val = Guid.NewGuid();
    //                    break;
    //                default:
    //                    {
    //                        val = Convert.ChangeType(value, prop.PropertyType);
    //                        break;
    //                    }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }

    //        return Expression.Constant(val);
    //    }
    //    private static BinaryExpression ApplyFilter(string opr, Expression left, Expression right)
    //    {
    //        BinaryExpression InnerLambda = null;
    //        switch (opr)
    //        {
    //            case "==":
    //            case "=":
    //                InnerLambda = Expression.Equal(left, right);
    //                break;
    //            case "<":
    //                InnerLambda = Expression.LessThan(left, right);
    //                break;
    //            case ">":
    //                InnerLambda = Expression.GreaterThan(left, right);
    //                break;
    //            case ">=":
    //                InnerLambda = Expression.GreaterThanOrEqual(left, right);
    //                break;
    //            case "<=":
    //                InnerLambda = Expression.LessThanOrEqual(left, right);
    //                break;
    //            case "!=":
    //                InnerLambda = Expression.NotEqual(left, right);
    //                break;
    //            case "&&":
    //                InnerLambda = Expression.And(left, right);
    //                break;
    //            case "||":
    //                InnerLambda = Expression.Or(left, right);
    //                break;
    //        }
    //        return InnerLambda;
    //    }

    //    public static Expression<Func<T, TResult>> And<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
    //    {
    //        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
    //        return Expression.Lambda<Func<T, TResult>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    //    }

    //    public static Func<T, TResult> ExpressionToFunc<T, TResult>(this Expression<Func<T, TResult>> expr)
    //    {
    //        var res = expr.Compile();
    //        return res;
    //    }
    //}

    //public static class GefyraContext
    //{
    //    public static IGefyraContext<ModelType> New<ModelType>(IDBController oDBController)
    //    {
    //        return new GefyraContext<ModelType, Object>(oDBController);
    //    }
    //}
}