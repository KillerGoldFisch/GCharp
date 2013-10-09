using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GSharp.Extensions.ObjectEx.Reflection {
    /// <summary>
    /// Description of ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions {
        public static System.Object InvokeMethod(this System.Object val, string MehodName, object[] Parameters) {
            MethodInfo method = val.GetType().GetMethod(MehodName);
            return method.Invoke(val, Parameters);
        }
        public static System.Object InvokeGenericMethod(this System.Object val, System.Type[] GenericTypes, string MethodName, object[] Parameters) {
            MethodInfo method = val.GetType().GetMethod(MethodName);
            method = method.MakeGenericMethod(GenericTypes);
            return method.Invoke(val, Parameters);
        }

        public static System.Object InvokeMethod(this System.Object val, string MehodName, System.Type[] methodTypes, object[] Parameters) {
            MethodInfo method = val.GetType().GetMethod(MehodName, methodTypes);
            return method.Invoke(val, Parameters);
        }

        public static System.Object InvokeGenericMethod(this System.Object val, System.Type[] GenericTypes, string MethodName, System.Type[] methodTypes, object[] Parameters) {
            MethodInfo method = val.GetType().GetMethod(MethodName, methodTypes);
            method = method.MakeGenericMethod(GenericTypes);
            return method.Invoke(val, Parameters);
        }

        public static System.Object InvokeGenericMethod(this System.Object val, string[] GenericTypes, string MethodName, object[] Parameters) {
            List<System.Type> types = new List<System.Type>();
            foreach (string type in GenericTypes)
                types.Add(System.Type.GetType(type));
            return val.InvokeGenericMethod(types.ToArray(), MethodName, Parameters);
        }

        public static System.Object InvokeGenericMethod(this System.Object val, string[] GenericTypes, string MethodName, System.Type[] methodTypes, object[] Parameters) {
            List<System.Type> types = new List<System.Type>();
            foreach (string type in GenericTypes)
                types.Add(System.Type.GetType(type));
            return val.InvokeGenericMethod(types.ToArray(), MethodName, methodTypes, Parameters);
        }

        public static object GetPrivateMember(this System.Object val, string Name) {
            FieldInfo fi = val.GetType().GetField(Name, BindingFlags.NonPublic | BindingFlags.Instance);
            return fi.GetValue(val);
        }

        public static string MakeGenericTypeString(string type, string[] genericTypes) {
            string returnString = type + "`" + genericTypes.Length.ToString() + "[";

            for (int n = 0; n < genericTypes.Length; n++)
                returnString += genericTypes[n] + (n < genericTypes.Length - 1 ? "," : "");

            returnString += "]";
            return returnString;
        }
    }
}
