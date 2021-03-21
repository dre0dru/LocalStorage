using System.IO;

namespace LocalStorage.Providers
{
    public class FileProvider : IFileProvider
    {
        public void Write(byte[] output, string filePath)
        {
            File.WriteAllBytes(filePath, output);
        }

        public byte[] Read(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}