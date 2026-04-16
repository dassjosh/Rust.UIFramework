using System;
using System.Reflection;
using System.Text;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

public static class MethodInfoExt
{
    extension(MethodInfo info)
    {
        public string GetMethodWithParams()
        {
            StringBuilder sb = UiPool.Internal.GetStringBuilder();
            sb.Append(info.Name);
            sb.Append('(');

            ParameterInfo[] parameters = info.GetParameters();
            for (int index = 0; index < parameters.Length; index++)
            {
                ParameterInfo parameter = parameters[index];
                if (index != 0)
                {
                    sb.Append(", ");
                }

                sb.Append(parameter.ParameterType.Name);
                sb.Append(' ');
                sb.Append(parameter.Name);
            }

            sb.Append(')');
            return UiPool.Internal.ToStringAndFree(sb);
        }

        internal T GetAttribute<T>(bool inherit) where T : Attribute => info.GetCustomAttribute(typeof(T), inherit) as T;
    }
}