using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.Service.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CordovaResourceGenerator.Service
{
    /// <summary>
    /// Service to all logic related to the platform android.
    /// </summary>
    public class AndroidService : IAndroidService
    {
        /// <summary>
        /// The dictionary relating the icon density to size on the android platform.
        /// Source: https://cordova.apache.org/docs/en/latest/config_ref/images.html#android
        /// </summary>
        private static readonly IDictionary<string, Size> AndroidDensityIconSizes = new Dictionary<string, Size>
        {
            ["ldpi"] = new Size(36, 36),
            ["mdpi"] = new Size(48, 48),
            ["hdpi"] = new Size(72, 72),
            ["xhdpi"] = new Size(96, 96),
            ["xxhdpi"] = new Size(144, 144),
            ["xxxhdpi"] = new Size(192, 192)
        };

        /// <summary>
        /// The dictionary relating the splash density to size on the android platform.
        /// Source: https://github.com/phonegap/phonegap/wiki/App-Splash-Screen-Sizes
        /// </summary>
        private static readonly IDictionary<string, Size> AndroidDensitySplashSizes = new Dictionary<string, Size>
        {
            ["ldpi"] = new Size(200, 320),
            ["mdpi"] = new Size(320, 480),
            ["hdpi"] = new Size(480, 800),
            ["xhdpi"] = new Size(720, 1280),
            ["xxhdpi"] = new Size(960, 1600),
            ["xxxhdpi"] = new Size(1280, 1920)
        };

        /// <summary>
        /// Converts an android icon density to actual size (width and height).
        /// </summary>
        /// <param name="density">The density to convert.</param>
        /// <returns>The size.</returns>
        public Size ConvertAndroidIconDensityToSize(string density)
        {
            return this.ConvertAndroidDensityToSize(AndroidService.AndroidDensityIconSizes, density);
        }

        /// <summary>
        /// Converts an android splash density to actual size (width and height).
        /// </summary>
        /// <param name="density">The density to convert.</param>
        /// <returns>The size.</returns>
        public Size ConvertAndroidSplashDensityToSize(string density)
        {
            return this.ConvertAndroidDensityToSize(AndroidService.AndroidDensitySplashSizes, density);
        }

        /// <summary>
        /// Converts and android icon or splash density to actual size (width and heiht).
        /// </summary>
        /// <param name="sizeDictionary">The density-to-size dictionary.</param>
        /// <param name="densityString">The density to convert.</param>
        /// <returns>The size of the icon or splash.</returns>
        private Size ConvertAndroidDensityToSize(IDictionary<string, Size> sizeDictionary, string densityString)
        {
            if (string.IsNullOrEmpty(densityString))
                throw new ArgumentNullException(nameof(densityString));

            var parts = densityString.Split('-');
            string orientation, density;

            //Gets the orientation and the actual density.
            switch (parts.Length)
            {
                case 1:
                    orientation = string.Empty;
                    density = parts[0];
                    break;
                case 2:
                    orientation = parts[0];
                    density = parts[1];
                    break;
                default:
                    throw new Exception(string.Format(Resources.CordovaProjectService_ExtractPlatform_DensityNotValid, densityString));
            }

            orientation = orientation?.Trim()?.ToLower();
            density = density?.Trim()?.ToLower();

            //Throws an exception if the orientation is different of 'land' or 'port'.
            if (orientation != string.Empty && (orientation != "land" && orientation != "port"))
                throw new Exception(string.Format(Resources.AndroidService_ConvertAndroidDensityToSize_InvalidOrientation, orientation));

            //Throws an exception if the density isn't unknown.
            if (!sizeDictionary.ContainsKey(density))
                throw new Exception(string.Format(Resources.AndroidService_ConvertAndroidDensityToSize_InvalidDensity, density));

            //Gets the actual size.
            var size = sizeDictionary[density];

            //Returns the size and reverses it if the orientation is 'land'.
            if (orientation == "land")
                return new Size(size.Height, size.Width);
            else
                return size;
        }
    }
}