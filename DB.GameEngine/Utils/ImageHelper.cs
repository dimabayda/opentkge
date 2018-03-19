using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DB.GameEngine.Utils
{
    public class ImageHelper
    {
        public static float[] ConvertToArray(Image image)
        {
            float[] r;
            using (var bmp = (Bitmap)image)
            {
                int width = bmp.Width;
                int height = bmp.Height;
                r = new float[width * height * 4];
                int index = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        r[index++] = pixel.R / 255f;
                        r[index++] = pixel.G / 255f;
                        r[index++] = pixel.B / 255f;
                        r[index++] = pixel.A / 255f;
                    }
                }
            }
            return r;
        }
    }
}
