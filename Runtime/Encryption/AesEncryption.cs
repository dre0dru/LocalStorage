using System;
using System.IO;
using System.Security.Cryptography;

namespace LocalStorage.Encryption
{
    public static class AesEncryption
    {
        public static byte[] Encrypt(byte[] dataToEncrypt, byte[] key, byte[] initializationVector)
        {
            if (dataToEncrypt == null || dataToEncrypt.Length <= 0)
                throw new ArgumentNullException(nameof(dataToEncrypt));

            using var aes = CreateAes(key, initializationVector);

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            return PerformCryptography(dataToEncrypt, encryptor);
        }

        public static byte[] Decrypt(byte[] encryptedData, byte[] key, byte[] initializationVector)
        {
            if (encryptedData == null || encryptedData.Length <= 0)
                throw new ArgumentNullException(nameof(encryptedData));
            
            using var aes = CreateAes(key, initializationVector);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            return PerformCryptography(encryptedData, decryptor);
        }

        private static Aes CreateAes(byte[] key, byte[] initializationVector)
        {
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (initializationVector == null || initializationVector.Length <= 0)
                throw new ArgumentNullException(nameof(initializationVector));
            
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = initializationVector;
            return aes;
        }

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using var msDecrypt = new MemoryStream();
            using (var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Write))
            {
                csDecrypt.Write(data, 0, data.Length);
                csDecrypt.FlushFinalBlock();
            }
            
            return msDecrypt.ToArray();
        }
    }
}