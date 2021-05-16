using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class UnityJsonSerializationProvider : ISerializationProvider
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

        public Task<byte[]> SerializeAsync<T>(T data) =>
            Task.FromResult(Serialize(data));

        public T Deserialize<T>(byte[] data) =>
            JsonUtility.FromJson<T>(Encoding.UTF8.GetString(data));

        public Task<T> DeserializeAsync<T>(byte[] data) =>
            Task.FromResult(Deserialize<T>(data));
    }
}