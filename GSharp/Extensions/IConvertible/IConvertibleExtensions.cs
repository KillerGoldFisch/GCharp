using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Extensions.IConvertible {
    public static class Extensions {
        public static T To<T>(this System.IConvertible obj) {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ToOrDefault<T>
                     (this System.IConvertible obj) {
            try {
                return To<T>(obj);
            } catch {
                return default(T);
            }
        }

        public static bool ToOrDefault<T>
                            (this System.IConvertible obj,
                             out T newObj) {
            try {
                newObj = To<T>(obj);
                return true;
            } catch {
                newObj = default(T);
                return false;
            }
        }

        public static T ToOrOther<T>
                               (this System.IConvertible obj,
                               T other) {
            try {
                return obj.To<T>();
            } catch {
                return other;
            }
        }

        public static bool ToOrOther<T>
                                 (this System.IConvertible obj,
                                 out T newObj,
                                 T other) {
            try {
                newObj = To<T>(obj);
                return true;
            } catch {
                newObj = other;
                return false;
            }
        }

        public static T ToOrNull<T>
                              (this System.IConvertible obj)
                              where T : class {
            try {
                return To<T>(obj);
            } catch {
                return null;
            }
        }

        public static bool ToOrNull<T>
                          (this System.IConvertible obj,
                          out T newObj)
                          where T : class {
            try {
                newObj = To<T>(obj);
                return true;
            } catch {
                newObj = null;
                return false;
            }
        }
    }
}
