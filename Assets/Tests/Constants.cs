using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using LocalStorage.Encryption;
using UnityEngine;

namespace LocalStorage.Tests
{
    public static class Constants
    {
        public class EncryptionSettings : IEncryptionSettings
        {
            public byte[] Key { get; private set; }
            public byte[] InitializationVector { get; private set; }

            public EncryptionSettings()
            {
                using var aes = Aes.Create();
                Key = aes.Key;
                InitializationVector = aes.IV;
            }
        }

        public const string FileName = "file.test";
        
        public static string FilePath => Path.Combine(Application.persistentDataPath, FileName);
        
        public static readonly IEncryptionSettings Es = new EncryptionSettings();
        
        public static IEnumerable<byte[]> FileProviderTestData()
        {
            yield return new byte[] {1, 2, 3};
            yield return "string".ToBytes();
        }
    }
}