using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace slControls
{
    public class ExportContent
    {
        /// <summary>
        /// Static function Export(UIElement objUIElement, string imgExtenstion) is used to call from the silverlight page by sending objUIElement and imgExtenstion
        /// objUIElement is the Silver light UIElement 
        /// expoExtension is to which extension of image has to be exported
        /// </summary>
        /// <param name="objUIElement"></param>
        /// <param name="extension"></param>
        public static void Export(UIElement objUIElement, string imgExtenstion) 
        {

            // Select a location for saving the file
            SaveFileDialog saveDialog = new SaveFileDialog();

            // Set the current file name filter string that appear in the "Save as file  
            //saveDialog.Filter = "PNG (*.png)|*.png";
            saveDialog.Filter = imgExtenstion.ToUpper() + " (*." + imgExtenstion.ToLower() + ")|*." + imgExtenstion.ToLower();
            
            //Set the default file name extension.
            saveDialog.DefaultExt = "." + imgExtenstion.ToLower();
            if (saveDialog.ShowDialog() == true)
            {
                WriteableBitmap wb = new WriteableBitmap(objUIElement, null);
                using (Stream stream = saveDialog.OpenFile())
                {
                    byte[] buffer = GetBuffer(wb);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
        /// <summary>
        /// GetBuffer method expects WriteableBitmap object to convert into the byte array which can be printed to get an image.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private static byte[] GetBuffer(WriteableBitmap bitmap)
        {
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            MemoryStream ms = new MemoryStream();
            #region BMP File Header(14 bytes)
            //the magic number(2 bytes):BM
            ms.WriteByte(0x42);
            ms.WriteByte(0x4D);
            //the size of the BMP file in bytes(4 bytes)
            long len = bitmap.Pixels.Length * 4 + 0x36;
            ms.WriteByte((byte)len);
            ms.WriteByte((byte)(len >> 8));
            ms.WriteByte((byte)(len >> 16));
            ms.WriteByte((byte)(len >> 24));
            //reserved(2 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //reserved(2 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //the offset(4 bytes)
            ms.WriteByte(0x36);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            #endregion
            #region Bitmap Information(40 bytes:Windows V3)
            //the size of this header(4 bytes)
            ms.WriteByte(0x28);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //the bitmap width in pixels(4 bytes)
            ms.WriteByte((byte)width);
            ms.WriteByte((byte)(width >> 8));
            ms.WriteByte((byte)(width >> 16));
            ms.WriteByte((byte)(width >> 24));
            //the bitmap height in pixels(4 bytes)
            ms.WriteByte((byte)height);
            ms.WriteByte((byte)(height >> 8));
            ms.WriteByte((byte)(height >> 16));
            ms.WriteByte((byte)(height >> 24));
            //the number of color planes(2 bytes)
            ms.WriteByte(0x01);
            ms.WriteByte(0x00);
            //the number of bits per pixel(2 bytes)
            ms.WriteByte(0x20);
            ms.WriteByte(0x00);
            //the compression method(4 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //the image size(4 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //the horizontal resolution of the image(4 bytes)
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            //the vertical resolution of the image(4 bytes)
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            ms.WriteByte(0x01);
            //the number of colors in the color palette(4 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            //the number of important colors(4 bytes)
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            ms.WriteByte(0x00);
            #endregion
            #region Bitmap data
            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    int pixel = bitmap.Pixels[width * y + x];
                    ms.WriteByte((byte)(pixel & 0xff)); //B
                    ms.WriteByte((byte)((pixel >> 8) & 0xff)); //G
                    ms.WriteByte((byte)((pixel >> 0x10) & 0xff)); //R
                    ms.WriteByte(0x00); //reserved
                }
            }

            #endregion
            return ms.GetBuffer();
        }
    }
}
