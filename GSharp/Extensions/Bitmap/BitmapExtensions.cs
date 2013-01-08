using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GSharp.Extensions.Bitmap {
    public static class BitmapExtensions {
        public static System.Drawing.Bitmap Resize(this System.Drawing.Bitmap b, int nWidth, int nHeight) {
            System.Drawing.Bitmap result = new System.Drawing.Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
        }
    }
}
