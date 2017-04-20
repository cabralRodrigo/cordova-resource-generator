using System.Diagnostics;

namespace CordovaResourceGenerator.Domain.Model
{
    /// <summary>
    /// Model of the properties of a image.
    /// </summary>
    [DebuggerDisplay("{Name}: {Width}x{Height}")]
    public class ImageProperty
    {
        /// <summary>
        /// The image file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the image.
        /// </summary>
        public int Height { get; set; }
    }
}