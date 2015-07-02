using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

namespace ImageConvertor
{
    class Program
    {
        public Bitmap ConvertToGrayscale(Bitmap source)
        {
            Bitmap bm = new Bitmap(source.Width, source.Height);
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = source.GetPixel(x, y);
                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bm.SetPixel(x, y, Color.FromArgb(luma, luma, luma));
                }
            }
            return bm;
        }

        public void ConvertToXBM(Bitmap source, String fileName)
        {
            String xbmContent = "#define image_width " + source.Width + "\r\n";
            xbmContent = xbmContent + "#define image_height " + source.Height + "\r\n";
            xbmContent = xbmContent + "static char image_bits[] = {\r\n";
            Bitmap bm = new Bitmap(source.Width, source.Height);
            StringBuilder hex = new StringBuilder(source.Width * source.Height * 8);
            BitArray bitArray = new BitArray(source.Width * source.Height);
            int index = 0;

            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    Color c = source.GetPixel(x, y);
                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bitArray[index] = (luma < 128);
                    index++;
                }
            }

            int byteLength = bitArray.Length / 8;

            if (bitArray.Length % 8 != 0)
            {
                byteLength = byteLength + 1;
            }

            Byte[] bytes = new byte[byteLength];
            bitArray.CopyTo(bytes, 0);
            index = 0;

            foreach (byte b in bytes)
            {
                hex.AppendFormat("0x{0:x2}, ", b);
                if ((index = index + 1) % 12 == 0)
                {
                    hex.Append("\r\n");
                }
            }

            xbmContent = xbmContent + hex.ToString() + "};";
            File.WriteAllText(fileName, xbmContent);
        }
        static void Main(string[] args)
        {
            Image i = Image.FromFile(@"E:\Images\Microsoft.jpg");
            Bitmap bitImage = new Bitmap(i);
            new Program().ConvertToXBM(bitImage, @"E:\Images\Abc.xbm");            
        }
    }
}
