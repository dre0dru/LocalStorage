using System.Text;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class UnityJsonSerializationProvider : ISerializationProviderAsync
    {
        private readonly bool _prettyPrint;

        [RequiredMember]
        public UnityJsonSerializationProvider() : this(false)
        {
        }

        public UnityJsonSerializationProvider(bool prettyPrint) =>
            _prettyPrint = prettyPrint;

        public byte[] Serialize<T>(T data) =>
            Encoding.UTF8.GetBytes(JsonUtility.ToJson(data, _prettyPrint));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> SerializeAsync<T>(T data) =>
            UniTask.FromResult(Serialize(data));
        #else
        public Task<byte[]> SerializeAsync<T>(T data) =>
            Task.FromResult(Serialize(data));
        #endif

        public T Deserialize<T>(byte[] data) =>
            JsonUtility.FromJson<T>(Encoding.UTF8.GetString(data));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<T> DeserializeAsync<T>(byte[] data) =>
            UniTask.FromResult(Deserialize<T>(data));
        #else
        public Task<T> DeserializeAsync<T>(byte[] data) =>
            Task.FromResult(Deserialize<T>(data));
        #endif
    }
}