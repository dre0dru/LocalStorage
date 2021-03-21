using System;
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
            _fileProvider = fileProvider  ??
                            throw new ArgumentNullException(nameof(fileProvider));;
            _encryptionSettings = encryptionSettings  ??
                                  throw new ArgumentNullException(nameof(encryptionSettings));;
        }

        public void Write(byte[] output, string filePath)
        {
            var encrypted = AesEncryption.Encrypt(output, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
            _fileProvider.Write(encrypted, filePath);
        }

        public byte[] Read(string filePath)
        {
            var bytes = _fileProvider.Read(filePath);
            return AesEncryption.Decrypt(bytes, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }
    }
}