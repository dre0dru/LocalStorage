namespace LocalStorage
{
    public interface ISerializationProvider
    {
        byte[] Serialize<T>(T data);

        T Deserialize<T>(byte[] output);
    }
}