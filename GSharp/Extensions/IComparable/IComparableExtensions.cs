using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Extensions.IComparableEx {
    public static class IComparableExtensions {
        public static T FitInto<T>(this T value, T min, T max) where T : IComparable {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;
            return value;
        }
    }
}
