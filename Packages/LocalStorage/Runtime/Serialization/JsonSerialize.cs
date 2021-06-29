using System.Text;
using UnityEngine;

namespace LocalStorage.Serialization
{
    public static class JsonSerialize
    {
        public static byte[] ToUnityJsonBytes<T>(T data, bool prettyPrint = false) =>
            JsonUtility.ToJson(data, prettyPrint).StringToBytes();

        public static T FromUnityJsonBytes<T>(byte[] data) =>
            JsonUtility.FromJson<T>(data.BytesToString());

        private static byte[] StringToBytes(this string str) =>
            Encoding.UTF8.GetBytes(str);

        private static string BytesToString(this byte[] bytes) =>
            Encoding.UTF8.GetString(bytes);
    }
}