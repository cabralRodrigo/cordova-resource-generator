using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.Service.Properties;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace CordovaResourceGenerator.Service
{
    //TODO: Comment this.

    public class ImageGenerationService : IImageGenerationService
    {
        public void Generate(string outputFolder, string iconSource, string splashSource, Color backgroundColor, params Domain.Model.Platform[] platforms)
        {
            if (string.IsNullOrEmpty(outputFolder))
                throw new ArgumentNullException(nameof(outputFolder));

            if (string.IsNullOrEmpty(iconSource))
                throw new ArgumentNullException(nameof(iconSource));

            if (string.IsNullOrEmpty(splashSource))
                throw new ArgumentNullException(nameof(splashSource));

            if (platforms == null)
                throw new ArgumentNullException(nameof(platforms));

            if (!platforms.Any())
                throw new ArgumentOutOfRangeException(Resources.ImageGenerationService_GenerateIconsAndSplashs_EmptyPlatforms, nameof(platforms));

            if (!Directory.Exists(outputFolder))
                throw new ArgumentException(string.Format(Resources.ImageGenerationService_GenerateIconsAndSplashs_OutputNotFound, outputFolder), nameof(outputFolder));

            if (!File.Exists(iconSource))
                throw new ArgumentException(string.Format(Resources.ImageGenerationService_GenerateIconsAndSplashs_FileNotFound, iconSource), nameof(iconSource));

            if (!File.Exists(splashSource))
                throw new ArgumentException(string.Format(Resources.ImageGenerationService_GenerateIconsAndSplashs_FileNotFound, splashSource), nameof(splashSource));

            using (var icon = Image.FromFile(iconSource))
            using (var splash = Image.FromFile(splashSource))
                foreach (var platform in platforms)
                {
                    var platformFolder = Path.Combine(outputFolder, platform.Name);
                    if (!Directory.Exists(platformFolder))
                        Directory.CreateDirectory(platformFolder);

                    this.GenerateIcon(platformFolder, backgroundColor, icon, platform);
                    this.GenerateSplash(platformFolder, backgroundColor, splash, platform);
                }
        }

        private void GenerateIcon(string outputFolder, Color backgroundColor, Image iconSource, Domain.Model.Platform platform)
        {
            var iconFolder = Path.Combine(outputFolder, "icon");
            if (!Directory.Exists(iconFolder))
                Directory.CreateDirectory(iconFolder);

            foreach (var icon in platform.Icons)
                this.GenerateImage(iconSource, backgroundColor, icon, iconFolder);
        }

        private void GenerateSplash(string outputFolder, Color backgroundColor, Image splashSource, Domain.Model.Platform platform)
        {
            var splashFolder = Path.Combine(outputFolder, "splash");
            if (!Directory.Exists(splashFolder))
                Directory.CreateDirectory(splashFolder);

            foreach (var splash in platform.Splashs)
                this.GenerateImage(splashSource, backgroundColor, splash, splashFolder);
        }

        private void GenerateImage(Image sourceImage, Color backgroundColor, Domain.Model.ImageProperty property, string outputFolder)
        {
            using (var source = this.GetSourceWithCorrectSize(sourceImage, property))
            using (var dest = new Bitmap(property.Width, property.Height))
            using (var graphics = Graphics.FromImage(dest))
            {
                var sourceCenter = new Point(source.Width / 2, source.Height / 2);
                var center = new Point(dest.Width / 2, dest.Height / 2);

                graphics.Clear(backgroundColor);

                var drawCenter = new Point(center.X - sourceCenter.X, center.Y - sourceCenter.Y);
                graphics.DrawImage(source, drawCenter);

                graphics.Save();
                dest.Save(Path.Combine(outputFolder, property.Name));
            }
        }

        private Image GetSourceWithCorrectSize(Image sourceImage, Domain.Model.ImageProperty property)
        {
            if (sourceImage.Width > property.Width || sourceImage.Height > property.Height)
            {
                var newSize = ResizeKeepAspect(new Size(sourceImage.Width, sourceImage.Height), property.Width, property.Height);
                return ResizeImage((Image)sourceImage.Clone(), newSize.Width, newSize.Height);
            }
            else
                return (Image)sourceImage.Clone();
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Size ResizeKeepAspect(Size currentSize, int maxWidth, int maxHeight)
        {
            var newHeight = currentSize.Height;
            var newWidth = currentSize.Width;

            if (maxWidth > 0 && newWidth > maxWidth) //WidthResize
            {
                var divider = Math.Abs(newWidth / (Decimal)maxWidth);
                newWidth = maxWidth;
                newHeight = (int)Math.Round(newHeight / divider);
            }

            if (maxHeight > 0 && newHeight > maxHeight) //HeightResize
            {
                var divider = Math.Abs(newHeight / (Decimal)maxHeight);
                newHeight = maxHeight;
                newWidth = (int)Math.Round(newWidth / divider);
            }

            return new Size(newWidth, newHeight);
        }
    }
}