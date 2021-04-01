using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IFileProvider
    {
        void Write(byte[] output, string fileName);

        Task WriteAsync(byte[] output, string fileName);

        byte[] Read(string fileName);

        Task<byte[]> ReadAsync(string fileName);
        
        bool Delete(string fileName);

        string GetFilePath(string fileName);

        bool FileExists(string fileName);
    }
}