using Kudos.Databases.Enums;
using Kudos.Databases.Enums.Columns;
using Kudos.Databases.Filters;
using Kudos.Utils;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.Models.Schemas.Columns
{
    public class DatabaseInformationSchemaColumnsModel : ADatabaseInformationSchemaModel
    {
        public readonly String SchemaName, TableName;

        private DatabaseInformationSchemaColumnsModel _oThis;
        private readonly Object _oLock;
        private readonly List<DatabaseInformationSchemaColumnDescriptorModel> _lDescriptors;
        private readonly Dictionary<Int32, DatabaseInformationSchemaColumnDescriptorModel[]> _dFHashCodes2Descriptors;

        internal DatabaseInformationSchemaColumnsModel(String? sSchemaName, String? sTableName) : base(E.Columns)
        {
            SchemaName = StringUtils.Parse2NotNullableFrom(sSchemaName);
            TableName = StringUtils.Parse2NotNullableFrom(sTableName);
            _oLock = new Object();
            _oThis = this;
            _lDescriptors = new List<DatabaseInformationSchemaColumnDescriptorModel>();
            _dFHashCodes2Descriptors = new Dictionary<Int32, DatabaseInformationSchemaColumnDescriptorModel[]>();
        }

        public DatabaseInformationSchemaColumnDescriptorModel[] GetDescriptors()
        {
            return GetDescriptors(null);
        }

        public DatabaseInformationSchemaColumnDescriptorModel[] GetDescriptors(Action<DBColumnFilter>? oAction)
        {
            lock (_oLock)
            {
                DBColumnFilter oFilter = new DBColumnFilter();

                if (oAction != null)
                    oAction.Invoke(oFilter);

                Int32 iFHashCode = oFilter.GetHashCode();

                DatabaseInformationSchemaColumnDescriptorModel[] aDescriptors;
                if (_dFHashCodes2Descriptors.TryGetValue(iFHashCode, out aDescriptors) && aDescriptors != null) 
                    return aDescriptors;

                List<DatabaseInformationSchemaColumnDescriptorModel> lDescriptors = new List<DatabaseInformationSchemaColumnDescriptorModel>(_lDescriptors.Count);

                for(int i=0; i< _lDescriptors.Count; i++)
                {
                    if(
                        (
                            oFilter.IsNullable != null 
                            && !oFilter.IsNullable.Equals(_lDescriptors[i].IsNullable)
                        )
                        || 
                        (
                            oFilter.Types != null 
                            && !oFilter.Types.Value.HasFlag(_lDescriptors[i].Type)
                        )
                        || 
                        (
                            oFilter.Keys != null
                            && !oFilter.Keys.Value.HasFlag(_lDescriptors[i].Key)
                        )
                        || 
                        (
                            oFilter.Extras != null
                            && !oFilter.Extras.Value.HasFlag(_lDescriptors[i].Extras)
                            && !_lDescriptors[i].Extras.HasFlag(oFilter.Extras.Value)
                        )
                    )
                        continue;

                    lDescriptors.Add(_lDescriptors[i]);
                }

                return aDescriptors = _dFHashCodes2Descriptors[iFHashCode] = lDescriptors.ToArray();
            }
        }

        public Boolean HasDescriptors()
        {
            return _lDescriptors.Count > 0;
        }

        internal void NewDescriptorFrom(DataRow? oDataRow)
        {
            DatabaseInformationSchemaColumnDescriptorModel mDescriptor = DatabaseInformationSchemaColumnDescriptorModel.New(ref _oThis, ref oDataRow);
            if (mDescriptor == null) return;

            _lDescriptors.Add(mDescriptor);

            lock (_oLock)
            {
                _dFHashCodes2Descriptors.Clear();
            }
        }
    }
}