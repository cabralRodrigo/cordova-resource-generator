using CordovaResourceGenerator.Domain.Model;
using System.Drawing;

namespace CordovaResourceGenerator.Domain.Service
{
    public interface IImageGenerationService
    {
        void Generate(string outputFolder, string iconSource, string splashSource, Color backgroundColor, params Platform[] platforms);
    }
}