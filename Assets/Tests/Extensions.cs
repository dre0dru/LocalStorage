using System.IO.Compression;
using System.Text;
using LocalStorage.Compression;
using LocalStorage.Encryption;
using UnityEngine;

namespace LocalStorage.Tests
{
    public static class Extensions
    {
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
            return AesEncryption.Encrypt(bytes, Constants.Es.Key, Constants.Es.InitializationVector);
        }
        
        public static byte[] Decrypt(this byte[] bytes)
        {
            return AesEncryption.Decrypt(bytes, Constants.Es.Key, Constants.Es.InitializationVector);
        }

        public static byte[] WriteGZip(this byte[] bytes)
        {
            return Compress.WriteGZip(bytes);
        }

        public static byte[] ReadGzip(this byte[] bytes)
        {
            return Compress.ReadGZip(bytes);
        }
        
        public static byte[] WriteDeflate(this byte[] bytes)
        {
            return Compress.WriteDeflate(bytes);
        }

        public static byte[] ReadDeflate(this byte[] bytes)
        {
            return Compress.ReadDeflate(bytes);
        }
    }
}