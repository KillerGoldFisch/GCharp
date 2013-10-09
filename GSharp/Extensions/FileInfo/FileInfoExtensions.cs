using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GSharp.Extensions.Serializing;

namespace GSharp.Extensions.FileInfoEx {
    public static class FileInfoExtensions {

        public static void SetString(this FileInfo @thisX, string content) {
            FileStream fileStream = @thisX.OpenWrite();
            using(StreamWriter streamWriter = new StreamWriter(fileStream))
                streamWriter.Write(content);
        }

        public static string GetString(this FileInfo @thisX) {
            FileStream fileStream = @thisX.OpenRead();
            using (StreamReader streamReader = new StreamReader(fileStream))
                return streamReader.ReadToEnd();
        }

        public static void SetObject<T>(this FileInfo @thisX, T content, SerializingExtensions.Serializer serializer) {
            @thisX.SetString(content.Serialize(serializer));
        }

        public static T GetObject<T>(this FileInfo @thisX, SerializingExtensions.Serializer serializer) {
            return @thisX.GetString().Deserialize<T>(serializer);
        }

        public static void SetObjectBinary<T>(this FileInfo @thisX, T content) {
            @thisX.SetString(content.SerializeBinary());
        }

        public static T GetObjectBinary<T>(this FileInfo @thisX) {
            return @thisX.GetString().DeserializeBinary<T>();
        }

        public static void SetObjectSoap<T>(this FileInfo @thisX, T content) {
            @thisX.SetString(content.SerializeSoap());
        }

        public static T GetObjectSoap<T>(this FileInfo @thisX) {
            return @thisX.GetString().DeserializeSoap<T>();
        }

        public static void SetObjectXML<T>(this FileInfo @thisX, T content) {
            @thisX.SetString(content.SerializeXML());
        }

        public static T GetObjectXML<T>(this FileInfo @thisX) {
            return @thisX.GetString().DeserializeXML<T>();
        }
    }
}
