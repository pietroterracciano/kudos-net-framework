using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Reflection.Types
{
    public class Instruction
    {
        public readonly Object? Info;
        public readonly OpCode KeyOpCode;
        public readonly OpCode[] ValueOpCodes;

        internal Instruction(ref Object? o, ref OpCode? oc, ref List<OpCode> l)
        {
            Info = o;
            KeyOpCode = oc.Value;
            ValueOpCodes = l.ToArray();
        }
    }
}