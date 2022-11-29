namespace Dre0Dru.LocalStorage
{
    public interface IFileProvider : IFileProviderSync, IFileProviderAsync
    {
    }

    public interface IFileProviderSync : IFileProviderCommon
    {
        void Write(byte[] output, string fileName);

        byte[] Read(string fileName);
    }

    public interface IFileProviderAsync : IFileProviderCommon
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask WriteAsync(byte[] output, string fileName);
        #else
        System.Threading.Tasks.Task WriteAsync(byte[] output, string fileName);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<byte[]> ReadAsync(string fileName);
        #else
        System.Threading.Tasks.Task<byte[]> ReadAsync(string fileName);
        #endif
    }

    public interface IFileProviderCommon
    {
        bool Delete(string fileName);

        string GetFilePath(string fileName);

        bool FileExists(string fileName);
    }
}