using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSharp.Extensions.String {
    public static class StringExtensions {
        public static byte[] ToByteArray(this string data) {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
