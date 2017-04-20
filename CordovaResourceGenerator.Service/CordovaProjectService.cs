using CordovaResourceGenerator.Domain.Model;
using CordovaResourceGenerator.Domain.Service;
using CordovaResourceGenerator.Service.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace CordovaResourceGenerator.Service
{
    /// <summary>
    /// Service to all logic related a cordovo project interaction.
    /// </summary>
    public class CordovaProjectService : ICordovaProjectService
    {
        /// <summary>
        /// The w3c's widget xml namespace.
        /// </summary>
        private const string XmlWidgetNamespaceUri = "http://www.w3.org/ns/widgets";

        /// <summary>
        /// The w3c's widtget prefix.
        /// </summary>
        private const string XmlWidgetNamespacePrefix = "widgets";

        /// <summary>
        /// The android service.
        /// </summary>
        private readonly IAndroidService androidService;

        /// <summary>
        /// Creates a new instance of the <see cref="CordovaProjectService"/> class.
        /// </summary>
        /// <param name="androidService">The android service.</param>
        public CordovaProjectService(IAndroidService androidService)
        {
            this.androidService = androidService;
        }

        /// <summary>
        /// Extract all platforms information from a cordova project xml file.
        /// </summary>
        /// <param name="configFullPath">The full path for a cordova project xml file.</param>
        /// <returns>All platforms in the cordova project xml file, if any.</returns>
        public Platform[] ExtractPlatforms(string configFullPath)
        {
            if (string.IsNullOrEmpty(configFullPath))
                throw new ArgumentNullException(nameof(configFullPath));

            if (!File.Exists(configFullPath))
                throw new ArgumentException(string.Format(Resources.CordovaProjectService_ExtractPlatforms_FileNotFound, configFullPath), nameof(configFullPath));

            //Loads the cordova project xml.
            var xml = new XmlDocument();
            xml.Load(configFullPath);

            //Adds the w3c's widget namespace.
            var manager = new XmlNamespaceManager(xml.NameTable);
            manager.AddNamespace(XmlWidgetNamespacePrefix, XmlWidgetNamespaceUri);

            //Selects all the platform tags on the xml.
            var platformNodes = xml.SelectNodes($"//{XmlWidgetNamespacePrefix}:platform", manager);

            //Process each platform and returns the result.
            return platformNodes.Cast<XmlNode>().Select(s => this.ExtractPlatform(s, manager)).ToArray();
        }

        /// <summary>
        /// Extract all the platform information from a xml node.
        /// </summary>
        /// <param name="xml">The xml node to extract the platform's information.</param>
        /// <param name="manager">The namespace manager of the xml.</param>
        /// <returns>The extracted platform.</returns>
        private Platform ExtractPlatform(XmlNode xml, XmlNamespaceManager manager)
        {
            var name = xml.Attributes?["name"]?.Value;

            if (string.IsNullOrEmpty(name))
                throw new Exception(Resources.CordovaProjectService_ExtractPlatform_PlatformNameNotFound);

            var doc = new XmlDocument();
            doc.LoadXml(xml.OuterXml);

            //Selects all icons and splashs tags.
            var icons = doc.SelectNodes($"//{XmlWidgetNamespacePrefix}:icon", manager);
            var splashs = doc.SelectNodes($"//{XmlWidgetNamespacePrefix}:splash", manager);

            //Process all the icons and splashs and return the result.
            return new Platform
            {
                Name = name,
                Icons = this.ExtractIconAndSplash(this.androidService.ConvertAndroidIconDensityToSize, icons, manager).ToArray(),
                Splashs = this.ExtractIconAndSplash(this.androidService.ConvertAndroidSplashDensityToSize, splashs, manager).ToArray()
            };
        }

        /// <summary>
        /// Extracts all the icon or splash information from a list of xml nodes.
        /// </summary>
        /// <param name="convertDensityToSize">The function to convert density to icon or splash size.</param>
        /// <param name="nodes">The xml node list.</param>
        /// <param name="manager">The namespace manager for xml node list.</param>
        /// <returns>The extracted images properties.</returns>
        private IEnumerable<ImageProperty> ExtractIconAndSplash(Func<string, Size> convertDensityToSize, XmlNodeList nodes, XmlNamespaceManager manager)
        {
            foreach (var node in nodes.Cast<XmlNode>())
            {
                Size size;
                var densityString = node.Attributes?["density"]?.Value;
                var srcString = node.Attributes?["src"]?.Value?.Trim();

                if (string.IsNullOrEmpty(srcString))
                    throw new Exception(Resources.CordovaProjectService_ExtractIconAndSplash_NotFoundIconOrSplashSource);

                //Try to use the density to get the size first.
                if (!string.IsNullOrEmpty(densityString))
                    size = convertDensityToSize(densityString);
                else
                {
                    var widthString = node.Attributes?["width"]?.Value;
                    var heightString = node.Attributes?["height"]?.Value;

                    if (!int.TryParse(widthString, out int width))
                        throw new Exception(Resources.CordovaProjectService_ExtractIconAndSplash_InvalidWidth);

                    if (!int.TryParse(heightString, out int height))
                        throw new Exception(Resources.CordovaProjectService_ExtractIconAndSplash_InvalidHeight);

                    size = new Size(width, height);
                }

                yield return new ImageProperty
                {
                    Height = size.Height,
                    Width = size.Width,
                    Name = srcString.Split('\\', '/').Last()
                };
            }
        }
    }
}