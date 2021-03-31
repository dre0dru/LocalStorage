using System.IO;
using System.Threading.Tasks;

namespace LocalStorage.Providers
{
    public class FileProvider : IFileProvider
    {
        public void Write(byte[] output, string filePath)
        {
            File.WriteAllBytes(filePath, output);
        }

        public async Task WriteAsync(byte[] output, string filePath)
        {
            using var fileStream =
                new FileStream(
                    filePath,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    4096, true);

            await fileStream.WriteAsync(output, 0, output.Length);
        }

        public byte[] Read(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public async Task<byte[]> ReadAsync(string filePath)
        {
            using var fileStream =
                new FileStream(
                    filePath,
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    4096, true);


            var buffer = new byte[fileStream.Length];
            //Assuming data length is < int.MaxValue
            await fileStream.ReadAsync(buffer, 0, buffer.Length);

            return buffer;
        }
    }
}