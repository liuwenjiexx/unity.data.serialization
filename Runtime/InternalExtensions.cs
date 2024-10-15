using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Serialization
{
    internal static class InternalExtensions
    {
        #region Type

        internal static object DefaultValue(this Type type)
        {
            if (type == null || type == typeof(string))
                return null;
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        internal static object CreateInstance(this Type type)
        {
            if (type == typeof(string))
                return string.Empty;
            return Activator.CreateInstance(type);
        }

        internal static Type FindGenericTypeDefinition(this Type type, Type genericTypeDefinition)
        {
            Type result = null;

            if (genericTypeDefinition.IsInterface)
            {
                foreach (var t in type.GetInterfaces())
                {
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == genericTypeDefinition)
                    {
                        result = t;
                        break;
                    }
                }
            }
            else
            {
                Type t = type;
                while (t != null)
                {
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == genericTypeDefinition)
                    {
                        result = t;
                        break;
                    }
                    t = t.BaseType;
                }
            }
            return result;
        }

        #endregion

        public static bool IsIndexer(this PropertyInfo property)
        {
            if (property.GetIndexParameters().Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
