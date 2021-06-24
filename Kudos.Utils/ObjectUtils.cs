using Kudos.Utils.Integers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Kudos.Utils
{
    public abstract class ObjectUtils
    {
        #region Members

        #region public static PropertyInfo[] GetProperties()

        /*
        /// <summary>Nullable</summary>
        public static PropertyInfo[] GetProperties(Object oObject)
        {
            return GetProperties(oObject, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static PropertyInfo[] GetProperties(Object oObject, BindingFlags eBindingFlags)
        {
            return GetProperties(oObject, eBindingFlags);
        }*/

        /// <summary>Nullable</summary>
        public static PropertyInfo[] GetProperties(Type oType)
        {
            return GetProperties(oType, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static PropertyInfo[] GetProperties(Type oType, BindingFlags eBindingFlags)
        {
            if (oType != null)
                try
                {
                    return oType.GetProperties(eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static MemberInfo[] GetMembers()

        public static MemberInfo[] GetMembers(Type oType)
        {
            return GetMembers(oType, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static MemberInfo[] GetMembers(Type oType, BindingFlags eBindingFlags)
        {
            if (oType != null)
                try
                {
                    return oType.GetMembers(eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static FieldInfo[] GetFields()

        /// <summary>Nullable</summary>
        public static FieldInfo[] GetFields(Object oObject)
        {
            return GetFields(oObject, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static FieldInfo[] GetFields(Object oObject, BindingFlags eBindingFlags)
        {
            return GetFields(oObject, eBindingFlags);
        }

        /// <summary>Nullable</summary>
        public static FieldInfo[] GetFields(Type oType)
        {
            return GetFields(oType, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static FieldInfo[] GetFields(Type oType, BindingFlags eBindingFlags)
        {
            if (oType != null)
                try
                {
                    return oType.GetFields(eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static MethodInfo[] GetMethods()

        /// <summary>Nullable</summary>
        public static MethodInfo[] GetMethods(Object oObject)
        {
            return GetMethods(oObject, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static MethodInfo[] GetMethods(Object oObject, BindingFlags eBindingFlags)
        {
            return GetMethods(oObject, eBindingFlags);
        }

        /// <summary>Nullable</summary>
        public static MethodInfo[] GetMethods(Type oType)
        {
            return GetMethods(oType, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static MethodInfo[] GetMethods(Type oType, BindingFlags eBindingFlags)
        {
            if (oType != null)
                try
                {
                    return oType.GetMethods(eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #endregion

        #region Member

        #region Property

        #region public static PropertyInfo GetProperty()

        /*
        /// <summary>Nullable</summary>
        public static PropertyInfo GetProperty(Object oObject, String sName)
        {
            return GetProperty(oObject, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static PropertyInfo GetProperty(Object oObject, String sName, BindingFlags eBindingFlags)
        {
            return GetProperty(oObject, sName, eBindingFlags);
        }*/

        /// <summary>Nullable</summary>
        public static PropertyInfo GetProperty(Type oType, String sName)
        {
            return GetProperty(oType, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static PropertyInfo GetProperty(Type oType, String sName, BindingFlags eBindingFlags)
        {
            if (oType != null && !String.IsNullOrWhiteSpace(sName))
                try
                {
                    return oType.GetProperty(sName, eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static Attribute GetPropertyAttribute()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetProperty(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            PropertyInfo oProperty
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oProperty, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetPropertyAttribute<AttributeType>(
            PropertyInfo oProperty,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oProperty, bFromInheritance);
        }

        #endregion

        #region public static Attribute[] GetPropertyAttributes()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetProperty(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            PropertyInfo oPropertyInfo
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oPropertyInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetPropertyAttributes<AttributeType>(
            PropertyInfo oPropertyInfo,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oPropertyInfo, bFromInheritance);
        }

        #endregion

        #endregion

        #region Field

        #region public static FieldInfo GetField()

        /*
        /// <summary>Nullable</summary>
        public static FieldInfo GetField(Object oObject, String sName)
        {
            return GetField(oObject, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static FieldInfo GetField(Object oObject, String sName, BindingFlags eBindingFlags)
        {
            return GetField(oObject, sName, eBindingFlags);
        }*/

        /// <summary>Nullable</summary>
        public static FieldInfo GetField(Type oType, String sName)
        {
            return GetField(oType, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static FieldInfo GetField(Type oType, String sName, BindingFlags eBindingFlags)
        {
            if (oType != null && !String.IsNullOrWhiteSpace(sName))
                try
                {
                    return oType.GetField(sName, eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static Attribute GetFieldAttribute()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetField(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            FieldInfo oFieldInfo
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oFieldInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetFieldAttribute<AttributeType>(
            FieldInfo oFieldInfo,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oFieldInfo, bFromInheritance);
        }

        #endregion

        #region public static Attribute[] GetFieldAttributes()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetField(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            FieldInfo oFieldInfo
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oFieldInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetFieldAttributes<AttributeType>(
            FieldInfo oFieldInfo,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oFieldInfo, bFromInheritance);
        }

        #endregion

        #endregion

        #region Method

        #region public static MethodInfo GetMethod()

        /*
        /// <summary>Nullable</summary>
        public static MethodInfo GetMethod(Object oObject, String sName)
        {
            return GetMethod(oObject, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static MethodInfo GetMethod(Object oObject, String sName, BindingFlags eBindingFlags)
        {
            return GetMethod(oObject, sName, eBindingFlags);
        }*/

        /// <summary>Nullable</summary>
        public static MethodInfo GetMethod(Type oType, String sName)
        {
            return GetMethod(oType, sName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public);
        }

        /// <summary>Nullable</summary>
        public static MethodInfo GetMethod(Type oType, String sName, BindingFlags eBindingFlags)
        {
            if (oType != null && !String.IsNullOrWhiteSpace(sName))
                try
                {
                    return oType.GetMethod(sName, eBindingFlags);
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region public static Attribute GetMethodAttribute()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(GetMethod(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            MethodInfo oMethodInfo
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oMethodInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMethodAttribute<AttributeType>(
            MethodInfo oMethodInfo,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oMethodInfo, bFromInheritance);
        }

        #endregion

        #region public static Attribute[] GetMethodAttributes()

        /*
        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Object oObject,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oObject, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Object oObject,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oObject, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oObject, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Object oObject,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oObject, sName, eBindingFlags), bFromInheritance);
        }*/

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Type oType,
            String sName
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oType, sName), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Type oType,
            String sName,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oType, sName), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oType, sName, eBindingFlags), false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            Type oType,
            String sName,
            BindingFlags eBindingFlags,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(GetMethod(oType, sName, eBindingFlags), bFromInheritance);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            MethodInfo oMethodInfo
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oMethodInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType[] GetMethodAttributes<AttributeType>(
            MethodInfo oMethodInfo,
            Boolean bFromInheritance
        )
            where AttributeType : Attribute
        {
            return GetMemberAttributes<AttributeType>(oMethodInfo, bFromInheritance);
        }

        #endregion

        #endregion

        #region private static Attribute GetMemberAttribute()

        /// <summary>Nullable</summary>
        public static AttributeType GetMemberAttribute<AttributeType>( MemberInfo oMemberInfo ) where AttributeType : Attribute
        {
            return GetMemberAttribute<AttributeType>(oMemberInfo, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetMemberAttribute<AttributeType>( MemberInfo oMemberInfo, Boolean bFromInheritance ) where AttributeType : Attribute
        {
            if (oMemberInfo != null)
                try
                {
                    return oMemberInfo.GetCustomAttribute(typeof(AttributeType), bFromInheritance) as AttributeType;
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #region private static Attribute[] GetMemberAttributes()

        /// <summary>Nullable</summary>
        private static AttributeType[] GetMemberAttributes<AttributeType>(MemberInfo oMemberInfo, Boolean bFromInheritance) where AttributeType : Attribute
        {
            if (oMemberInfo != null)
                try
                {
                    return oMemberInfo.GetCustomAttributes(typeof(AttributeType), bFromInheritance) as AttributeType[];
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #endregion

        #region Class

        #region public static Attribute GetClassAttribute()

        /// <summary>Nullable</summary>
        public static AttributeType GetClassAttribute<AttributeType>( Type oType ) where AttributeType : Attribute
        {
            return GetClassAttribute<AttributeType>(oType, false);
        }

        /// <summary>Nullable</summary>
        public static AttributeType GetClassAttribute<AttributeType>(Type oType, Boolean bFromInheritance) where AttributeType : Attribute
        {
            if (oType != null)
                try
                {
                    return oType.GetCustomAttribute(typeof(AttributeType), bFromInheritance) as AttributeType;
                }
                catch
                {
                }

            return null;
        }

        #endregion

        #endregion

        #region public static Object ChangeType()

        /// <summary>Nullable</summary>
        public static Object ChangeType(Object oObject, Type oNewType)
        {
            if (oObject != null && oNewType != null)
            {
                if (oNewType.IsEnum)
                    try { return EnumUtils.ParseFrom(oNewType, oObject); } catch { }
                else
                    try { return Convert.ChangeType(oObject, oNewType); } catch { }
            }

            return null;
        }

        #endregion

        #region public static ObjectType NewFrom<ObjectType>()

        /// <summary>Nullable</summary>
        public static ObjectType NewFrom<ObjectType>(
           DataRow oDataRow
        )
            where ObjectType : new()
        {
            return NewFrom<ObjectType>(
                oDataRow,
                new Dictionary<String, String>(),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static
            );
        }

        /// <summary>Nullable</summary>
        public static ObjectType NewFrom<ObjectType>(
           DataRow oDataRow,
           BindingFlags eBindingFlags
        )
            where ObjectType : new()
        {
            return NewFrom<ObjectType>(
                oDataRow,
                new Dictionary<String, String>(),
                eBindingFlags
            );
        }

        /// <summary>Nullable</summary>
        public static ObjectType NewFrom<ObjectType>(
           DataRow oDataRow,
           Dictionary<String, String> dDRColumnsNames2OMembersNames
        )
            where ObjectType : new()
        {
            return NewFrom<ObjectType>(
                oDataRow,
                dDRColumnsNames2OMembersNames,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static
            );
        }

        /// <summary>Nullable</summary>
        public static ObjectType NewFrom<ObjectType>(
            DataRow oDataRow, 
            Dictionary<String, String> dDRColumnsNames2OMembersNames, 
            BindingFlags eBindingFlags
        ) 
            where ObjectType : new()
        {
            if (
                oDataRow == null
                || oDataRow.Table == null
                || oDataRow.Table.Columns == null
                || dDRColumnsNames2OMembersNames == null
            )
                return default(ObjectType);

            ObjectType
                oObject = new ObjectType();

            Type 
                oOType = typeof(ObjectType);

            foreach (DataColumn oDRTColumn in oDataRow.Table.Columns)
            {
                if (
                    oDRTColumn == null 
                    || String.IsNullOrEmpty(oDRTColumn.ColumnName)
                )
                    continue;

                String sMemberName;
                dDRColumnsNames2OMembersNames.TryGetValue(oDRTColumn.ColumnName, out sMemberName);

                if (String.IsNullOrEmpty(sMemberName))
                    sMemberName = oDRTColumn.ColumnName;

                PropertyInfo oPropertyInfo = null;

                try { oPropertyInfo = GetProperty(oOType, sMemberName, eBindingFlags); } catch { }

                if (oPropertyInfo != null)
                {
                    if (oPropertyInfo.SetMethod != null)
                        try { oPropertyInfo.SetValue(oObject, ChangeType(oDataRow[oDRTColumn.ColumnName], oPropertyInfo.PropertyType)); } catch { }

                    continue;
                }

                FieldInfo oFieldInfo = null;

                try { oFieldInfo = GetField(oOType, sMemberName, eBindingFlags); } catch { }

                if (oFieldInfo != null)
                    try { oFieldInfo.SetValue(oObject, ChangeType(oDataRow[oDRTColumn.ColumnName], oFieldInfo.FieldType)); } catch { }
            }

            return oObject;
        }

        #endregion
    }
}