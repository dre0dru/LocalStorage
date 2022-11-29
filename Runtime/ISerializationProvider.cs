namespace Dre0Dru.LocalStorage
{
    public interface ISerializationProvider : ISerializationProviderSync, ISerializationProviderAsync
    {
    }

    public interface ISerializationProviderSync
    {
        byte[] Serialize<T>(T data);

        T Deserialize<T>(byte[] data);
    }

    public interface ISerializationProviderAsync
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<byte[]> SerializeAsync<T>(T data);
        #else
        System.Threading.Tasks.Task<byte[]> SerializeAsync<T>(T data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<T> DeserializeAsync<T>(byte[] data);
        #else
        System.Threading.Tasks.Task<T> DeserializeAsync<T>(byte[] data);
        #endif
    }
}