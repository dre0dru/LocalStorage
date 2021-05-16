using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LocalStorage.Encryption
{
    public static class AesEncryption
    {
        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] initializationVector)
        {
            if (dataToEncrypt == null)
            {
                throw new ArgumentNullException(nameof(dataToEncrypt));
            }

            var encryptor = CreateEncryptor(key, initializationVector);

            return PerformCryptography(dataToEncrypt, encryptor);
        }

        public static Task<byte[]> EncryptAsync(byte[] dataToEncrypt, byte[] key, byte[] initializationVector)
        {
            if (dataToEncrypt == null)
            {
                throw new ArgumentNullException(nameof(dataToEncrypt));
            }

            var encryptor = CreateEncryptor(key, initializationVector);

            return PerformCryptographyAsync(dataToEncrypt, encryptor);
        }

        public static byte[] Decrypt(byte[] encryptedData, byte[] key, byte[] initializationVector)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            var decryptor = CreateDecryptor(key, initializationVector);

            return PerformCryptography(encryptedData, decryptor);
        }

        public static Task<byte[]> DecryptAsync(byte[] encryptedData, byte[] key, byte[] initializationVector)
        {
            if (encryptedData == null)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            var decryptor = CreateDecryptor(key, initializationVector);

            return PerformCryptographyAsync(encryptedData, decryptor);
        }

        private static ICryptoTransform CreateEncryptor(byte[] key, byte[] initializationVector)
        {
            using var aes = CreateAes(key, initializationVector);

            return aes.CreateEncryptor(aes.Key, aes.IV);
        }

        private static ICryptoTransform CreateDecryptor(byte[] key, byte[] initializationVector)
        {
            using var aes = CreateAes(key, initializationVector);

            return aes.CreateDecryptor(aes.Key, aes.IV);
        }

        private static Aes CreateAes(byte[] key, byte[] initializationVector)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = initializationVector;
            return aes;
        }

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (cryptoTransform)
            using (var msDecrypt = new MemoryStream())
            using (var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Write))
            {
                csDecrypt.Write(data, 0, data.Length);
                csDecrypt.FlushFinalBlock();

                return msDecrypt.ToArray();
            }
        }

        private static Task<byte[]> PerformCryptographyAsync(byte[] data, ICryptoTransform cryptoTransform)
        {
            var msDecrypt = new MemoryStream();
            var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Write);
            return csDecrypt.WriteAsync(data, 0, data.Length)
                .ContinueWith(task =>
                {
                    using (csDecrypt)
                    using (cryptoTransform)
                    using (msDecrypt)
                    {
                        csDecrypt.FlushFinalBlock();
                        return msDecrypt.ToArray();
                    }
                });
        }
    }
}