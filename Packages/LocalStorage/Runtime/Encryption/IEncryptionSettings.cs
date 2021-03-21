namespace LocalStorage.Encryption
{
    public interface IEncryptionSettings
    {
        byte[] Key { get; }

        byte[] InitializationVector { get; }
    }
}