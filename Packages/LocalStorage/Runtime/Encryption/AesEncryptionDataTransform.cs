using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Encryption
{
    public class AesEncryptionDataTransform : IDataTransform
    {
        private readonly IEncryptionSettings _encryptionSettings;

        [RequiredMember]
        public AesEncryptionDataTransform(IEncryptionSettings encryptionSettings)
        {
            _encryptionSettings = encryptionSettings ??
                                  throw new ArgumentNullException(nameof(encryptionSettings));
        }

        public byte[] Apply(byte[] data)
        {
            return AesEncryption.Encrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return AesEncryption.EncryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public byte[] Reverse(byte[] data)
        {
            return AesEncryption.Decrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return AesEncryption.DecryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }
    }
}