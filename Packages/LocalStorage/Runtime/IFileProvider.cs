using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IFileProvider
    {
        void Write(byte[] output, string filePath);

        Task WriteAsync(byte[] output, string filePath);

        byte[] Read(string filePath);

        Task<byte[]> ReadAsync(string filePath);
    }
}