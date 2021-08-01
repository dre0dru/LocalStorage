#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage.Serialization
{
    public class UnityJsonSerializationProvider : ISerializationProvider
    {
        private readonly bool _prettyPrint;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public UnityJsonSerializationProvider() : this(false)
        {
        }

        public UnityJsonSerializationProvider(bool prettyPrint)
        {
            _prettyPrint = prettyPrint;
        }

        public byte[] Serialize<T>(T data) =>
            JsonSerialize.ToUnityJsonBytes(data, _prettyPrint);

        public T Deserialize<T>(byte[] data) =>
            JsonSerialize.FromUnityJsonBytes<T>(data);

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> SerializeAsync<T>(T data) =>
            this.SerializeFakeAsync(data);
        #else
        public Task<byte[]> SerializeAsync<T>(T data) =>
            this.SerializeFakeAsync(data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<T> DeserializeAsync<T>(byte[] data) =>
            this.DeserializeFakeAsync<T>(data);
        #else
        public Task<T> DeserializeAsync<T>(byte[] data) =>
            this.DeserializeFakeAsync<T>(data);
        #endif
    }
}