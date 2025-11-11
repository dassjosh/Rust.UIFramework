using System;
using System.Reflection;
using System.Text;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

/// <summary>
/// Extensions for <see cref="Type"/>
/// </summary>
internal static class TypeExt
{
    /// <param name="type">Type to check</param>
    extension(Type type)
    {
        /// <summary>
        /// Returns if the type is <see cref="Nullable"/>
        /// </summary>
        /// <returns>True if type is <see cref="Nullable"/>; false otherwise</returns>
        public bool IsNullable => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        /// <summary>
        /// Returns if the type is <see cref="Nullable"/>
        /// </summary>
        /// <returns>True if type is <see cref="Nullable"/>; false otherwise</returns>
        public Type GetNullableType() => Nullable.GetUnderlyingType(type);

        /// <summary>
        /// Returns if a type is a value type
        /// </summary>
        /// <returns></returns>
        public bool IsValueType => type.IsValueType;

        /// <summary>
        /// Returns the default value for <see cref="Type"/>
        /// </summary>
        /// <returns>default value for <see cref="Type"/></returns>
        public object GetDefault() => type.IsValueType ? Activator.CreateInstance(type) : null;

        internal T GetAttribute<T>(bool inherit) where T : Attribute => type.GetCustomAttribute(typeof(T), inherit) as T;

        internal bool HasAttribute<T>(bool inherit) where T : Attribute => GetAttribute<T>(type, inherit) != null;

        public string GetRealTypeName()
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }

            StringBuilder sb = UiPool.Internal.GetStringBuilder();
            sb.Append(type.Name.AsSpan()[..type.Name.IndexOf('`')]);
            sb.Append('<');
            Type[] args = type.GetGenericArguments();
            for (int index = 0; index < args.Length; index++)
            {
                Type arg = args[index];
                if (index != 0)
                {
                    sb.Append(',');
                }
                sb.Append(GetRealTypeName(arg));
            }
            sb.Append('>');
            return UiPool.Internal.ToStringAndFree(sb);
        }

        public TypeCode GetTypeCode()
        {
            if (type == null)
            {
                return TypeCode.Empty;
            }

            // Handle nullable types
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            return Type.GetTypeCode(type);
        }

        public bool IsNumericType()
        {
            switch (type.GetTypeCode())
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}