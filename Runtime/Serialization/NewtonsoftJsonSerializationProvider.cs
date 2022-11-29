#if NEWTONSOFT_JSON_SUPPORT

#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif
using System.Text;
using Newtonsoft.Json;

namespace Dre0Dru.LocalStorage.Serialization
{
    public class NewtonsoftJsonSerializationProvider : ISerializationProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public NewtonsoftJsonSerializationProvider(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public byte[] Serialize<T>(T data) =>
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, _serializerSettings));

        public T Deserialize<T>(byte[] data) =>
            JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));

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
#endif
