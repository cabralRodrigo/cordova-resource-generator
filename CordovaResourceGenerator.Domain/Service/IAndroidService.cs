using System.Drawing;

namespace CordovaResourceGenerator.Domain.Service
{
    /// <summary>
    /// Service to all logic related to the platform android.
    /// </summary>
    public interface IAndroidService
    {
        /// <summary>
        /// Converts an android icon density to actual size (width and height).
        /// </summary>
        /// <param name="density">The density to convert.</param>
        /// <returns>The size.</returns>
        Size ConvertAndroidIconDensityToSize(string density);

        /// <summary>
        /// Converts an android splash density to actual size (width and height).
        /// </summary>
        /// <param name="density">The density to convert.</param>
        /// <returns>The size.</returns>
        Size ConvertAndroidSplashDensityToSize(string density);
    }
}