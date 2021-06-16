using System;
using System.Collections.Generic;
using System.Text;

namespace Kudos.DataBases.Models
{
    public class DBErrorModel
    {
        public Int32 ID
        {
            get;
            private set;
        }
        public String Message
        {
            get;
            private set;
        }

        public DBErrorModel(Int32 i32ID, String sMessage)
        {
            ID = i32ID;
            Message = sMessage != null ? sMessage : "";
        }
    }
}