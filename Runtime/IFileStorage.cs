namespace Dre0Dru.LocalStorage
{
    public interface IFileStorage : IFileStorageSync, IFileStorageAsync
    {
    }

    public interface IFileStorageSync : IFileProviderCommon
    {
        void Save<TData>(TData data, string fileName);

        TData Load<TData>(string fileName);
    }

    public interface IFileStorageAsync : IFileProviderCommon
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask SaveAsync<TData>(TData data, string fileName);
        #else
        System.Threading.Tasks.Task SaveAsync<TData>(TData data, string fileName);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<TData> LoadAsync<TData>(string fileName);
        #else
        System.Threading.Tasks.Task<TData> LoadAsync<TData>(string fileName);
        #endif
    }
}