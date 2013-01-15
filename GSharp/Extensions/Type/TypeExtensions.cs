using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GSharp.Extensions.Type {
    /// <summary>
    /// Description of TypeExtensions.
    /// </summary>
    public static class TypeExtensions {
        public static bool IsRealySerializable(this System.Type type) {
            // base case
            if (type.IsValueType || type == typeof(string)) return true;

            //Assert.IsTrue(type.IsSerializable, type + " must be marked [Serializable]");
            if (!type.IsSerializable) return false;

            foreach (var propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                if (propertyInfo.PropertyType.IsGenericType) {
                    foreach (var genericArgument in propertyInfo.PropertyType.GetGenericArguments()) {
                        if (genericArgument == type) continue; // base case for circularly referenced properties
                        if (!genericArgument.IsRealySerializable()) return false;
                    }
                } else if (propertyInfo.GetType() != type) // base case for circularly referenced properties
                    if (!propertyInfo.PropertyType.IsRealySerializable()) return false;
            }

            return true;
        }
    }
}
