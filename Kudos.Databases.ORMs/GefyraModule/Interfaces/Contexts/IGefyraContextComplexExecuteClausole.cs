using Kudos.Databases.Models.Results;
using Kudos.Databases.ORMs.GefyraModule.Builders;
using Kudos.Databases.ORMs.GefyraModule.Interfaces.Commands.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Databases.ORMs.GefyraModule.Interfaces.Contexts
{
    public interface IGefyraContextComplexExecuteClausole<ModelType>
    {
        //Boolean ExecuteNonQuery(IGefyraCommandBuilder oCommandBuilder);
        //Boolean ExecuteNonQuery(String sCommandText, params KeyValuePair<String, Object>[] aParameters);

        //ModelType[] ExecuteQuery(IGefyraCommandBuilder oCommandBuilder);
        //ModelType[] ExecuteQuery(String sCommandText, params KeyValuePair<String, Object>[] aParameters);
    }
}
