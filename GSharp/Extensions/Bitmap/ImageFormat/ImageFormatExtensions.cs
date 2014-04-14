using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace GSharp.Extensions.Bitmap.ImageFormatEx {
    public static class ImageFormatExtensions {

        public static ImageCodecInfo GetEncoder(this System.Drawing.Imaging.ImageFormat format) {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }
    }
}
