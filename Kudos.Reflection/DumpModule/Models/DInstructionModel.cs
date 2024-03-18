using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Reflection.DumpModule.Models
{
    public class DInstructionModel
    {
        public readonly Object Value;
        public readonly OperandType OperandType;
        public readonly OpCode[] OpCodes;

        internal DInstructionModel(ref Object oValue, ref OperandType eOperandType, ref List<OpCode> lOpCodes)
        {
            Value = oValue;
            OperandType = eOperandType;
            OpCodes = lOpCodes.ToArray();
        }
    }
}