using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using GSharp.Extensions.Bitmap.ImageFormatEx;
using System.Drawing.Imaging;

namespace GSharp.Extensions.BitmapEx {
    public static class BitmapExtensions {
        /// <summary>
        /// Erstellt ein neues Bild aus dem übergebenen
        /// und skaliert es.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap Resize(this System.Drawing.Bitmap b, int nWidth, int nHeight) {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }

        /// <summary>
        /// Konvertiert ein Bitmap zum JPG Format und gibt dessen Daten als Bytes zurück.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static Byte[] ToJpegBytes(this System.Drawing.Bitmap data, long quality)
        {
            MemoryStream ms = new MemoryStream();
            System.Drawing.Imaging.ImageCodecInfo jgpEncoder = System.Drawing.Imaging.ImageFormat.Jpeg.GetEncoder();
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            data.Save(ms, jgpEncoder, myEncoderParameters);
            Byte[] Data = ms.GetBuffer();
            data.Dispose();
            ms.Close();
            return Data;
        }
    }
}
