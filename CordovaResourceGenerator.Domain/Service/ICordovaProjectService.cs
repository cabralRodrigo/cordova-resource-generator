using CordovaResourceGenerator.Domain.Model;

namespace CordovaResourceGenerator.Domain.Service
{
    /// <summary>
    /// Service to all logic related a cordovo project interaction.
    /// </summary>
    public interface ICordovaProjectService
    {
        /// <summary>
        /// Extract all platforms information from a cordova project xml file.
        /// </summary>
        /// <param name="configFullPath">The full path for a cordova project xml file.</param>
        /// <returns>All platforms in the cordova project xml file, if any.</returns>
        Platform[] ExtractPlatforms(string configFullPath);
    }
}