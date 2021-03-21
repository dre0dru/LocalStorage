using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using LocalStorage.Encryption;
using UnityEngine;

namespace LocalStorage.Tests
{
    public static class Helpers
    {
        private class EncryptionSettings : IEncryptionSettings
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

        public static bool FileExist => File.Exists(FilePath);
        
        public static readonly IEncryptionSettings Es = new EncryptionSettings();

        public static void DeleteFileIfExists()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }

        public static void CreateFile()
        {
            File.Create(FilePath);
        }

        public static void WriteToFile(byte[] bytes)
        {
            File.WriteAllBytes(FilePath, bytes);
        }

        public static byte[] ReadFromFile()
        {
            return File.ReadAllBytes(FilePath);
        }

        public static string FromBytes(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public static byte[] Encrypt(this byte[] bytes)
        {
            return AesEncryption.Encrypt(bytes, Es.Key, Es.InitializationVector);
        }
        
        public static byte[] Decrypt(this byte[] bytes)
        {
            return AesEncryption.Decrypt(bytes, Es.Key, Es.InitializationVector);
        }
    }
}