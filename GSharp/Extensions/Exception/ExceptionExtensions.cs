using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Extensions.ExceptionEx {
    public static class ExceptionExtensions {
        private static List<int> cachedExeptions = new List<int>();

        public static bool IsCached(this Exception @thisX) {
            int hashKey = @thisX.ToString().GetHashCode();
            if (cachedExeptions.Contains(hashKey))
                return true;

            cachedExeptions.Add(hashKey);
            return false;
        }
    }
}
