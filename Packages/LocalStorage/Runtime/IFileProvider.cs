#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage
{
    public interface IFileProvider
    {
        void Write(byte[] output, string fileName);

        byte[] Read(string fileName);

        bool Delete(string fileName);

        string GetFilePath(string fileName);

        bool FileExists(string fileName);
    }

    public interface IFileProviderAsync : IFileProvider
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask WriteAsync(byte[] output, string fileName);
        #else
        Task WriteAsync(byte[] output, string fileName);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<byte[]> ReadAsync(string fileName);
        #else
        Task<byte[]> ReadAsync(string fileName);
        #endif
    }
}