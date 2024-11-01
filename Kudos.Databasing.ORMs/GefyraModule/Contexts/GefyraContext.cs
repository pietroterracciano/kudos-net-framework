using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Kudos.Constants;
using Kudos.Databasing.Descriptors;
using Kudos.Databasing.Interfaces;
using Kudos.Databasing.ORMs.GefyraModule.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Enums;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Builders;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Contexts;
using Kudos.Databasing.ORMs.GefyraModule.Interfaces.Entities;
using Kudos.Databasing.ORMs.GefyraModule.Types;
using Kudos.Databasing.Results;
using Kudos.Reflection.Utils;
using Kudos.Utils;
using Kudos.Utils.Collections;

namespace Kudos.Databasing.ORMs.GefyraModule.Contexts
{
	public sealed class
		GefyraContext<T>
	:
        IGefyraContext<T>,
        IGefyraExecuteContext<T>
    {
        private static EGefyraClausole
            __egcInsert;

        static GefyraContext()
        {
            __egcInsert = EGefyraClausole.Insert;
        }

		private readonly IDatabaseHandler? _dh;
		private readonly Boolean _bHasDatabaseHandler;
        private readonly List<GefyraLazyLoad> _l;

		internal GefyraContext(ref IDatabaseHandler? dh)
		{
            _bHasDatabaseHandler = ( _dh = dh ) != null;
            _l = new List<GefyraLazyLoad>();
        }

        #region IGefyraInsertContext<T>

        public IGefyraExecuteContext<T> Insert(T? o)
        {
            lock (_l) { _l.Add(new GefyraLazyLoad(ref __egcInsert, o)); }
            return this;
        }

        public IGefyraExecuteContext<T> Insert(Action<T?> o)
        {
            return this;
        }

        #endregion

        #region IGefyraExecuteContext<T>

        public async Task<T?> ExecuteAsync()
        {
            if(!_bHasDatabaseHandler)
                return default(T);

            GefyraLazyLoad[] gll;

            lock(_l)
            {
                if (_l.Count < 1)
                    return default(T);

                gll = new GefyraLazyLoad[_l.Count];
                EGefyraClausole egci;

                for (int i = 0; i < _l.Count; i++)
                {
                    egci = _l[i].Clausole;
                    gll[i] = _l[i];
                }

                _l.Clear();
            }

            GefyraBuilder
                gb = Gefyra.RequestBuilder() as GefyraBuilder;

            for (int i = 0; i < gll.Length; i++)
            {
                _PrepareBuilder(ref gb, ref gll[i]);
            }

            //_gb.Insert();

            IGefyraTable
                gt = Gefyra.RequestTable<T>();

            if
            (
                gt.IsInvalid
                || gt.IsIgnored
            )
                return default(T);

            MemberInfo[]?
                mia = ReflectionUtils.GetMembers<T>(CBindingFlags.Instance);

            if (mia == null)
                return default(T);

            IGefyraColumn gci;
            DatabaseColumnDescriptor? dcdi;
            for (int i = 0; i < mia.Length; i++)
            {
                gci = gt.RequestColumn(mia[i].Name);

                if
                (
                    gci.IsIgnored
                    || gci.IsInvalid
                )
                    continue;

                dcdi = await _dh.GetColumnDescriptorAsync(gt.SchemaName, gt.Name, gci.Name);
            }

            return default(T);
        }

        #endregion

        #region private void _PrepareBuilder(...)

        private void _PrepareBuilder
        (
            ref GefyraBuilder gb,
            ref GefyraLazyLoad gll
        )
        {
            //switch (gll.Clausole)
            //{
            //    case EGefyraClausole.Insert:
            //        gb.Insert();

            //        GefyraColumn[]
            //            aTColumns = oTable.GetColumns();

            //        if (aTColumns.Length < 1)
            //            break;

            //        oLLPayLoad0 =
            //            mLazyLoad.GetPayLoad(0);

            //        ModelType?
            //            oModel;

            //        if (!PrepareModel(ref oLLPayLoad0, out oModel))
            //            break;

            //        List<GefyraColumn> lColumns = new List<GefyraColumn>(aTColumns.Length);
            //        List<Object> lValues = new List<Object>(aTColumns.Length);

            //        for (int i = 0; i < aTColumns.Length; i++)
            //        {
            //            if (aTColumns[i].DBInformationSchema == null)
            //                continue;

            //            Object?
            //                oMValue = MemberUtils.GetValue(oModel, aTColumns[i].Member);

            //            //if (aTColumns[i].DBInformationSchema.Type != EDBColumnType.Json)
            //            //{
            //            //    ICollection cMValue = ObjectUtils.AsCollection(oMValue);
            //            //    if (cMValue != null)
            //            //        oMValue = JSONUtils.Serialize(cMValue);
            //            //}

            //            // oMValue = aTColumns[i].PrepareValue(EGefyraPreparation.Member2DataBase, oMValue);

            //            if (!aTColumns[i].DBInformationSchema.IsRequired)
            //            {
            //                if (
            //                    Object.Equals(aTColumns[i].DBInformationSchema.DefaultValue, oMValue)
            //                    ||
            //                    (
            //                        aTColumns[i].DBInformationSchema.Extras.HasFlag(EDBColumnExtra.AutoIncrement)
            //                        && Object.Equals(ObjectUtils.ChangeType(0, aTColumns[i].DBInformationSchema.ValueType), oMValue)
            //                    )
            //                )
            //                    continue;
            //            }

            //            lColumns.Add(aTColumns[i]);
            //            lValues.Add(oMValue);
            //        }

            //        GefyraColumn[]? aColumns2Use1;
            //        GefyraColumn? oColumn2Use0 = ArrayUtils.Shift(lColumns, out aColumns2Use1);
            //        Object[]? aValues2Use1;
            //        Object? oValues2Use0 = ArrayUtils.Shift(lValues, out aValues2Use1);

            //        oCommandBuilder
            //            .Into(oTable, oColumn2Use0, aColumns2Use1)
            //            .Values(oValues2Use0, aValues2Use1);

            //        break;
            //}
        }

        #endregion

        #region private void _

        private async Task provaAsync(Type? t)
        {
            if (!_bHasDatabaseHandler || t == null)
                return;

            GefyraTable gt;
            GefyraTable.Request(ref t, out gt);

            if
            (
                gt.IsIgnored
                || gt.IsInvalid
                || gt.Descriptor.HasDatabaseDescriptor
            )
                return;

            gt.Descriptor.DatabaseDescriptor =
                await
                    _dh.GetTableDescriptorAsync
                    (
                        gt.SchemaName,
                        gt.Name
                    );

            MemberInfo[]?
                mia = ReflectionUtils.GetMembers<T>(CBindingFlags.Instance);

            if (mia == null)
                return;

            //IGefyraColumn gci;
            //DatabaseColumnDescriptor? dcdi;
            //for (int i = 0; i < mia.Length; i++)
            //{
            //    gci = GefyraTable.RequestColumn(ref gt, mia[i].Name);

            //    if
            //    (
            //        gci.IsIgnored
            //        || gci.IsInvalid
            //    )
            //        continue;

            //    dcdi = await _dh.GetColumnDescriptorAsync(gt.SchemaName, gt.Name, gci.Name);
            //}
        }

        #endregion
    }
}

