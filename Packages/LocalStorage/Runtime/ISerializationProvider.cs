#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage
{
    public interface ISerializationProvider
    {
        byte[] Serialize<T>(T data);

        T Deserialize<T>(byte[] data);
    }

    public interface ISerializationProviderAsync : ISerializationProvider
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<byte[]> SerializeAsync<T>(T data);
        #else
        Task<byte[]> SerializeAsync<T>(T data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<T> DeserializeAsync<T>(byte[] data);
        #else
        Task<T> DeserializeAsync<T>(byte[] data);
        #endif
    }
}