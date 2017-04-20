using System.Diagnostics;

namespace CordovaResourceGenerator.Domain.Model
{
    /// <summary>
    /// Model of the platform's information.
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class Platform
    {
        /// <summary>
        /// The platform's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The platform's icons.
        /// </summary>
        public ImageProperty[] Icons { get; set; }

        /// <summary>
        /// The platform's splashs.
        /// </summary>
        public ImageProperty[] Splashs { get; set; }
    }
}