using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif
using UnityEngine.Scripting;

namespace LocalStorage.Encryption
{
    public class AesEncryptionDataTransform : IDataTransformAsync
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

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ApplyAsync(byte[] data)
        {
            return AesEncryption.EncryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector).AsUniTask();
        }
        #else
        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return AesEncryption.EncryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }
        #endif

        public byte[] Reverse(byte[] data)
        {
            return AesEncryption.Decrypt(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ReverseAsync(byte[] data)
        {
            return AesEncryption.DecryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector).AsUniTask();
        }
        #else
        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return AesEncryption.DecryptAsync(data, _encryptionSettings.Key,
                _encryptionSettings.InitializationVector);
        }
        #endif
    }
}