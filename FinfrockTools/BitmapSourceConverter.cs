﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace FinfrockTools
{
    public static class BitmapSourceConverter
    {
        public enum ImageType
        {
            Small,
            Large
        }

        public static ImageSource ToImageSource(Bitmap bitmap, ImageType imageType)
        {
            switch (imageType)
            {
                case ImageType.Small:
                    return ToImageSource(bitmap).Resize(16);

                case ImageType.Large:
                    return ToImageSource(bitmap).Resize(32);

                default:
                    throw new ArgumentOutOfRangeException(nameof(imageType), imageType, null);
            }
        }

#pragma warning disable CA1416
        public static BitmapImage ToImageSource(Bitmap bitmap)
        {
            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
#pragma warning restore CA1416

        /// <summary>
        /// Resize ImageResource
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ImageSource Resize(this ImageSource imageSource, int size)
        {
            return Thumbnail(imageSource, size);
        }

        private static ImageSource Thumbnail(ImageSource source, int size)
        {
            var rect = new Rect(0, 0, size, size);
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(source, rect);
            }
            var resizedImage = new RenderTargetBitmap((int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            resizedImage.Render(drawingVisual);

            return resizedImage;
        }
    }
}
