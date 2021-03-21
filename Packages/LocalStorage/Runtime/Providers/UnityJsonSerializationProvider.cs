using System.Text;
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

        public T Deserialize<T>(byte[] output)
        {
            var json = Encoding.UTF8.GetString(output);
            return JsonUtility.FromJson<T>(json);
        }
    }
}