using System.Reflection.Emit;
using System.Reflection;
using Kudos.Utils;
using Kudos.Reflection.DumpModule.Models;
using Kudos.Reflection.DumpModule.Filters;
using System;
using Kudos.Utils.Members;
using Kudos.Utils.Numerics.Integers;

namespace Kudos.Reflection.DumpModule.Controllers
{
    public static class DumpController
    {
        private static readonly Type
            __tOpCodes = typeof(OpCodes);

        private static readonly Dictionary<short, OpCode>
            __dOCValues2OCodes;

        static DumpController()
        {
            FieldInfo[]? aOCFields = FieldUtils.Get(__tOpCodes);

            if (aOCFields == null) return;

            __dOCValues2OCodes = new Dictionary<short, OpCode>(aOCFields.Length);

            for (int i = 0; i < aOCFields.Length; i++)
            {
                OpCode? oOpCodei = null;// ObjectUtils.Cast<OpCode?>(ObjectUtils.GetFieldValue(aOCFields[i]));
                if (oOpCodei == null) continue;
                __dOCValues2OCodes[oOpCodei.Value.Value] = oOpCodei.Value;
            }
        }

        //https://stackoverflow.com/questions/5667816/get-types-used-inside-a-c-sharp-method-body
        /// <returns>[AllowNull]</returns>
        public static DInstructionModel[]? FetchInstructions(MethodInfo? infMethod)
        {
            if (infMethod == null) return null;

            MethodBody bdyMethod = infMethod.GetMethodBody();

            if (bdyMethod == null) return null;

            Module oMModule = infMethod.Module;

            if (oMModule == null) return null;

            byte[] aMMBILA = bdyMethod.GetILAsByteArray();

            if (aMMBILA == null) return null;

            IEnumerator<byte> enmMMBILA = ((IEnumerable<byte>)aMMBILA).GetEnumerator();

            long lOperand;
            int iByteCount = 4;

            Object oIValue;
            List<OpCode> lIOpCodes = new List<OpCode>();

            List<DInstructionModel> lInstructions = new List<DInstructionModel>(aMMBILA.Length);

            while (enmMMBILA.MoveNext())
            {
                lIOpCodes.Clear();

                OpCode oOpCode;

                if (!__dOCValues2OCodes.TryGetValue(enmMMBILA.Current, out oOpCode)) continue;

                lIOpCodes.Add(oOpCode);

                OperandType eOCOperandType = oOpCode.OperandType;

                if (eOCOperandType != OperandType.InlineNone)
                {
                    switch (eOCOperandType)
                    {
                        case OperandType.InlineMethod:
                        case OperandType.InlineField:
                        case OperandType.InlineSig:
                        case OperandType.InlineString:
                        case OperandType.InlineType:
                            break;
                        default:
                            oIValue = null;
                            lInstructions.Add(new DInstructionModel(ref oIValue, ref eOCOperandType, ref lIOpCodes));
                            continue;
                    }

                    lOperand = 0;

                    for (int j = 0; j < iByteCount; j++)
                    {
                        try { enmMMBILA.MoveNext(); } catch { goto END_METHOD; }
                        if (!__dOCValues2OCodes.TryGetValue(enmMMBILA.Current, out oOpCode)) continue;
                        lIOpCodes.Add(oOpCode);
                        lOperand |= (long)oOpCode.Value << 8 * j;
                    }

                    switch (eOCOperandType)
                    {
                        case OperandType.InlineMethod:
                            MethodBase mthResolved;
                            if (!TryResolveMethod(ref oMModule, ref lOperand, out mthResolved)) continue;
                            oIValue = mthResolved;
                            break;
                        case OperandType.InlineField:
                            FieldInfo fiResolved;
                            if (!TryResolveField(ref oMModule, ref lOperand, out fiResolved)) continue;
                            oIValue = fiResolved;
                            break;
                        case OperandType.InlineSig:
                            byte[] baResolved;
                            if (!TryResolveSignature(ref oMModule, ref lOperand, out baResolved)) continue;
                            oIValue = baResolved;
                            break;
                        case OperandType.InlineString:
                            String sResolved;
                            if (!TryResolveString(ref oMModule, ref lOperand, out sResolved)) continue;
                            oIValue = sResolved;
                            break;
                        case OperandType.InlineType:
                            Type tResolved;
                            if (!TryResolveType(ref oMModule, ref lOperand, out tResolved)) continue;
                            oIValue = tResolved;
                            break;
                        default:
                            oIValue = null;
                            break;
                    }

                    lInstructions.Add(new DInstructionModel(ref oIValue, ref eOCOperandType, ref lIOpCodes));
                }
                else
                {
                    oIValue = null;
                    lInstructions.Add(new DInstructionModel(ref oIValue, ref eOCOperandType, ref lIOpCodes));
                }
            }

            END_METHOD:

            return lInstructions.ToArray();
        }

        /// <returns>[AllowNull]</returns>
        public static MemberInfo[]? FetchMembers(MethodInfo? infMethod, Action<DMemberInfoFilter>? oAction = null)
        {
            DInstructionModel[]? aInstructions = FetchInstructions(infMethod);
            if (aInstructions == null || aInstructions.Length < 1) return null;

            DMemberInfoFilter oFilter = new DMemberInfoFilter();

            if (oAction != null)
                oAction.Invoke(oFilter);
            
            List<MemberInfo> oList = new List<MemberInfo>(aInstructions.Length);

            for (int i = 0; i < aInstructions.Length; i++)
            {
                MemberInfo infMember;

                if (aInstructions[i].OperandType == OperandType.InlineField)
                    infMember = (MemberInfo)aInstructions[i].Value;
                else if (aInstructions[i].OperandType == OperandType.InlineMethod)
                    infMember = (MethodInfo)aInstructions[i].Value;
                else
                    infMember = null;

                if (
                    infMember == null
                    ||
                    (
                        oFilter.Types != null
                        && !oFilter.Types.HasFlag(infMember.MemberType)
                        && !infMember.MemberType.HasFlag(oFilter.Types)
                    )
                )
                    continue;

                oList.Add(infMember);
            }

            return oList.ToArray();
        }

        private static bool TryResolveField(ref Module oModule, ref long lOperand, out FieldInfo oFieldInfo)
        {
            try { oFieldInfo = oModule.ResolveField(Int32Utils.From(lOperand)); return oFieldInfo != null; }
            catch { oFieldInfo = null; }
            return false;
        }

        private static bool TryResolveMethod(ref Module oModule, ref long lOperand, out MethodBase oMethod)
        {
            try { oMethod = oModule.ResolveMethod(Int32Utils.From(lOperand)); return oMethod != null; }
            catch { oMethod = null; }
            return false;
        }
        private static bool TryResolveString(ref Module oModule, ref long lOperand, out string oString)
        {
            try { oString = oModule.ResolveString(Int32Utils.From(lOperand)); return oString != null; }
            catch { oString = null; }
            return false;
        }
        private static bool TryResolveSignature(ref Module oModule, ref long lOperand, out byte[] aBytes)
        {
            try { aBytes = oModule.ResolveSignature(Int32Utils.From(lOperand)); return aBytes != null; }
            catch { aBytes = null; }
            return false;
        }
        private static bool TryResolveType(ref Module oModule, ref long lOperand, out Type oType)
        {
            try { oType = oModule.ResolveType(Int32Utils.From(lOperand)); return oType != null; }
            catch { oType = null; }
            return false;
        }
    }
}