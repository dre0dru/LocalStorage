namespace Dre0Dru.LocalStorage.Encryption
{
    public interface IEncryptionSettings
    {
        byte[] Key { get; }

        byte[] InitializationVector { get; }
    }
}