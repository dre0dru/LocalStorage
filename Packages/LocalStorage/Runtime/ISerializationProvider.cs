using System.Threading.Tasks;

namespace LocalStorage
{
    public interface ISerializationProvider
    {
        byte[] Serialize<T>(T data);

        Task<byte[]> SerializeAsync<T>(T data);

        T Deserialize<T>(byte[] data);
        
        Task<T> DeserializeAsync<T>(byte[] data);
    }
}