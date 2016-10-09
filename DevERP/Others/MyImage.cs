using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace DevERP.Others
{
    public class MyImage
    {
        public static string ImagePath()
        {
            //return AppDomain.CurrentDomain.BaseDirectory+"Image\\";
            return @"Image\";
        }
        public bool IsImageType(string ext)
        {
            switch (ext.ToLower())
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                case ".bmp":
                    return true;
            }
            return false;
        }

        public string GetExtention(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public byte[] ConvertImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                //Double xRatio = (double)img.Width / maxWidth;
                //Double yRatio = (double)img.Height / maxHeight;
                //Double ratio = Math.Max(xRatio, yRatio);
                //int nnx = (int)Math.Floor(img.Width / ratio);
                //int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(maxWidth, maxHeight, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, maxWidth, maxHeight),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }

    }
}