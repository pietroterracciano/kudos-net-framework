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
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Kudos.Reflection.Utils
{
    public static class ReflectionUtils
    {
        // mi -> String
        private static Dictionary<MemberInfo, String>
            __dmi2hk;

        private static readonly Object
            __lck;

        // MemberInfos
        private static HashSet<MemberInfo>
            __hsami;
        // Type -> MemberInfo[]
        private static Dictionary<Type, MemberInfo[]?>
            __dt2mia;
        // Type -> MemberTypes -> MemberInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, MemberInfo[]?>?>
            __dt2emt2mia;
        // Type -> BindingFlags -> MemberInfo[]
        private static Dictionary<Type, Dictionary<BindingFlags, MemberInfo[]?>?>
            __dt2ebf2mia;
        // Type -> MemberInfo.Name -> MemberInfo
        private static Dictionary<Type, Dictionary<String, MemberInfo>?>
            __dt2min2mi;
        // Type -> HashKey(ParameterInfo[]) -> MemberInfo
        private static Dictionary<Type, Dictionary<String, MemberInfo[]?>?>
            __dt2pihk2mia;

        // Type -> MemberTypes -> BindingFlags -> MemberInfo.Name -> HashKey(ParameterInfo[]) -> MemberInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, MemberInfo[]?>?>?>?>?>
            __dt2emt2ebf2min2pihk2mia;
        // Type -> MemberTypes -> BindingFlags -> MemberInfo.Name -> HashKey(ParameterInfo[]) -> PropertyInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, PropertyInfo[]?>?>?>?>?>
            __dt2emt2ebf2min2pihk2pia;
        // Type -> MemberTypes -> BindingFlags -> MemberInfo.Name -> HashKey(ParameterInfo[]) -> FieldInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, FieldInfo[]?>?>?>?>?>
            __dt2emt2ebf2min2pihk2fia;
        // Type -> MemberTypes -> BindingFlags -> MemberInfo.Name -> HashKey(ParameterInfo[]) -> MethodInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, MethodInfo[]?>?>?>?>?>
            __dt2emt2ebf2min2pihk2mthia;
        // Type -> MemberTypes -> BindingFlags -> MemberInfo.Name -> HashKey(ParameterInfo[]) -> ConstructorInfo[]
        private static Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, ConstructorInfo[]?>?>?>?>?>
            __dt2emt2ebf2min2pihk2cia;

        // Type -> MemberInfo -> Boolean -> Attribute[]
        private static Dictionary<Type, Dictionary<MemberInfo, Dictionary<bool, Attribute[]?>?>?>
            __dt2mi2b2aa;

        // OpCode.Value -> OpCode
        private static readonly Dictionary<Int16, OpCode?>
            __docv2oc;

        // OpCode.Name -> OpCode
        private static readonly Dictionary<String, OpCode?>
            __docn2oc;

        private static OpCode[]? 
            __oca;
        private static readonly Object
            __lckoc;
        private static Boolean 
            __bIsOpCodesFetched;

        private static StringBuilder
            __sbmi2hk;

        private static readonly String
            __sdtfn,
            __smin;

        #region static ReflectionUtils()

        static ReflectionUtils()
        {
            __lck = new object();

            __hsami = new HashSet<MemberInfo>();
            __dt2mia = new Dictionary<Type, MemberInfo[]?>();
            __dt2min2mi = new Dictionary<Type, Dictionary<string, MemberInfo>?>();
            __dt2emt2mia = new Dictionary<Type, Dictionary<MemberTypes, MemberInfo[]?>?>();
            __dt2ebf2mia = new Dictionary<Type, Dictionary<BindingFlags, MemberInfo[]?>?>();
            __dt2pihk2mia = new Dictionary<Type, Dictionary<string, MemberInfo[]?>?>();

            __dt2emt2ebf2min2pihk2mia = new Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, MemberInfo[]?>?>?>?>?>();
            __dt2emt2ebf2min2pihk2pia = new Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, PropertyInfo[]?>?>?>?>?>();
            __dt2emt2ebf2min2pihk2fia = new Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, FieldInfo[]?>?>?>?>?>();
            __dt2emt2ebf2min2pihk2mthia = new Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, MethodInfo[]?>?>?>?>?>();
            __dt2emt2ebf2min2pihk2cia = new Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, ConstructorInfo[]?>?>?>?>?>();

            __dt2mi2b2aa = new Dictionary<Type, Dictionary<MemberInfo, Dictionary<bool, Attribute[]?>?>?>();
            __dmi2hk = new Dictionary<MemberInfo, string>();
            __sbmi2hk = new StringBuilder();

            __sdtfn = "dtfn";
            __smin = "min";

            __lckoc = new Object();
            __docv2oc = new Dictionary<short, OpCode?>();
            __docn2oc = new Dictionary<string, OpCode?>();
        }

        #endregion

        #region public static ...

        #region public static String? GetHashKey(...)

        public static String? GetHashKey(MemberInfo? mi)
        {
            String? shk;
            __GetHashKey(ref mi, out shk);
            return shk;
        }

        #endregion

        #region public static Attribute? GetCustomAttribute<...>(...)

        public static T? GetCustomAttribute<T>(MemberInfo? mi, bool bConsiderInheritance = true)
            where T : Attribute
        { return GetCustomAttribute(typeof(T), mi, bConsiderInheritance) as T; }
        public static Attribute? GetCustomAttribute(Type? t, MemberInfo? mi, bool bConsiderInheritance = true)
        {
            Attribute[]? aa = GetCustomAttributes(t, mi, bConsiderInheritance);
            return aa != null && aa.Length == 1 ? aa[0] : null;
        }

        #endregion

        #region public static Attribute? GetCustomAttributes<...>(...)

        public static T[]? GetCustomAttributes<T>(MemberInfo? mi, bool bConsiderInheritance = true)
            where T : Attribute
        { return GetCustomAttributes(typeof(T), mi, bConsiderInheritance) as T[]; }
        public static Attribute[]? GetCustomAttributes(Type? t, MemberInfo? mi, bool bConsiderInheritance = true)
        {
            lock (__dt2mi2b2aa)
            {
                Attribute[]? aa;

                FETCH:
                if (__FetchFromDictionaryIfPossible(ref __dt2mi2b2aa, ref t, ref mi, ref bConsiderInheritance, out aa))
                    return aa;

                try { aa = mi.GetCustomAttributes(t, bConsiderInheritance) as Attribute[]; } catch { }

                __AddToDictionaryIfNecessary(ref __dt2mi2b2aa, ref t, ref mi, ref bConsiderInheritance, ref aa);

                goto FETCH;
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

        #region public static Type? GetInterface<...>(...)

        public static Type? GetInterface<T>(string? s) { return GetInterface(typeof(T), s); }
        public static Type? GetInterface(Type? t, string? s)
        {
            if (t != null && s != null)
                try { return t.GetInterface(s); } catch { }

            return null;
        }

        #endregion

        #region public static Type? GetInterfaces<...>(...)

        public static Type[]? GetInterfaces<T>() { return GetInterfaces(typeof(T)); }
        public static Type[]? GetInterfaces(Type? t)
        {
            if (t != null)
                try { return t.GetInterfaces(); } catch { }

            return null;
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

        public static Task<Boolean> SetPropertyValueAsync(object? o, PropertyInfo? pi, object? oValue, bool bForceValueCompatibility = true)
        {
            return Task.Run(() => SetPropertyValue(o, pi, oValue, bForceValueCompatibility));
        }
        public static bool SetPropertyValue(object? o, PropertyInfo? pi, object? oValue, bool bForceValueCompatibility = true)
        {
            if (o != null && pi != null && pi.SetMethod != null)
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

        public static bool SetMemberValue(object? o, MemberInfo? mi, object? ov, bool bForceValueCompatibility = false)
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

        public static Task<T?> GetPropertyValueAsync<T>(object? o, PropertyInfo? pi) { return Task.Run(() => GetPropertyValue<T>(o, pi)); }
        public static T? GetPropertyValue<T>(object? o, PropertyInfo? pi) { return ObjectUtils.Cast<T>(GetPropertyValue(o, pi)); }
        public static Task<object?> GetPropertyValueAsync(object? o, PropertyInfo? pi) { return Task.Run(() => GetPropertyValue(o, pi)); }
        public static object? GetPropertyValue(object? o, PropertyInfo? pi)
        {
            if (o != null && pi != null && pi.GetMethod != null)
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
            ConstructorInfo? ci = GetConstructor(t, bf, ta);
            Object?[]? oa0;
            __FindCompatibleConstructorParameters(ref ci, ref oa, out oa0);
            Object? o = InvokeConstructor<Object>(ci, oa0);
            if (o != null) return o;

            //__FindCompatibleConstructor(ref t, ref bf, ref oa, out ci, out oa0);
            //if (ci != null)
            //{
            //    o = InvokeConstructor<Object>(ci, oa0);
            //    if (o != null) return o;
            //}

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

        public static Task<T?> InvokeConstructorAsync<T>(ConstructorInfo? ci) { return Task.Run(() => InvokeConstructor<T>(ci)); }
        public static T? InvokeConstructor<T>(ConstructorInfo? ci) { return ObjectUtils.Cast<T>(InvokeConstructor(ci)); }
        public static Task<T?> InvokeConstructorAsync<T>(ConstructorInfo? ci, params object?[]? a) { return Task.Run(() => InvokeConstructor<T>(ci,a)); }
        public static T? InvokeConstructor<T>(ConstructorInfo? ci, params object?[]? a) { return ObjectUtils.Cast<T>(InvokeConstructor(ci, a)); }
        public static Task<Object?> InvokeConstructorAsync(ConstructorInfo? ci) { return Task.Run(() => InvokeConstructor(ci)); }
        public static Object? InvokeConstructor(ConstructorInfo? ci) { return InvokeConstructor(ci, null); }
        public static Task<Object?> InvokeConstructorAsync(ConstructorInfo? ci, params object?[]? a) { return Task.Run(() => InvokeConstructor(ci, a)); }
        public static Object? InvokeConstructor(ConstructorInfo? ci, params object?[]? a)
        {
            if (ci != null)
                try { return ci.Invoke(a); } catch { }

            return null;
        }

        #endregion

        #region public static Boolean InvokeMethod(...)

        public static Task<T?> InvokeMethodAsync<T>(object? o, MethodInfo? mi) { return Task.Run(() => InvokeMethod<T>(o, mi)); }
        public static T? InvokeMethod<T>(object? o, MethodInfo? mi) { return ObjectUtils.Cast<T>(InvokeMethod(o, mi)); }
        public static Task<T?> InvokeMethodAsync<T>(object? o, MethodInfo? mi, params object[]? a) { return Task.Run(() => InvokeMethod<T>(o, mi, a)); }
        public static T? InvokeMethod<T>(object? o, MethodInfo? mi, params object[]? a) { return ObjectUtils.Cast<T>(InvokeMethod(o, mi, a)); }
        public static Task<Object?> InvokeMethodAsync(object? o, MethodInfo? mi) { return Task.Run(() => InvokeMethod(o, mi)); }
        public static Object? InvokeMethod(object? o, MethodInfo? mi) { return InvokeMethod(o, mi, null); }
        public static Task<Object?> InvokeMethodAsync(object? o, MethodInfo? mi, params object[]? a) { return Task.Run(() => InvokeMethod(o, mi, a)); }
        public static Object? InvokeMethod(object? o, MethodInfo? mi, params object[]? a)
        {
            if (mi != null && o != null)
                try { return mi.Invoke(o, a); } catch { }

            return null;
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
            lock (__lckoc)
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
                    __docv2oc[oci.Value.Value] = oci.Value;
                    socni = oci.Value.Name;
                    __NormalizeName(ref socni, out socni);
                    __docn2oc[socni] = oci.Value;
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
            OpCode? oc; __docn2oc.TryGetValue(s, out oc);
            return oc;
        }

        public static OpCode? GetOpCode(Int16 i)
        {
            GetOpCodes();
            OpCode? oc; __docv2oc.TryGetValue(i, out oc);
            return oc;
        }

        #endregion

        #region public static Object? ResolveTokenizedObject(...)

        public static T? ResolveTokenizedObject<T>(int iToken) { return ObjectUtils.Cast<T>(ResolveTokenizedObject(iToken)); }
        public static Object? ResolveTokenizedObject(int iToken)
        {
            throw new InvalidOperationException();
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

        #region public static Type? MakeGenericType(...)

        public static Type? MakeGenericType(Type? t, Type? tg, params Type[]? tga)
        {
            if (t == null) return null;
            Type?[] ta = ArrayUtils.UnShift(tg, tga);
            if (ta == null) return null;
            List<Type> l = new List<Type>(ta.Length);
            for (int i = 0; i < ta.Length; i++) { if (ta[i] == null) continue; l.Add(ta[i]); }
            try { return t.MakeGenericType(l.ToArray()); } catch { }
            return null;
        }

        #endregion

        #region public static ... Copy<...>()

        public static T? Copy<T>(T? o)
        {
            return Copy<T>(o, CBindingFlags.PublicInstance);
        }
        public static T? Copy<T>(T? o, BindingFlags bf)
        {
            return Copy<T>(o, bf, null);
        }
        public static T? Copy<T>(T? o, params MemberInfo?[]? aMembersToCopy)
        {
            return Copy<T>(o, CBindingFlags.PublicInstance, aMembersToCopy);
        }
        public static T? Copy<T>(T? o, BindingFlags bf, params MemberInfo?[]? aMembersToCopy)
        {
            if (o == null) return default(T);
            T? o0 = ReflectionUtils.InvokeConstructor<T>(ReflectionUtils.GetConstructor(o.GetType(), bf));
            return Copy(ref o, ref o0, bf, aMembersToCopy) ? o0 : default(T);
        }
        public static Boolean Copy<T>(ref T? oIn, ref T? oOut)
        {
            return Copy<T>(ref oIn, ref oOut, CBindingFlags.PublicInstance);
        }
        public static Boolean Copy<T>(ref T? oIn, ref T? oOut, BindingFlags bf)
        {
            return Copy<T>(ref oIn, ref oOut, bf, null);
        }
        public static Boolean Copy<T>(ref T? oIn, ref T? oOut, params MemberInfo?[]? aMembersToCopy)
        {
            return Copy<T>(ref oIn, ref oOut, CBindingFlags.PublicInstance, aMembersToCopy);
        }
        public static Boolean Copy<T>(ref T? oIn, ref T? oOut, BindingFlags bf, params MemberInfo?[]? aMembersToCopy)
        {
            if (oIn == null || oOut == null)
                return false;

            Type
                to = oIn.GetType();

            MemberInfo[]?
                a = ReflectionUtils.GetMembers(to, bf);

            if (a == null)
                return true;

            HashSet<String>? hsMembersToCopy;

            if (aMembersToCopy != null)
            {
                hsMembersToCopy = new HashSet<string>(aMembersToCopy.Length);
                for (int i = 0; i < aMembersToCopy.Length; i++)
                {
                    if (aMembersToCopy[i] == null) continue;
                    hsMembersToCopy.Add(aMembersToCopy[i].Name);
                }
            }
            else
                hsMembersToCopy = null;

            Type? ti, ti0;
            Object? oi;
            for (int i = 0; i < a.Length; i++)
            {
                if (hsMembersToCopy != null && !hsMembersToCopy.Contains(a[i].Name)) continue;
                ti = ReflectionUtils.GetMemberValueType(a[i]);
                if (ti == null) continue;
                oi = ReflectionUtils.GetMemberValue(oIn, a[i]);
                ti0 = Nullable.GetUnderlyingType(ti);
                if (ti0 != null) ti = ti0;

                if
                (
                    !ti.IsPrimitive
                    && ti != CType.String
                )
                    Copy(ref oi, ref oi, bf);  //JSONUtils.Deserialize(ti, JSONUtils.Serialize(oi));

                ReflectionUtils.SetMemberValue(oOut, a[i], oi, false);
            }

            return true;
        }

        #endregion

        #region public static ... ParseToExpandoObject(...)

        public static ExpandoObject? ParseToExpandoObject(Object? o, BindingFlags bf = CBindingFlags.PublicInstance)
        {
            Type?
                t = o as Type;

            if (t != null || o == null)
                return null;

            t = o.GetType();

            MemberInfo[]?
                mia = ArrayUtils.Append<MemberInfo>(GetProperties(t, bf), GetFields(t, bf));

            if (mia == null || mia.Length < 1)
                return null;

            ExpandoObject eo = new ExpandoObject();
            IDictionary<String, Object?> d = eo as IDictionary<String, Object?>;

            if (d == null)
                return null;

            for (int i = 0; i < mia.Length; i++)
                d[mia[i].Name] = GetMemberValue(o, mia[i]);

            return eo;
        }

        #endregion

        #endregion

        #region internal static ...

        internal static void RegisterTokenizedObject(TokenizedObject to)
        {
        }

        #endregion

        #region private static ...

        #region private static void __Get(...)

        private static void __Get<T>
        (
            ref Type? t,
            ref String? s, /*ref MemberTypes mt,*/
            ref BindingFlags bf,
            ref Type[]? ta,
            out T[]? taOut
        )
            where T : MemberInfo
        {
            lock (__lck)
            {
                if(!__Analyze(ref t))
                {
                    taOut = null;
                    return;
                }

                Type t0 = typeof(T);
                if (s == null) s = string.Empty;

                MemberTypes mt;

                Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?> d;

                if (t0 == CType.PropertyInfo)
                {
                    mt = MemberTypes.Property;
                    d = __dt2emt2ebf2min2pihk2pia as Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?>;
                }
                else if (t0 == CType.FieldInfo)
                {
                    mt = MemberTypes.Field;
                    d = __dt2emt2ebf2min2pihk2fia as Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?>;
                }
                else if (t0 == CType.MethodInfo)
                {
                    mt = MemberTypes.Method;
                    d = __dt2emt2ebf2min2pihk2mthia as Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?>;
                }
                else if (t0 == CType.ConstructorInfo)
                {
                    mt = MemberTypes.Constructor;
                    d = __dt2emt2ebf2min2pihk2cia as Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?>;
                }
                else
                {
                    mt = MemberTypes.Constructor | MemberTypes.Property | MemberTypes.Field | MemberTypes.Method;
                    d = __dt2emt2ebf2min2pihk2mia as Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<String, Dictionary<String, T[]?>?>?>?>?>;
                }

                String? stahk; __GetHashKey(ref ta, out stahk);
                if (stahk == null) stahk = String.Empty;

                FETCH0:
                if (__FetchFromDictionaryIfPossible(ref d, ref t, ref mt, ref bf, ref s, ref stahk, out taOut))
                    return;

                MemberInfo[]?
                    mia;

                FETCH1:
                if (__FetchFromDictionaryIfPossible(ref __dt2emt2ebf2min2pihk2mia, ref t, ref mt, ref bf, ref s, ref stahk, out mia))
                {
                    List<T>?
                        l = mia != null ? new List<T>(mia.Length) : null;

                    if (l != null)
                    {
                        T? tai;
                        for(int i=0; i < mia.Length; i++)
                        {
                            tai = mia[i] as T;
                            if (tai == null) continue;
                            l.Add(tai);
                        }

                        taOut = l.ToArray();
                    }

                    __AddToDictionaryIfNecessary(ref d, ref t, ref mt, ref bf, ref s, ref stahk, ref taOut);
                    goto FETCH0;
                }

                HashSet<MemberInfo>
                    hsemtmi = new HashSet<MemberInfo>();

                if (mt.HasFlag(MemberTypes.Field))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2emt2mia, ref t, MemberTypes.Field, out mia);
                    HashSetUtils.UnionWith(hsemtmi, mia);
                }

                if (mt.HasFlag(MemberTypes.Constructor))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2emt2mia, ref t, MemberTypes.Constructor, out mia);
                    HashSetUtils.UnionWith(hsemtmi, mia);
                }

                if (mt.HasFlag(MemberTypes.Property))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2emt2mia, ref t, MemberTypes.Property, out mia);
                    HashSetUtils.UnionWith(hsemtmi, mia);
                }

                if (mt.HasFlag(MemberTypes.Method))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2emt2mia, ref t, MemberTypes.Method, out mia);
                    HashSetUtils.UnionWith(hsemtmi, mia);
                }

                HashSet<MemberInfo>
                    hsebfmi0 = new HashSet<MemberInfo>();

                if (bf.HasFlag(BindingFlags.Public))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2ebf2mia, ref t, BindingFlags.Public, out mia);
                    HashSetUtils.UnionWith(hsebfmi0, mia);
                }

                if (bf.HasFlag(BindingFlags.NonPublic))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2ebf2mia, ref t, BindingFlags.NonPublic, out mia);
                    HashSetUtils.UnionWith(hsebfmi0, mia);
                }

                HashSetUtils.IntersectWith(hsemtmi, hsebfmi0);

                HashSet<MemberInfo>
                    hsebfmi1 = new HashSet<MemberInfo>();

                if (bf.HasFlag(BindingFlags.Instance))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2ebf2mia, ref t, BindingFlags.Instance, out mia);
                    HashSetUtils.UnionWith(hsebfmi1, mia);
                }

                if (bf.HasFlag(BindingFlags.Static))
                {
                    __FetchFromDictionaryIfPossible(ref __dt2ebf2mia, ref t, BindingFlags.Static, out mia);
                    HashSetUtils.UnionWith(hsebfmi1, mia);
                }

                HashSetUtils.IntersectWith(hsemtmi, hsebfmi1);

                MemberInfo?
                    mi;

                if (s.Length > 0)
                {
                    __FetchFromDictionaryIfPossible(ref __dt2min2mi, ref t, s, out mi);
                    if(mi != null) HashSetUtils.IntersectWith(hsemtmi, new MemberInfo[] { mi });
                }

                __FetchFromDictionaryIfPossible(ref __dt2pihk2mia, ref t, stahk, out mia);
                HashSetUtils.IntersectWith(hsemtmi, mia);

                mia = hsemtmi.Count > 0
                    ? new MemberInfo[hsemtmi.Count]
                    : null;

                if (mia != null)
                {
                    int i = 0;
                    foreach (MemberInfo mi0 in hsemtmi)
                    {
                        mia[i] = mi0;
                        i++;
                    }
                }

                __AddToDictionaryIfNecessary(ref __dt2emt2ebf2min2pihk2mia, ref t, ref mt, ref bf, ref s, ref stahk, ref mia);
                goto FETCH1;
            }
        }

        #endregion

        #region private static Boolean __Analyze(...)

        private static Boolean __Analyze(ref Type? t)
        {
            if (t == null)
                return false;

            lock(__lck)
            {
                if (__hsami.Contains(t)) return true;
                __hsami.Add(t);

                MemberInfo[]? mia;
                try { mia = t.GetMembers(CBindingFlags.All); } catch { mia = null; }
                if (mia == null)
                {
                    __dt2mia[t] = null;
                    return true;
                }

                String? shki;
                ParameterInfo[]? pia;
                BindingFlags bfi;

                List<MemberInfo> lmi = new List<MemberInfo>(mia.Length);

                Dictionary<String, MemberInfo> dmin2mi = new Dictionary<string, MemberInfo>(mia.Length);
                Dictionary<BindingFlags, List<MemberInfo>?> debf2lmi = new Dictionary<BindingFlags, List<MemberInfo>?>(mia.Length);
                Dictionary<MemberTypes, List<MemberInfo>?> demt2lmi = new Dictionary<MemberTypes, List<MemberInfo>?>(mia.Length);
                Dictionary<String, List<MemberInfo>> dpihk2mia = new Dictionary<string, List<MemberInfo>>(mia.Length);

                __dt2min2mi[t] = new Dictionary<string, MemberInfo>();

                String? spiahk;

                for (int i = 0; i < mia.Length; i++)
                {
                    if (mia[i] == null) continue;
                    lmi.Add(mia[i]);

                    switch (mia[i].MemberType)
                    {
                        case MemberTypes.Method:
                            MethodInfo mi = mia[i] as MethodInfo;
                            if (mi.IsSpecialName && mi.IsHideBySig) continue;
                            __CalculateBindingFlags(ref mi, out bfi);
                            pia = mi.GetParameters();
                            break;
                        case MemberTypes.Field:
                            FieldInfo fi = mia[i] as FieldInfo;
                            if (fi.Name.EndsWith(">k__BackingField")) continue;
                            __CalculateBindingFlags(ref fi, out bfi);
                            pia = null;
                            break;
                        case MemberTypes.Property:
                            PropertyInfo pi = mia[i] as PropertyInfo;
                            __CalculateBindingFlags(ref pi, out bfi);
                            pia = null;
                            break;
                        case MemberTypes.Constructor:
                            ConstructorInfo ci = mia[i] as ConstructorInfo;
                            __CalculateBindingFlags(ref ci, out bfi);
                            pia = ci.GetParameters();
                            break;
                        default:
                            continue;
                    }

                    dmin2mi[mia[i].Name] = mia[i];

                    __AddToDictionaryIfNecessary(ref demt2lmi, mia[i].MemberType, ref mia[i]);

                    if (bfi.HasFlag(BindingFlags.Public))
                        __AddToDictionaryIfNecessary(ref debf2lmi, BindingFlags.Public, ref mia[i]);
                    if (bfi.HasFlag(BindingFlags.NonPublic))
                        __AddToDictionaryIfNecessary(ref debf2lmi, BindingFlags.NonPublic, ref mia[i]);
                    if (bfi.HasFlag(BindingFlags.Instance))
                        __AddToDictionaryIfNecessary(ref debf2lmi, BindingFlags.Instance, ref mia[i]);
                    if (bfi.HasFlag(BindingFlags.Static))
                        __AddToDictionaryIfNecessary(ref debf2lmi, BindingFlags.Static, ref mia[i]);

                    __GetHashKey(ref pia, out spiahk);
                    __AddToDictionaryIfNecessary(ref dpihk2mia, String.Empty, ref mia[i]);
                    if(spiahk != null) __AddToDictionaryIfNecessary(ref dpihk2mia, spiahk, ref mia[i]);
                }

                __dt2mia[t] = lmi.ToArray();

                if (dmin2mi.Count > 0)
                {
                    Dictionary<String, MemberInfo>.Enumerator
                        enm = dmin2mi.GetEnumerator();

                    __dt2min2mi[t] = new Dictionary<string, MemberInfo>(dmin2mi.Count);

                    while (enm.MoveNext())
                        __dt2min2mi[t][enm.Current.Key] = enm.Current.Value;
                }

                if (demt2lmi.Count > 0)
                {
                    Dictionary<MemberTypes, List<MemberInfo>?>.Enumerator
                        enm = demt2lmi.GetEnumerator();

                    __dt2emt2mia[t] = new Dictionary<MemberTypes, MemberInfo[]?>(demt2lmi.Count);

                    while (enm.MoveNext())
                        __dt2emt2mia[t][enm.Current.Key] = enm.Current.Value.ToArray();
                }

                if (debf2lmi.Count > 0)
                {
                    Dictionary<BindingFlags, List<MemberInfo>?>.Enumerator
                        enm = debf2lmi.GetEnumerator();

                    __dt2ebf2mia[t] = new Dictionary<BindingFlags, MemberInfo[]?>(debf2lmi.Count);

                    while (enm.MoveNext())
                        __dt2ebf2mia[t][enm.Current.Key] = enm.Current.Value.ToArray();
                }

                if (dpihk2mia.Count > 0)
                {
                    Dictionary<String, List<MemberInfo>?>.Enumerator
                        enm = dpihk2mia.GetEnumerator();

                    __dt2pihk2mia[t] = new Dictionary<String, MemberInfo[]?>(dpihk2mia.Count);

                    while (enm.MoveNext())
                        __dt2pihk2mia[t][enm.Current.Key] = enm.Current.Value.ToArray();
                }

                return true;
            }
        }

        #endregion

        #region private static void FetchFromDictionaryIfPossible<...>(...)

        private static Boolean __FetchFromDictionaryIfPossible<V>
        (
            ref Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<String, V?>?>?>?>?> d,
            ref Type? k0,
            ref MemberTypes k1,
            ref BindingFlags k2,
            ref string k3,
            ref string k4,
            out V? v
        )
        {
            Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, V?>?>?>?>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null) { v = default; return false; }

            Dictionary<BindingFlags, Dictionary<string, Dictionary<String, V?>?>?>? d1;
            if (!d0.TryGetValue(k1, out d1) || d1 == null) { v = default; return false; }

            Dictionary<string, Dictionary<string, V?>?>? d2;
            if (!d1.TryGetValue(k2, out d2) || d2 == null) { v = default; return false; }

            Dictionary<String, V?>? d3;
            if (!d2.TryGetValue(k3, out d3) || d3 == null) { v = default; return false; }

            return d3.TryGetValue(k4, out v);
        }

        private static bool __FetchFromDictionaryIfPossible<K1>
        (
            ref Dictionary<Type, Dictionary<K1, MemberInfo>?> d,
            ref Type k0,
            K1 k1,
            out MemberInfo? v
        )
        {
            Dictionary<K1, MemberInfo>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null) { v = default; return false; }
            return d0.TryGetValue(k1, out v);
        }

        private static bool __FetchFromDictionaryIfPossible<K1>
        (
            ref Dictionary<Type, Dictionary<K1, MemberInfo[]?>?> d,
            ref Type k0,
            K1 k1,
            out MemberInfo[]? v
        )
        {
            Dictionary<K1, MemberInfo[]?>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null) { v = default; return false; }
            return d0.TryGetValue(k1, out v);
        }

        private static bool __FetchFromDictionaryIfPossible
        (
            ref Dictionary<Type, Dictionary<MemberInfo, Dictionary<Boolean, Attribute[]?>?>?> d,
            ref Type k0,
            ref MemberInfo k1,
            ref bool k2,
            out Attribute[]? v
        )
        {
            Dictionary<MemberInfo, Dictionary<bool, Attribute[]?>?>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null) { v = default; return false; }

            Dictionary<bool, Attribute[]?>? d1;
            if (!d0.TryGetValue(k1, out d1) || d1 == null) { v = default; return false; }

            return d1.TryGetValue(k2, out v);
        }

        #endregion

        #region private static void __AddToDictionaryIfNecessary<...>(...)

        private static void __AddToDictionaryIfNecessary<V>
        (
            ref Dictionary<Type, Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<String, V?>?>?>?>?>? d,
            ref Type? k0,
            ref MemberTypes k1,
            ref BindingFlags k2,
            ref string k3,
            ref string k4,
            ref V? v
        )
        {
            Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<String, V?>?>?>?>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null)
                d[k0] = d0 = new Dictionary<MemberTypes, Dictionary<BindingFlags, Dictionary<string, Dictionary<string, V?>?>?>?>();

            Dictionary<BindingFlags, Dictionary<string, Dictionary<String, V?>?>?>? d1;
            if (!d0.TryGetValue(k1, out d1) || d1 == null)
                d0[k1] = d1 = new Dictionary<BindingFlags, Dictionary<string, Dictionary<string, V?>?>?>();

            Dictionary<string, Dictionary<String, V?>?>? d2;
            if (!d1.TryGetValue(k2, out d2) || d2 == null)
                d1[k2] = d2 = new Dictionary<string, Dictionary<string, V?>?>();

            Dictionary<String, V?>? d3;
            if (!d2.TryGetValue(k3, out d3) || d3 == null)
                d2[k3] = d3 = new Dictionary<string, V?>();

            V? v0;
            if (!d3.TryGetValue(k4, out v0))
                d3[k4] = v;
        }

        private static void __AddToDictionaryIfNecessary<T>
        (
            ref Dictionary<T, List<MemberInfo>?> d,
            T k,
            ref MemberInfo v
        )
        {
            List<MemberInfo>? l;
            if (!d.TryGetValue(k, out l) || l == null) d[k] = l = new List<MemberInfo>();
            l.Add(v);
        }

        private static void __AddToDictionaryIfNecessary
        (
            ref Dictionary<Type, Dictionary<MemberInfo, Dictionary<Boolean, Attribute[]?>?>?> d,
            ref Type k0,
            ref MemberInfo k1,
            ref bool k2,
            ref Attribute[]? v
        )
        {
            Dictionary<MemberInfo, Dictionary<bool, Attribute[]?>?>? d0;
            if (!d.TryGetValue(k0, out d0) || d0 == null)
                d[k0] = d0 = new Dictionary<MemberInfo, Dictionary<bool, Attribute[]?>?>();

            Dictionary<bool, Attribute[]?>? d1;
            if (!d0.TryGetValue(k1, out d1) || d1 == null)
                d0[k1] = d1 = new Dictionary<bool, Attribute[]?>();

            Attribute[]? v0;
            if (!d1.TryGetValue(k2, out v0))
                d1[k2] = v;
        }

        #endregion

        #region private static void __CalculateBindingFlags(...)

        private static void __CalculateBindingFlags(MethodInfo mi, out BindingFlags bf) { __CalculateBindingFlags(ref mi, out bf); }
        private static void __CalculateBindingFlags(ref MethodInfo mi, out BindingFlags bf)
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

        private static void __CalculateBindingFlags(ref FieldInfo fi, out BindingFlags bf)
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

        private static void __CalculateBindingFlags(ref ConstructorInfo ci, out BindingFlags bf)
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

        private static void __CalculateBindingFlags(ref PropertyInfo pi, out BindingFlags bf)
        {
            BindingFlags bfSet;
            if (pi.SetMethod != null) __CalculateBindingFlags(pi.SetMethod, out bfSet);
            else bfSet = BindingFlags.Default;

            BindingFlags bfGet;
            if (pi.GetMethod != null) __CalculateBindingFlags(pi.GetMethod, out bfGet);
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

        private static void __FindCompatibleConstructor(ref Type? t, ref BindingFlags bf, ref Object?[]? oa, out ConstructorInfo? ci, out Object?[]? oaOut)
        {
            ConstructorInfo[]?
                cia = ReflectionUtils.GetConstructors(t, bf);

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

        #region private static void __GetTypes(...)

        private static void __GetTypes
        (
           ref ParameterInfo[]? pia,
           out Type[]? ta
        )
        {
            if(pia == null)
            {
                ta = null;
                return;
            }

            ta = new Type[pia.Length];

            for (int j = 0; j < pia.Length; j++)
                ta[j] = pia[j].ParameterType;
        }

        #endregion

        #region private static void __GetHashKey(...)

        private static void __GetHashKey(ref Type? t, out String? shk)
        {
            MemberInfo? mi = t as MemberInfo;
            __GetHashKey(ref mi, out shk);
        }
        private static void __GetHashKey(ref MemberInfo? mi, out String? shk)
        {
            if (mi == null) { shk = null; return; }

            lock (__sbmi2hk)
            {
                if (__dmi2hk.TryGetValue(mi, out shk))
                    return;

                __sbmi2hk
                    .Clear();

                if (mi.DeclaringType != null)
                    __sbmi2hk.Append(__sdtfn).Append(CCharacter.DoubleDot).Append(mi.DeclaringType.FullName).Append(CCharacter.Pipe);

                __sbmi2hk.Append(__smin).Append(CCharacter.DoubleDot).Append(mi.Name);

                shk = __dmi2hk[mi] = __sbmi2hk.ToString();
            }
        }

        private static void __GetHashKey
        (
           ref ParameterInfo[]? pia,
           out String? s
        )
        {
            Type[]? ta;
            __GetTypes(ref pia, out ta);
            __GetHashKey(ref ta, out s);
        }

        private static void __GetHashKey
        (
            ref Type[]? ta,
            out String? s
        )
        {
            if(ta == null)
            {
                s = null;
                return;
            }

            StringBuilder sb = new StringBuilder();

            String? si;
            for (int i=0; i<ta.Length; i++)
            {
                __GetHashKey(ref ta[i], out si);
                if (si != null) sb.Append(si).Append(CCharacter.Pipe);
            }

            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);

            s = sb.ToString();
        }

        #endregion

        #endregion
    }
}