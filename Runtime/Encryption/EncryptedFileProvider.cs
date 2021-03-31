using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Encryption
{
    public class EncryptedFileProvider : IFileProvider
    {
        private readonly IFileProvider _fileProvider;
        private readonly IEncryptionSettings _encryptionSettings;

        [RequiredMember]
        public EncryptedFileProvider(IFileProvider fileProvider,
            IEncryptionSettings encryptionSettings)
        {
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
            ;
            _encryptionSettings = encryptionSettings ??
                                  throw new ArgumentNullException(nameof(encryptionSettings));
            ;
        }

        public void Write(byte[] output, string filePath)
        {
            var encrypted = Encrypt(output);
            _fileProvider.Write(encrypted, filePath);
        }

        public Task WriteAsync(byte[] output, string filePath)
        {
            var encrypted = Encrypt(output);
            return _fileProvider.WriteAsync(encrypted, filePath);
        }

        private byte[] Encrypt(byte[] data)
        {
            return AesEncryption.Encrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public byte[] Read(string filePath)
        {
            var bytes = _fileProvider.Read(filePath);
            return Decrypt(bytes);
        }

        public async Task<byte[]> ReadAsync(string filePath)
        {
            var bytes = await _fileProvider.ReadAsync(filePath);
            return Decrypt(bytes);
        }

        private byte[] Decrypt(byte[] data)
        {
            return AesEncryption.Decrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }
    }
}