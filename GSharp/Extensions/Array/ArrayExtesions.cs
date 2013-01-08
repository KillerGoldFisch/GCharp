using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GSharp.Extensions.Array {
    public static class ArrayExtesions {
        public static T[] SubArray<T>(this T[] data, int index, int length) {
            T[] result = new T[length];
            System.Array.Copy(data, index, result, 0, length);
            return result;
        }
        public static T[] SubArrayDeepClone<T>(this T[] data, int index, int length) {
            T[] arrCopy = new T[length];
            System.Array.Copy(data, index, arrCopy, 0, length);
            using (MemoryStream ms = new MemoryStream()) {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, arrCopy);
                ms.Position = 0;
                return (T[])bf.Deserialize(ms);
            }
        }
    }
}
