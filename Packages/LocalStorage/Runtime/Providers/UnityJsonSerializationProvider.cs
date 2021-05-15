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

        public UnityJsonSerializationProvider(bool prettyPrint)
        {
            _prettyPrint = prettyPrint;
        }

        public byte[] Serialize<T>(T data)
        {
            var json = JsonUtility.ToJson(data, _prettyPrint);
            return Encoding.UTF8.GetBytes(json);
        }

        public Task<byte[]> SerializeAsync<T>(T data)
        {
            return Task.FromResult(Serialize(data));
        }

        public T Deserialize<T>(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(json);
        }

        public Task<T> DeserializeAsync<T>(byte[] data)
        {
            return Task.FromResult(Deserialize<T>(data));
        }
    }
}