using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSharp.Extensions.Array;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace GSharp.Extensions.ObjectEx {
    public static class ObjectExtensions {
        public static byte[] Serialize(this System.Object objectToSerialize, ByteArrayExtensions.DataFormatType format) {
            MemoryStream ms = new MemoryStream();
            //try {
            switch (format) {
                case ByteArrayExtensions.DataFormatType.Binary:
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(ms, objectToSerialize);
                    break;
                case ByteArrayExtensions.DataFormatType.Soap:
                    SoapFormatter sFormatter = new SoapFormatter();
                    sFormatter.Serialize(ms, objectToSerialize);
                    break;
                case ByteArrayExtensions.DataFormatType.XML:
                    XmlSerializer xFormatter = new XmlSerializer(objectToSerialize.GetType());
                    xFormatter.Serialize(ms, objectToSerialize);
                    break;
            }
            //} catch (Exception ex) { }
            return ms.ToArray();
        }
    }
}
