using System;
using System.IO;

namespace GSharp.Extensions.Array.BytaArray
{
    public static class BytaArrayExtension
    {
        public static void SaveToFile(this Byte[] data, string filename, FileMode mode = FileMode.Create)
        {
            FileStream fs = new FileStream(filename, mode);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
