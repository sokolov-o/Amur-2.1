using System.Collections.Generic;
using System.Reflection;

namespace SOV.Common
{
    public static class ObjectHandler
    {
        public static bool IsInsideArr<T>(object obj, List<T> arr)
        {
            return arr == null || arr.Contains((T)obj);
        }

        public static bool IsEqualToVal(object obj, object val)
        {
            return val == null || obj.Equals(val);
        }

        public static bool IsFieldEqualToVal<T>(object obj, string fieldName, object val)
        {
            return IsEqualToVal(FieldVal<T>(obj, fieldName), val);
        }

        public static bool IsFieldInsideArr<T, U>(object obj, string fieldName, List<U> arr)
        {
            return IsInsideArr<U>(FieldVal<T>(obj, fieldName), arr);
        }

        public static object FieldVal<T>(object obj, string fieldName)
        {
            if (obj == null || fieldName == null) return null;
            var field = typeof(T).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return field.GetValue(obj);
        }

        public static object MethodVal<T>(object obj, string methodName)
        {
            var method = typeof(T).GetMethod(methodName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            return method.Invoke(obj, null);
        }
    }
}
