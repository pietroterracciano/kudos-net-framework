using Kudos.Constants;
using Kudos.Reflection.Constants;
using Kudos.Reflection.Types;
using Kudos.Types;
using Kudos.Utils;
using Kudos.Utils.Collections;
using Kudos.Utils.Numerics;
using Kudos.Utils.Texts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Kudos.Reflection.Utils
{
    public static class ReflectionUtils
    {
        private static readonly long
            __Multiplier = 10000000000;

        // t.Module.MetadataToken -> t.MetadataToken
        private static Dictionary<int, HashSet<int>>
            __d0;
        // t.Module.MetadataToken -> t.MetadataToken -> mi.MetadataToken -> mi
        private static Dictionary<int, Dictionary<int, Dictionary<int, MemberInfo>>>
            __d1;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> mi.MetadataToken
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, HashSet<int>>>>
            __d2;
        // t.Module.MetadataToken -> t.MetadataToken -> BindingFlags -> mi.MetadataToken
        private static Dictionary<int, Dictionary<int, Dictionary<BindingFlags, HashSet<int>>>>
            __d3;
        // t.Module.MetadataToken -> t.MetadataToken -> mi.Name -> mi.MetadataToken
        private static Dictionary<int, Dictionary<int, Dictionary<string, HashSet<int>>>>
            __d4;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> mi.Name -> mi.MetadataToken
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, HashSet<int>?>>>>>>
            __d5;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> mi.Name -> mia
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, MemberInfo[]?>>>>>>
            __d6;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> pia.Name -> pia
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, PropertyInfo[]?>>>>>>
            __d7;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> fia.Name -> fia
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, FieldInfo[]?>>>>>>
            __d8;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> mi.Name -> mia
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, MethodInfo[]?>>>>>>
            __d9;
        // t.Module.MetadataToken -> t.MetadataToken -> MemberTypes -> BindingFlags -> cia.Name -> cia
        private static Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, ConstructorInfo[]?>>>>>>
            __d10;
        // t.Module.MetadataToken -> t.MetadataToken -> HashKey:mi.Parameters -> mi.MetadataToken
        private static Dictionary<int, Dictionary<int, Dictionary<long, HashSet<int>>>>
            __d11;
        // t.Module.MetadataToken -> t.MetadataToken -> mi.MetadataToken -> Boolean -> aa
        private static Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>>
            __d12;
        // oc.Value -> oc
        private static readonly Dictionary<Int16, OpCode?> 
            __d13;
        // oc.Name -> oc
        private static readonly Dictionary<String, OpCode?> 
            __d14;
        // Token -> TokenizedObject
        private static readonly Dictionary<Int32, Object>
            __d15;
        private static OpCode[]? 
            __oca;

        private static Boolean 
            __bIsOpCodesFetched;

        private static readonly object
            __lck0,
            __lck1,
            __lck2;

        #region static ReflectionUtils()

        static ReflectionUtils()
        {
            __lck0 = new object();
            __lck1 = new object();
            __lck2 = new object();
            __d0 = new Dictionary<int, HashSet<int>>();
            __d1 = new Dictionary<int, Dictionary<int, Dictionary<int, MemberInfo>>>();
            __d2 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, HashSet<int>>>>();
            __d3 = new Dictionary<int, Dictionary<int, Dictionary<BindingFlags, HashSet<int>>>>();
            __d4 = new Dictionary<int, Dictionary<int, Dictionary<string, HashSet<int>>>>();
            __d5 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, HashSet<int>?>>>>>>();
            __d6 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, MemberInfo[]?>>>>>>();
            __d7 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, PropertyInfo[]?>>>>>>();
            __d8 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, FieldInfo[]?>>>>>>();
            __d9 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, MethodInfo[]?>>>>>>();
            __d10 = new Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, ConstructorInfo[]?>>>>>>();
            __d11 = new Dictionary<int, Dictionary<int, Dictionary<long, HashSet<int>>>>();
            __d12 = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>>();
            __d13 = new Dictionary<Int16, OpCode?>();
            __d14 = new Dictionary<String, OpCode?>();
            __d15 = new Dictionary<Int32, Object>();
        }

        #endregion

        #region public static ...

        #region public static Attribute? GetCustomAttribute<...>(...)

        public static T? GetCustomAttribute<T>(MemberInfo? mi, bool bConsiderInheritance = true)
            where T : Attribute
        { return GetCustomAttribute(mi, typeof(T), bConsiderInheritance) as T; }
        public static Attribute? GetCustomAttribute(MemberInfo? mi, Type? t, bool bConsiderInheritance = true)
        {
            Attribute[]? aa = GetCustomAttributes(mi, t, bConsiderInheritance);
            return aa != null && aa.Length == 1 ? aa[0] : null;
        }

        #endregion

        #region public static Attribute? GetCustomAttributes<...>(...)

        public static T[]? GetCustomAttributes<T>(MemberInfo? mi, bool bConsiderInheritance = true)
            where T : Attribute
        { return GetCustomAttributes(mi, typeof(T), bConsiderInheritance) as T[]; }
        public static Attribute[]? GetCustomAttributes(MemberInfo? mi, Type? t, bool bConsiderInheritance = true)
        {
            if (mi == null || t == null)
                return null;

            lock (__lck1)
            {
                Attribute[]? aa;

                D12:
                if (FetchFromDictionaryIfPossible(ref t, ref mi, ref __d12, ref bConsiderInheritance, out aa))
                    return aa;

                try { aa = mi.GetCustomAttributes(t, bConsiderInheritance) as Attribute[]; } catch { }

                AddToDictionaryIfNecessary(ref t, ref mi, ref __d12, ref bConsiderInheritance, ref aa);

                goto D12;
            }
        }

        #endregion

        #region public static PropertyInfo? GetProperty<...>(...)

        public static PropertyInfo? GetProperty<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance) { return GetProperty(typeof(T), s, bf); }
        public static PropertyInfo? GetProperty(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            PropertyInfo[]? a;
            Type[]? ta = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a != null && a.Length == 1 ? a[0] : null;
        }

        #endregion

        #region public static PropertyInfo[]? GetProperties<...>(...)

        public static PropertyInfo[]? GetProperties<T>(BindingFlags bf = CBindingFlags.PublicInstance) { return GetProperties(typeof(T), bf); }
        public static PropertyInfo[]? GetProperties(Type? t, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            PropertyInfo[]? a;
            Type[]? ta = null;
            string? s = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }

        #endregion

        #region public static FieldInfo? GetField<...>(...)

        public static FieldInfo? GetField<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance) { return GetField(typeof(T), s, bf); }
        public static FieldInfo? GetField(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            FieldInfo[]? a;
            Type[]? ta = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a != null && a.Length == 1 ? a[0] : null;
        }

        #endregion

        #region public static FieldInfo[]? GetFields<...>(...)

        public static FieldInfo[]? GetFields<T>(BindingFlags bf = CBindingFlags.PublicInstance) { return GetFields(typeof(T), bf); }
        public static FieldInfo[]? GetFields(Type? t, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            FieldInfo[]? a;
            Type[]? ta = null;
            string? s = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }

        #endregion

        #region public static MethodInfo? GetMethod<...>(...)

        public static MethodInfo? GetMethod<T>(string? s, params Type[]? ta) { return GetMethod(typeof(T), s, ta); }
        public static MethodInfo? GetMethod<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta) { return GetMethod(typeof(T), s, bf, ta); }
        public static MethodInfo? GetMethod(Type? t, string? s, params Type[]? ta) { return GetMethod(t, s, CBindingFlags.PublicInstance, ta); }
        public static MethodInfo? GetMethod(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta)
        {
            MethodInfo[]? a;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a != null && a.Length == 1 ? a[0] : null;
        }

        #endregion

        #region public static MethodInfo[]? GetMethods<...>(...)

        public static MethodInfo[]? GetMethods<T>(params Type[]? ta) { return GetMethods(typeof(T), ta); }
        public static MethodInfo[]? GetMethods<T>(BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta) { return GetMethods(typeof(T), bf, ta); }
        public static MethodInfo[]? GetMethods(Type? t, params Type[]? ta) { return GetMethods(t, CBindingFlags.PublicInstance, ta); }
        public static MethodInfo[]? GetMethods(Type? t, BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta)
        {
            MethodInfo[]? a;
            string? s = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }
        public static MethodInfo[]? GetMethods<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance) { return GetMethods(typeof(T), s, bf); }
        public static MethodInfo[]? GetMethods(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            MethodInfo[]? a;
            Type[]? ta = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }

        #endregion

        #region public static MethodInfo? GetConstructor<...>(...)

        public static ConstructorInfo? GetConstructor<T>(params Type[]? ta) { return GetConstructor(typeof(T), ta); }
        public static ConstructorInfo? GetConstructor<T>(BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta) { return GetConstructor(typeof(T), bf, ta); }
        public static ConstructorInfo? GetConstructor(Type? t, params Type[]? ta) { return GetConstructor(t, CBindingFlags.PublicInstance, ta); }
        public static ConstructorInfo? GetConstructor(Type? t, BindingFlags bf = CBindingFlags.PublicInstance, params Type[]? ta)
        {
            ConstructorInfo[]? a;
            string? s = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a != null && a.Length == 1 ? a[0] : null;
        }

        #endregion

        #region public static MethodInfo[]? GetConstructors<...>(...)

        public static ConstructorInfo[]? GetConstructors<T>(BindingFlags bf = CBindingFlags.PublicInstance) { return GetConstructors(typeof(T), bf); }
        public static ConstructorInfo[]? GetConstructors(Type? t, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            ConstructorInfo[]? a;
            string? s = null;
            Type[]? ta = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }

        #endregion

        #region public static MemberInfo? GetMember(...)

        public static MemberInfo? GetMember<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance) { return GetMember(typeof(T), s, bf); }
        public static MemberInfo? GetMember(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            MemberInfo[]? a = GetMembers(t, s, bf);
            return a != null && a.Length == 1 ? a[0] : null;
        }

        #endregion

        #region public static MemberInfo[]? GetMembers(...)

        public static MemberInfo[]? GetMembers<T>(BindingFlags bf = CBindingFlags.PublicInstance) { return GetMembers(typeof(T), bf); }
        public static MemberInfo[]? GetMembers(Type? t, BindingFlags bf = CBindingFlags.PublicInstance) { return GetMembers(t, null, bf); }
        public static MemberInfo[]? GetMembers<T>(string? s, BindingFlags bf = CBindingFlags.PublicInstance) { return GetMembers(typeof(T), s, bf); }
        public static MemberInfo[]? GetMembers(Type? t, string? s, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            MemberInfo[]? a;
            Type[]? ta = null;
            __Get(ref t, ref s, ref bf, ref ta, out a);
            return a;
        }

        public static MemberInfo[]? GetMembers(Delegate? dlg, BindingFlags bf = CBindingFlags.PublicInstance) { return dlg != null ? GetMembers(dlg.Method, bf) : null; }
        public static MemberInfo[]? GetMembers(MethodBase? mb, BindingFlags bf = CBindingFlags.PublicInstance) { return mb != null ? GetMembers(mb.Module, mb.GetMethodBody(), bf) : null; }
        public static MemberInfo[]? GetMembers(Module? mdl, MethodBody? mb, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            Instruction[]? a = GetInstructions(mdl, mb, OperandType.InlineField, OperandType.InlineMethod);

            if (a == null)
                return null;

            List<MemberInfo>
                l = new List<MemberInfo>(a.Length);

            for (int i = 0; i < a.Length; i++)
            {
                switch(a[i].KeyOpCode.OperandType)
                {
                    case OperandType.InlineField:
                        l.Add(a[i].Info as FieldInfo);
                        break;
                    case OperandType.InlineMethod:
                        MethodInfo mi = a[i].Info as MethodInfo;
                        if (!mi.IsSpecialName || !mi.IsHideBySig)
                        {
                            l.Add(mi);
                            continue;
                        }
                        String s = mi.Name;

                        if (s.StartsWith(CClausole.SetPrefixOfHideBySig))
                            s = s.Substring(CClausole.SetPrefixOfHideBySig.Length);
                        else if (s.StartsWith(CClausole.GetPrefixOfHideBySig))
                            s = s.Substring(CClausole.GetPrefixOfHideBySig.Length);

                        MemberInfo? mi0 = GetMember(mi.DeclaringType, s, bf);
                        if (mi0 == null) continue;
                        l.Add(mi0);
                        break;
                }
            }

            return l.ToArray();
        }

        #endregion

        #region public static Type? GetDeclaringType(...)

        public static Type? GetDeclaringType(MemberInfo? m)
        {
            return m != null ? m.DeclaringType : null;
        }

        #endregion

        #region public static Type? GetMemberType(...)

        public static MemberTypes? GetMemberType(MemberInfo? m)
        {
            return m != null ? m.MemberType : null;
        }

        #endregion

        #region public static Type? GetMemberValueType(...)

        public static Type? GetMemberValueType(MemberInfo? mi)
        {
            if (mi != null)
                switch (mi.MemberType)
                {
                    case MemberTypes.Property:
                        return (mi as PropertyInfo).PropertyType;
                    case MemberTypes.Field:
                        return (mi as FieldInfo).FieldType;
                    case MemberTypes.Method:
                        return (mi as MethodInfo).ReturnType;
                }

            return null;
        }

        #endregion

        #region public static Boolean SetPropertyValue(...)

        public static bool SetPropertyValue(object? o, PropertyInfo? pi, object? oValue, bool bForceValueCompatibility = true)
        {
            if (o != null && pi != null)
                try { pi.SetValue(o, bForceValueCompatibility ? ObjectUtils.Parse(pi.PropertyType, oValue) : oValue); return true; } catch { }

            return false;
        }

        #endregion

        #region public static Boolean SetFieldValue(...)

        public static bool SetFieldValue(object? o, FieldInfo? fi, object? oValue, bool bForceValueCompatibility = true)
        {
            if (o != null && fi != null)
                try { fi.SetValue(o, bForceValueCompatibility ? ObjectUtils.Parse(fi.FieldType, oValue) : oValue); return true; } catch { }

            return false;
        }

        #endregion

        #region public static Boolean SetMemberValue(...)

        public static bool SetMemberValue(object? o, MemberInfo? mi, object? ov, bool bForceValueCompatibility = true)
        {
            if (mi != null)
                switch (mi.MemberType)
                {
                    case MemberTypes.Property:
                        return SetPropertyValue(o, mi as PropertyInfo, ov, bForceValueCompatibility);
                    case MemberTypes.Field:
                        return SetFieldValue(o, mi as FieldInfo, ov, bForceValueCompatibility);
                }

            return false;
        }

        #endregion

        #region public static Object? GetPropertyValue(...)

        public static T? GetPropertyValue<T>(object? o, PropertyInfo? pi) { return ObjectUtils.Cast<T>(GetPropertyValue(o, pi)); }
        public static object? GetPropertyValue(object? o, PropertyInfo? pi)
        {
            if (o != null && pi != null)
                try { return pi.GetValue(o); } catch { }

            return null;
        }

        #endregion

        #region public static Object? GetFieldValue(...)

        public static T? GetFieldValue<T>(FieldInfo? fi) { return GetFieldValue<T>(null, fi); }
        public static T? GetFieldValue<T>(object? o, FieldInfo? fi) { return ObjectUtils.Cast<T>(GetFieldValue(o, fi)); }
        public static object? GetFieldValue(FieldInfo? fi) { return GetFieldValue(null, fi); }
        public static object? GetFieldValue(object? o, FieldInfo? fi)
        {
            if (
                fi != null
                &&
                (
                    fi.IsStatic
                    || o != null
                )
            )
                try { return fi.GetValue(o); } catch { }

            return null;
        }

        #endregion

        #region public static Object? GetMemberValue(...)

        public static T? GetMemberValue<T>(object? o, MemberInfo? mi) { return ObjectUtils.Cast<T>(GetMemberValue(o, mi)); }
        public static object? GetMemberValue(object? o, MemberInfo? mi)
        {
            if (mi != null)
                switch (mi.MemberType)
                {
                    case MemberTypes.Property:
                        return GetPropertyValue(o, mi as PropertyInfo);
                    case MemberTypes.Field:
                        return GetFieldValue(o, mi as FieldInfo);
                }

            return null;
        }

        #endregion

        #region public static Object? CreateInstance(...)

        public static T? CreateInstance<T>() { return CreateInstance<T>(CBindingFlags.PublicInstance); }
        public static T? CreateInstance<T>(BindingFlags bf) { return CreateInstance<T>(bf, null); }
        public static T? CreateInstance<T>(Object?[]? oa) { return CreateInstance<T>(CBindingFlags.PublicInstance, oa); }
        public static T? CreateInstance<T>(BindingFlags bf, Object?[]? oa) { return CreateInstance<T>(bf, null, oa); }
        public static T? CreateInstance<T>(Type[]? ta, Object?[]? oa) { return CreateInstance<T>(CBindingFlags.PublicInstance, ta, oa); }
        public static T? CreateInstance<T>(BindingFlags bf, Type[]? ta, Object?[]? oa) { return ObjectUtils.Cast<T>(CreateInstance(typeof(T), bf, ta, oa)); }
        public static object? CreateInstance(Type? t) { return CreateInstance(t, CBindingFlags.PublicInstance); }
        public static object? CreateInstance(Type? t, BindingFlags bf) { return CreateInstance(t, bf, null); }
        public static object? CreateInstance(Type? t, Object?[]? oa) { return CreateInstance(t, CBindingFlags.PublicInstance, null, oa); }
        public static object? CreateInstance(Type? t, Type[]? ta, Object?[]? oa) { return CreateInstance(t, CBindingFlags.PublicInstance, ta, oa); }
        public static object? CreateInstance(Type? t, BindingFlags bf, Object?[]? oa) { return CreateInstance(t, bf, null, oa); }
        public static object? CreateInstance(Type? t, BindingFlags bf, Type[]? ta, Object?[]? oa)
        {
            Object? o = InvokeConstructor<Object>( GetConstructor(t, bf, ta), oa );
            if (o != null)
                return o;

            Object?[]? oa0;
            ConstructorInfo? ci;
            __FindCompatibleConstructor(ref t, ref oa, out ci, out oa0);
            if (ci != null)
            {
                o = InvokeConstructor<Object>(ci, oa0);
                if (o != null) return o;
            }

            try
            {
                return Activator.CreateInstance(t, oa);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region public static Boolean InvokeConstructor(...)

        public static T? InvokeConstructor<T>(ConstructorInfo? ci, params object?[]? a)
        {
            if (ci != null)
                try { return ObjectUtils.Cast<T>(ci.Invoke(a)); } catch { }

            return default(T);
        }

        #endregion

        #region public static Boolean InvokeMethod(...)

        public static T? InvokeMethod<T>(object? o, MethodInfo? mi, params object[]? a)
        {
            if (mi != null && o != null)
                try { return ObjectUtils.Cast<T>(mi.Invoke(o, a)); } catch { }

            return default(T);
        }

        #endregion

        #region public static Object? InvokeMember(...)

        public static T? InvokeMember<T>(object? o, MemberInfo? mi, params object[]? a)
        {
            if (mi != null)
                switch (mi.MemberType)
                {
                    case MemberTypes.Method:
                        return InvokeMethod<T>(o, mi as MethodInfo, a);
                    case MemberTypes.Constructor:
                        return InvokeConstructor<T>(mi as ConstructorInfo, a);
                }

            return default(T);
        }

        #endregion

        #region public static OpCode[]? GetOpCodes()

        public static OpCode[]? GetOpCodes()
        {
            lock (__lck0)
            {
                if (__bIsOpCodesFetched)
                    return __oca;

                __bIsOpCodesFetched = true;

                FieldInfo[]?
                    fia = GetFields(CType.OpCodes, CBindingFlags.PublicStatic);

                if (fia == null)
                    return null;

                __oca = new OpCode[fia.Length];

                OpCode? oci;
                String? socni;
                for (int i = 0; i < fia.Length; i++)
                {
                    oci = GetFieldValue<OpCode>(fia[i]);
                    if (oci == null) continue;
                    __d13[oci.Value.Value] = oci;
                    socni = oci.Value.Name;
                    __NormalizeName(ref socni, out socni);
                    __d14[socni] = oci;
                    __oca[i] = oci.Value;
                }

                return __oca;
            }
        }

        #endregion

        #region public static OpCode? GetOpCode(...)

        public static OpCode? GetOpCode(String? s)
        {
            __NormalizeName(ref s, out s);
            GetOpCodes();
            OpCode? oc; __d14.TryGetValue(s, out oc);
            return oc;
        }

        public static OpCode? GetOpCode(Int16 i)
        {
            GetOpCodes();
            OpCode? oc; __d13.TryGetValue(i, out oc);
            return oc;
        }

        #endregion

        #region public static Object? ResolveTokenizedObject(...)

        public static T? ResolveTokenizedObject<T>(int iToken) { return ObjectUtils.Cast<T>(ResolveTokenizedObject(iToken)); }
        public static Object? ResolveTokenizedObject(int iToken)
        {
            lock(__lck2)
            {
                Object? o;
                __d15.TryGetValue(iToken, out o);
                return o;
            }
        }

        #endregion

        #region public static MethodInfo? ResolveMethod(...)

        public static MethodInfo? ResolveMethod(Module? mdl, int iMetadataToken) { MethodInfo? mi; __TryResolve(ref mdl, ref iMetadataToken, out mi); return mi; }

        #endregion

        #region public static MethodInfo? ResolveField(...)

        public static FieldInfo? ResolveField(Module? mdl, int iMetadataToken) { FieldInfo? fi; __TryResolve(ref mdl, ref iMetadataToken, out fi); return fi; }

        #endregion

        #region public static MemberInfo? ResolveMember(...)

        public static MemberInfo? ResolveMember(Module? mdl, int iMetadataToken) { MemberInfo? mi; __TryResolve(ref mdl, ref iMetadataToken, out mi); return mi; }

        #endregion

        #region public static MemberInfo? ResolveString(...)

        public static String? ResolveString(Module? mdl, int iMetadataToken) { String? s; __TryResolve(ref mdl, ref iMetadataToken, out s); return s; }

        #endregion

        #region public static Byte[]? ResolveSignature(...)

        public static Byte[]? ResolveSignature(Module? mdl, int iMetadataToken) { Byte[]? a; __TryResolve(ref mdl, ref iMetadataToken, out a); return a; }

        #endregion

        #region public static Instruction[]? GetInstructions(...)

        public static Instruction[]? GetInstructions(Delegate? dlg, params OperandType[]? ota) { return dlg != null ? GetInstructions(dlg.Method, ota) : null; }
        public static Instruction[]? GetInstructions(MethodBase? mi, params OperandType[]? ota)  { return mi != null ? GetInstructions(mi.Module, mi.GetMethodBody(), ota) : null; }
        public static Instruction[]? GetInstructions(Module? mdl, MethodBody? mb, params OperandType[]? ota)
        {
            if (mdl == null || mb == null) return null;

            byte[]? ba = mb.GetILAsByteArray();

            if (ba == null) return null;

            IEnumerator enmb = ba.GetEnumerator();

            List<OpCode>
                loc = new List<OpCode>(ba.Length);

            List<Instruction>
                li = new List<Instruction>(ba.Length);

            long? l, lj;
            int? i;
            int nni;

            // Bytes Count
            int? ibc;

            Boolean? bIsDirectAccess;
            Object? oi;
            OpCode? oci, ocj;
            //OpCode nnoci;

            HashSet<OperandType>?
                hs = ota != null && ota.Length > 0 ? new HashSet<OperandType>(ota.Length) : null;

            HashSetUtils.UnionWith(hs, ota);

            while (enmb.MoveNext())
            {
                loc.Clear();
                oci = GetOpCode((byte)enmb.Current);
                if (oci == null || (hs != null && !hs.Contains(oci.Value.OperandType))) continue;
                //nnoci = oci.Value;

                //Console.WriteLine(oci.Value.Name);
                //Console.WriteLine("\tOperandType\t" + StringUtils.NNParse(oci.Value.OperandType));
                //Console.WriteLine("\tOpCodeType\t" + StringUtils.NNParse(oci.Value.OpCodeType));
                //Console.WriteLine("\tFlowControl\t" + StringUtils.NNParse(oci.Value.FlowControl));
                //Console.WriteLine("\tSize\t" + StringUtils.NNParse(oci.Value.Size));
                //Console.WriteLine("\tValue\t" + StringUtils.NNParse(oci.Value.Value));
                //Console.WriteLine(String.Empty);
                loc.Add(oci.Value);

                switch (oci.Value.OperandType)
                {
                    // 64bit Operands
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                        ibc = 8;
                        break;
                    // 32bit Operands
                    case OperandType.ShortInlineR:
                    case OperandType.InlineBrTarget:
                    case OperandType.InlineI:
                    case OperandType.InlineSwitch:
                    case OperandType.InlineType:
                    case OperandType.InlineMethod:
                    case OperandType.InlineField:
                    case OperandType.InlineSig:
                    case OperandType.InlineString:
                        ibc = 4;
                        break;
                    // 16bit Operands
                    case OperandType.InlineVar:
                        ibc = 2;
                        break;
                    // 8bit Operands : Direct Access
                    case OperandType.ShortInlineBrTarget:
                    case OperandType.ShortInlineVar:
                    case OperandType.ShortInlineI:
                        ibc = 1;
                        break;

                    // 0bit Operands
                    case OperandType.InlineNone:
                    default:
                        ibc = 0;
                        break;
                }

                switch(oci.Value.OperandType)
                { 
                    // Direct Access
                    case OperandType.InlineI8:
                    case OperandType.InlineI:
                    case OperandType.InlineSwitch:
                    case OperandType.ShortInlineI:
                        bIsDirectAccess = true;
                        break;
                    default:
                        bIsDirectAccess = false;
                        break;
                }

                if (ibc > 0)
                {
                    l = 0L;
                    for (int j = 0; j < ibc; j++)
                    {
                        if (!enmb.MoveNext()) continue;

                        else if (!bIsDirectAccess.Value)
                        {
                            ocj = GetOpCode((byte)enmb.Current);
                            if (ocj == null) continue;
                            loc.Add(ocj.Value);
                            lj = Int64Utils.Parse(ocj.Value.Value);

                            Console.WriteLine("\t" + ocj.Value.Name);
                            Console.WriteLine("\t\tOperandType\t" + StringUtils.NNParse(ocj.Value.OperandType));
                            Console.WriteLine("\t\tOpCodeType\t" + StringUtils.NNParse(ocj.Value.OpCodeType));
                            Console.WriteLine("\t\tFlowControl\t" + StringUtils.NNParse(ocj.Value.FlowControl));
                            Console.WriteLine("\t\tSize\t" + StringUtils.NNParse(ocj.Value.Size));
                            Console.WriteLine("\t\tValue\t" + StringUtils.NNParse(ocj.Value.Value));
                            Console.WriteLine("\t\tOperand\t" + l);
                        }
                        else
                            lj = Int64Utils.Parse(enmb.Current);

                        l |= lj << (8 * j);
                    }
                }
                else
                    l = null;

                switch (oci.Value.OperandType)
                {
                    case OperandType.InlineMethod:
                    case OperandType.InlineField:
                    case OperandType.InlineString:
                    case OperandType.InlineType:
                    case OperandType.InlineSig:
                        // Int32 : MetadataToken
                        i = Int32Utils.Parse(l);
                        if (i == null)
                        {
                            oi = null;
                            break;
                        }

                        nni = i.Value;

                        switch (oci.Value.OperandType)
                        {
                            case OperandType.InlineMethod:
                                MethodInfo? mi;
                                if (!__TryResolve(ref mdl, ref nni, out mi)) continue;
                                oi = mi;
                                break;
                            case OperandType.InlineField:
                                FieldInfo? fi;
                                if (!__TryResolve(ref mdl, ref nni, out fi)) continue;
                                oi = fi;
                                break;
                            case OperandType.InlineString:
                                String? s;
                                if (!__TryResolve(ref mdl, ref nni, out s)) continue;
                                oi = s;
                                break;
                            case OperandType.InlineType:
                                Type? t;
                                if (!__TryResolve(ref mdl, ref nni, out t)) continue;
                                oi = t;
                                break;
                            case OperandType.InlineSig:
                                Byte[]? a;
                                if (!__TryResolve(ref mdl, ref nni, out a)) continue;
                                oi = a;
                                break;
                            default:
                                oi = null;
                                break;
                        }
                        break;
                    case OperandType.InlineI:
                        // Int32 : Value
                        oi = Int32Utils.Parse(l);
                        break;
                    case OperandType.InlineI8:
                        // Int64 : Value
                        oi = l;
                        break;
                    case OperandType.ShortInlineI:
                        // Int16 : Value
                        oi = Int16Utils.Parse(l);
                        break;
                    default:
                        oi = l;
                        break;
                }

                li.Add(new Instruction(ref oi, ref oci, ref loc));
            }

            return li.ToArray();
        }

        #endregion

        #region public static ... Copy<...>()

        public static T? Copy<T>(T? o, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            if (o == null) return default(T);
            T? o0 = ReflectionUtils.InvokeConstructor<T>(ReflectionUtils.GetConstructor(o.GetType(), bf));
            return Copy(ref o, ref o0, bf) ? o0 : default(T);
        }
        public static Boolean Copy<T>(ref T? oIn, ref T? oOut, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            if (oIn == null || oOut == null)
                return false;

            Type
                to = oIn.GetType();

            MemberInfo[]?
                a = ReflectionUtils.GetMembers(to, bf);

            if (a == null)
                return true;

            Type? ti, ti0;
            Object? oi;
            for (int i = 0; i < a.Length; i++)
            {
                ti = ReflectionUtils.GetMemberValueType(a[i]);
                if (ti == null) continue;
                oi = ReflectionUtils.GetMemberValue(oIn, a[i]);
                ti0 = Nullable.GetUnderlyingType(ti);
                if (ti0 != null) ti = ti0;
                if (!ti.IsPrimitive) oi = JSONUtils.Deserialize(ti, JSONUtils.Serialize(oi));
                ReflectionUtils.SetMemberValue(oOut, a[i], oi, false);
            }

            return true;
        }

        #endregion

        #endregion

        #region internal static ...

        internal static void RegisterTokenizedObject(TokenizedObject to)
        {
            lock(__lck2)
            {
                __d15[to.Token] = to;
            }
        }

        #endregion

        #region private static ...

        #region private static void __Get(...)

        private static void __Get<T>
        (
            ref Type? t,
            ref string? s, /*ref MemberTypes mt,*/
            ref BindingFlags bf,
            ref Type[]? ta,
            out T[]? uia
        )
            where T : MemberInfo
        {
            lock (__lck0)
            {
                if (!Analyze(t))
                {
                    uia = null;
                    return;
                }

                Type t0 = typeof(T);
                MemberTypes mt;

                Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>> d;

                if (t0 == CType.PropertyInfo)
                {
                    mt = MemberTypes.Property;
                    d = __d7 as Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>>;
                }
                else if (t0 == CType.FieldInfo)
                {
                    mt = MemberTypes.Field;
                    d = __d8 as Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>>;
                }
                else if (t0 == CType.MethodInfo)
                {
                    mt = MemberTypes.Method;
                    d = __d9 as Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>>;
                }
                else if (t0 == CType.ConstructorInfo)
                {
                    mt = MemberTypes.Constructor;
                    d = __d10 as Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>>;
                }
                else
                {
                    mt = MemberTypes.Constructor | MemberTypes.Property | MemberTypes.Field | MemberTypes.Method;
                    d = __d6 as Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, T[]?>>>>>>;
                }

                if (s == null) s = string.Empty;
                long shk; CalculateHashKey(ref ta, out shk);

            D6:
                if (FetchFromDictionaryIfPossible(ref t, ref d, ref mt, ref bf, ref s, ref shk, out uia))
                    return;

                HashSet<int>? hs;

            D5:
                if (FetchFromDictionaryIfPossible(ref t, ref __d5, ref mt, ref bf, ref s, ref shk, out hs))
                {
                    List<T>?
                        l = hs != null ? new List<T>(hs.Count) : null;

                    if (l != null)
                    {
                        MemberInfo? mii;
                        T? ui;
                        foreach (int i in hs)
                        {
                            FetchFromDictionaryIfPossible(ref t, ref __d1, i, out mii);
                            ui = mii as T;
                            if (ui == null) continue;
                            l.Add(ui);
                        }

                        uia = l.ToArray();
                    }

                    AddToDictionaryIfNecessary(ref t, ref d, ref mt, ref bf, ref s, ref shk, ref uia);
                    goto D6;
                }

                HashSet<int>?
                    hs2 = new HashSet<int>();

                if (mt.HasFlag(MemberTypes.Field))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d2, MemberTypes.Field, out hs);
                    HashSetUtils.UnionWith(hs2, hs);
                }

                if (mt.HasFlag(MemberTypes.Constructor))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d2, MemberTypes.Constructor, out hs);
                    HashSetUtils.UnionWith(hs2, hs);
                }

                if (mt.HasFlag(MemberTypes.Property))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d2, MemberTypes.Property, out hs);
                    HashSetUtils.UnionWith(hs2, hs);
                }

                if (mt.HasFlag(MemberTypes.Method))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d2, MemberTypes.Method, out hs);
                    HashSetUtils.UnionWith(hs2, hs);
                }

                HashSet<int>
                    hs30 = new HashSet<int>();

                if (bf.HasFlag(BindingFlags.Public))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d3, BindingFlags.Public, out hs);
                    HashSetUtils.UnionWith(hs30, hs);
                }

                if (bf.HasFlag(BindingFlags.NonPublic))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d3, BindingFlags.NonPublic, out hs);
                    HashSetUtils.UnionWith(hs30, hs);
                }

                HashSetUtils.IntersectWith(hs2, hs30);

                HashSet<int>
                    hs31 = new HashSet<int>();

                if (bf.HasFlag(BindingFlags.Instance))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d3, BindingFlags.Instance, out hs);
                    HashSetUtils.UnionWith(hs31, hs);
                }

                if (bf.HasFlag(BindingFlags.Static))
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d3, BindingFlags.Static, out hs);
                    HashSetUtils.UnionWith(hs31, hs);
                }

                HashSetUtils.IntersectWith(hs2, hs31);

                if (s.Length > 0)
                {
                    FetchFromDictionaryIfPossible(ref t, ref __d4, s, out hs);
                    HashSetUtils.IntersectWith(hs2, hs);
                }

                FetchFromDictionaryIfPossible(ref t, ref __d11, shk, out hs);
                HashSetUtils.IntersectWith(hs2, hs);

                if (hs2.Count < 1) hs2 = null;
                AddToDictionaryIfNecessary(ref t, ref __d5, ref mt, ref bf, ref s, ref shk, ref hs2);
                goto D5;
            }
        }

        #endregion

        #region private static void CalculateHashKey(...)

        private static void CalculateHashKey
        (
           ref ParameterInfo[]? pia,
           out long i
        )
        {
            Type[]? ta = pia != null ? new Type[pia.Length] : null;

            if (ta != null)
            {
                for (int j = 0; j < pia.Length; j++)
                    ta[j] = pia[j].ParameterType;
            }

            CalculateHashKey(ref ta, out i);
        }

        private static void CalculateHashKey
        (
            ref Type[]? ta,
            out long i
        )
        {
            i = 0L;

            if (ta == null)
                return;

            for (int j = 0; j < ta.Length; j++)
            {
                if (ta[j] == null) continue;
                i += ta[j].Module.MetadataToken * __Multiplier;
                i += ta[j].MetadataToken * (j + 1);
            }
        }

        #endregion

        #region private static Boolean Analyze(...)

        private static bool Analyze(Type? t)
        {
            if (t == null)
                return false;

            HashSet<int>? hs;
            if (!__d0.TryGetValue(t.Module.MetadataToken, out hs) || hs == null)
                __d0[t.Module.MetadataToken] = hs = new HashSet<int>();
            else if (hs.Contains(t.MetadataToken))
                return true;

            hs.Add(t.MetadataToken);

            MemberInfo[]? a;
            try { a = t.GetMembers(CBindingFlags.All); } catch { a = null; }
            if (a == null) return true;

            long shk;
            ParameterInfo[]? pia;
            BindingFlags bfi;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == null)
                    continue;

                switch (a[i].MemberType)
                {
                    case MemberTypes.Method:
                        MethodInfo mi = a[i] as MethodInfo;
                        if (mi.IsSpecialName && mi.IsHideBySig) continue;
                        CalculateBindingFlags(mi, out bfi);
                        pia = mi.GetParameters();
                        break;
                    case MemberTypes.Field:
                        FieldInfo fi = a[i] as FieldInfo;
                        if (fi.Name.EndsWith(">k__BackingField")) continue;
                        CalculateBindingFlags(fi, out bfi);
                        pia = null;
                        break;
                    case MemberTypes.Property:
                        PropertyInfo pi = a[i] as PropertyInfo;
                        CalculateBindingFlags(pi, out bfi);
                        pia = null;
                        break;
                    case MemberTypes.Constructor:
                        ConstructorInfo ci = a[i] as ConstructorInfo;
                        CalculateBindingFlags(ci, out bfi);
                        pia = ci.GetParameters();
                        break;
                    default:
                        continue;
                }

                AddToDictionaryIfNecessary(ref t, ref __d1, a[i].MetadataToken, ref a[i]);
                AddToDictionaryIfNecessary(ref t, ref __d2, a[i].MemberType, a[i].MetadataToken);

                if (bfi.HasFlag(BindingFlags.Public))
                    AddToDictionaryIfNecessary(ref t, ref __d3, BindingFlags.Public, a[i].MetadataToken);
                if (bfi.HasFlag(BindingFlags.NonPublic))
                    AddToDictionaryIfNecessary(ref t, ref __d3, BindingFlags.NonPublic, a[i].MetadataToken);
                if (bfi.HasFlag(BindingFlags.Instance))
                    AddToDictionaryIfNecessary(ref t, ref __d3, BindingFlags.Instance, a[i].MetadataToken);
                if (bfi.HasFlag(BindingFlags.Static))
                    AddToDictionaryIfNecessary(ref t, ref __d3, BindingFlags.Static, a[i].MetadataToken);

                AddToDictionaryIfNecessary(ref t, ref __d4, a[i].Name, a[i].MetadataToken);

                CalculateHashKey(ref pia, out shk);
                AddToDictionaryIfNecessary(ref t, ref __d11, 0, a[i].MetadataToken);
                AddToDictionaryIfNecessary(ref t, ref __d11, shk, a[i].MetadataToken);
            }

            return true;
        }

        #endregion

        #region private static void FetchFromDictionaryIfPossible<...>(...)

        private static Boolean FetchFromDictionaryIfPossible<V>
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>>> d,
            ref MemberTypes k0,
            ref BindingFlags k1,
            ref string k2,
            ref long k3,
            out V? v
        )
        {
            Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V>>>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null) { v = default; return false; }

            Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V>>>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null) { v = default; return false; }

            Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V>>>? d2;
            if (!d1.TryGetValue(k0, out d2) || d2 == null) { v = default; return false; }

            Dictionary<string, Dictionary<long, V>>? d3;
            if (!d2.TryGetValue(k1, out d3) || d3 == null) { v = default; return false; }

            Dictionary<long, V>? d4;
            if (!d3.TryGetValue(k2, out d4) || d4 == null) { v = default; return false; }

            return d4.TryGetValue(k3, out v);
        }

        private static void FetchFromDictionaryIfPossible<K>
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<K, HashSet<int>>>> d,
            K k,
            out HashSet<int>? hs
        )
        {
            Dictionary<int, Dictionary<K, HashSet<int>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null) { hs = null; return; }
            Dictionary<K, HashSet<int>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null) { hs = null; return; }
            d1.TryGetValue(k, out hs);
        }

        private static void FetchFromDictionaryIfPossible
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<int, MemberInfo>>> d,
            int k,
            out MemberInfo? v
        )
        {
            Dictionary<int, Dictionary<int, MemberInfo>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null) { v = null; return; }
            Dictionary<int, MemberInfo>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null) { v = null; return; }
            d1.TryGetValue(k, out v);
        }

        private static bool FetchFromDictionaryIfPossible
        (
            ref Type t,
            ref MemberInfo m,
            ref Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>> d,
            ref bool k,
            out Attribute[]? v
        )
        {
            Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null) { v = default; return false; }

            Dictionary<int, Dictionary<bool, Attribute[]>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null) { v = default; return false; }

            Dictionary<bool, Attribute[]>? d2;
            if (!d1.TryGetValue(m.MetadataToken, out d2) || d2 == null) { v = default; return false; }

            return d2.TryGetValue(k, out v);
        }

        #endregion

        #region private static void AddToDictionaryIfNecessary<...>(...)

        private static void AddToDictionaryIfNecessary<V>
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>>> d,
            ref MemberTypes k0,
            ref BindingFlags k1,
            ref string k2,
            ref long k3,
            ref V? v
        )
        {
            Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null)
                d[t.Module.MetadataToken] = d0 = new Dictionary<int, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>>();

            Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null)
                d0[t.MetadataToken] = d1 = new Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>>();

            Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>? d2;
            if (!d1.TryGetValue(k0, out d2) || d2 == null)
                d1[k0] = d2 = new Dictionary<BindingFlags, Dictionary<string, Dictionary<long, V?>>>();

            Dictionary<string, Dictionary<long, V?>>? d3;
            if (!d2.TryGetValue(k1, out d3) || d3 == null)
                d2[k1] = d3 = new Dictionary<string, Dictionary<long, V?>>();

            Dictionary<long, V?>? d4;
            if (!d3.TryGetValue(k2, out d4) || d4 == null)
                d3[k2] = d4 = new Dictionary<long, V?>();

            V? v0;
            if (!d4.TryGetValue(k3, out v0))
                d4[k3] = v;
        }

        private static void AddToDictionaryIfNecessary
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<int, MemberInfo>>> d,
            int k,
            ref MemberInfo v
        )
        {
            Dictionary<int, Dictionary<int, MemberInfo>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null)
                d[t.Module.MetadataToken] = d0 = new Dictionary<int, Dictionary<int, MemberInfo>>();

            Dictionary<int, MemberInfo>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null)
                d0[t.MetadataToken] = d1 = new Dictionary<int, MemberInfo>();

            MemberInfo? v0;
            if (!d1.TryGetValue(k, out v0))
                d1[k] = v;
        }

        private static void AddToDictionaryIfNecessary<K>
        (
            ref Type t,
            ref Dictionary<int, Dictionary<int, Dictionary<K, HashSet<int>>>> d,
            K k,
            int v
        )
            where K : notnull
        {
            Dictionary<int, Dictionary<K, HashSet<int>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null)
                d[t.Module.MetadataToken] = d0 = new Dictionary<int, Dictionary<K, HashSet<int>>>();

            Dictionary<K, HashSet<int>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null)
                d0[t.MetadataToken] = d1 = new Dictionary<K, HashSet<int>>();

            HashSet<int>? hs;
            if (!d1.TryGetValue(k, out hs) || hs == null)
                d1[k] = hs = new HashSet<int>();

            hs.Add(v);
        }

        private static void AddToDictionaryIfNecessary
        (
           ref Type t,
           ref MemberInfo m,
           ref Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>> d,
           ref bool k,
           ref Attribute[]? v
        )
        {
            Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>? d0;
            if (!d.TryGetValue(t.Module.MetadataToken, out d0) || d0 == null)
                d[t.Module.MetadataToken] = d0 = new Dictionary<int, Dictionary<int, Dictionary<bool, Attribute[]?>>>();

            Dictionary<int, Dictionary<bool, Attribute[]?>>? d1;
            if (!d0.TryGetValue(t.MetadataToken, out d1) || d1 == null)
                d0[t.MetadataToken] = d1 = new Dictionary<int, Dictionary<bool, Attribute[]?>>();

            Dictionary<bool, Attribute[]?>? d2;
            if (!d1.TryGetValue(m.MetadataToken, out d2) || d2 == null)
                d1[m.MetadataToken] = d2 = new Dictionary<bool, Attribute[]?>();

            Attribute[]? v0;
            if (!d2.TryGetValue(k, out v0))
                d2[k] = v;
        }

        #endregion

        #region private static void CalculateBindingFlags(...)

        private static void CalculateBindingFlags(MethodInfo mi, out BindingFlags bf)
        {
            bf =
                BindingFlags.Default;

            bf |=
                mi.IsAssembly
                || mi.IsFamilyOrAssembly
                || mi.IsPrivate
                || !mi.IsPublic
                    ? BindingFlags.NonPublic
                    : BindingFlags.Public;

            bf |=
                mi.IsStatic
                    ? BindingFlags.Static
                    : BindingFlags.Instance;
        }

        private static void CalculateBindingFlags(FieldInfo fi, out BindingFlags bf)
        {
            bf =
                BindingFlags.Default;

            bf |=
                fi.IsAssembly
                || fi.IsFamilyOrAssembly
                || fi.IsPrivate
                || !fi.IsPublic
                    ? BindingFlags.NonPublic
                    : BindingFlags.Public;

            bf |=
                fi.IsStatic
                    ? BindingFlags.Static
                    : BindingFlags.Instance;
        }

        private static void CalculateBindingFlags(ConstructorInfo ci, out BindingFlags bf)
        {
            bf =
                BindingFlags.Default;

            bf |=
                ci.IsAssembly
                || ci.IsFamilyOrAssembly
                || ci.IsPrivate
                || !ci.IsPublic
                    ? BindingFlags.NonPublic
                    : BindingFlags.Public;

            bf |=
                ci.IsStatic
                    ? BindingFlags.Static
                    : BindingFlags.Instance;
        }

        private static void CalculateBindingFlags(PropertyInfo pi, out BindingFlags bf)
        {
            BindingFlags bfSet;
            if (pi.SetMethod != null) CalculateBindingFlags(pi.SetMethod, out bfSet);
            else bfSet = BindingFlags.Default;

            BindingFlags bfGet;
            if (pi.GetMethod != null) CalculateBindingFlags(pi.GetMethod, out bfGet);
            else bfGet = BindingFlags.Default;

            bf = BindingFlags.Default;

            bf |=
                bfSet.HasFlag(BindingFlags.Static)
                && bfGet.HasFlag(BindingFlags.Static)
                    ? BindingFlags.Static
                    : BindingFlags.Instance;

            bf |=
                bfSet.HasFlag(BindingFlags.NonPublic)
                && bfGet.HasFlag(BindingFlags.NonPublic)
                    ? BindingFlags.NonPublic
                    : BindingFlags.Public;
        }

        #endregion

        #region private static void __NormalizeName(...)

        private static void __NormalizeName(ref String? s0, out String s1)
        {
            s1 = s0 != null ? s0 : String.Empty;
            s1 = s1.Trim().ToLower();
        }

        #endregion

        #region private static Boolean _TryResolve<T>(...)

        private static Boolean __TryResolve<T>(ref Module? mdl, ref int mt, out T? o)
        {
            if (mdl == null)
            {
                o = default;
                return false;
            }

            Type to = typeof(T);

            Object? o0;
            try
            {
                if (to == CType.String)
                    o0 = mdl.ResolveString(mt);
                else if (to == CType.MemberInfo || to == CType.PropertyInfo)
                    o0 = mdl.ResolveMember(mt);
                else if (to == CType.FieldInfo)
                    o0 = mdl.ResolveField(mt);
                else if (to == CType.MethodInfo)
                    o0 = mdl.ResolveMethod(mt);
                else if (to == CType.Type)
                    o0 = mdl.ResolveType(mt);
                else if (to == CType.BytesArray)
                    o0 = mdl.ResolveSignature(mt);
                else
                    o0 = null;
            }
            catch
            {
                o0 = null;
            }

            o = ObjectUtils.Cast<T>(o0);
            return o != null;
        }

        #endregion

        #region private static void __FindCompatibleConstructor<...>(...)

        private static void __FindCompatibleConstructor(ref Type? t, ref Object?[]? oa, out ConstructorInfo? ci, out Object?[]? oaOut)
        {
            ConstructorInfo[]?
                cia = ReflectionUtils.GetConstructors(t);

            if (cia != null)
                for (int i = 0; i < cia.Length; i++)
                {
                    __FindCompatibleConstructorParameters(ref cia[i], ref oa, out oaOut);
                    if (oaOut == null) continue;
                    ci = cia[i];
                    return;
                }

            ci = null;
            oaOut = null;
            return;
        }


        #endregion

        #region private static Boolean __FindCompatibleConstructorParameters()

        private static void __FindCompatibleConstructorParameters
        (
            ref ConstructorInfo? ci,
            ref Object?[]? oaIn,
            out Object?[]? oaOut
        )
        {
            if (ci == null)
            {
                oaOut = null;
                return;
            }

            ParameterInfo[] pia = ci.GetParameters();

            if (pia == null)
            {
                oaOut = null;
                return;
            }
            else if (pia.Length < 1)
            {
                oaOut = new Object[0];
                return;
            }
            else if (oaIn == null || oaIn.Length < 1)
            {
                oaOut = null;
                return;
            }

            oaOut = new Object[pia.Length];

            for (int i = 0; i < pia.Length; i++)
            {
                if (pia[i] == null) continue;

                for (int j = 0; j < oaIn.Length; j++)
                {
                    if
                    (
                        oaIn[j] == null
                        || !oaIn[j].GetType().Equals(pia[i].ParameterType)
                    )
                        continue;

                    oaOut[i] = oaIn[j];
                    break;
                }

                if (oaOut[i] == null)
                {
                    oaOut = null;
                    return;
                }
            }
        }

        #endregion

        #endregion
    }
}