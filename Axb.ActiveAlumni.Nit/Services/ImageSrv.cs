using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Axb.ActiveAlumni.Nit.ViewModels;

namespace Axb.ActiveAlumni.Nit.Services
{
    public class ImageSrv
    {
        public static ImageResult ThumbnailCrop(Image srcImage, int width, int height)
        {
            var newHeight = srcImage.Height * width / srcImage.Width;
            using (var newImage = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, width, newHeight));
                newImage.Save(stream, ImageFormat.Png);
                return new ImageResult(stream.ToArray(), "image/png");
            }
        }

        public static ImageResult Thumbnail(Image srcImage, int width, int height)
        {
            var newHeight = srcImage.Height * width / srcImage.Width;
            using (var newImage = new Bitmap(width, newHeight))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, width, newHeight));
                newImage.Save(stream, ImageFormat.Png);
                return new ImageResult(stream.ToArray(), "image/png");
            }
        }
    }
}