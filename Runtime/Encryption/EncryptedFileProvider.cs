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
            _encryptionSettings = encryptionSettings ??
                                  throw new ArgumentNullException(nameof(encryptionSettings));
        }

        public void Write(byte[] output, string fileName)
        {
            var encrypted = Encrypt(output);
            _fileProvider.Write(encrypted, fileName);
        }

        public Task WriteAsync(byte[] output, string fileName)
        {
            var encrypted = Encrypt(output);
            return _fileProvider.WriteAsync(encrypted, fileName);
        }

        private byte[] Encrypt(byte[] data)
        {
            return AesEncryption.Encrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public byte[] Read(string fileName)
        {
            var bytes = _fileProvider.Read(fileName);
            return Decrypt(bytes);
        }

        public async Task<byte[]> ReadAsync(string fileName)
        {
            var bytes = await _fileProvider.ReadAsync(fileName);
            return Decrypt(bytes);
        }

        private byte[] Decrypt(byte[] data)
        {
            return AesEncryption.Decrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public bool Delete(string fileName)
        {
            return _fileProvider.Delete(fileName);
        }

        public string GetFilePath(string fileName)
        {
            return _fileProvider.GetFilePath(fileName);
        }

        public bool FileExists(string fileName)
        {
            return _fileProvider.FileExists(fileName);
        }
    }
}