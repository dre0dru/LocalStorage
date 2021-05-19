using System.IO;
using UnityEngine;

namespace LocalStorage.PlayModeTests
{
    public static class Setup
    {
        public static void CreateEmptyFile(string filePath)
        {
            File.Create(filePath).Dispose();
        }

        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public static byte[] ReadFromFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        public static void WriteToFile(string filePath, byte[] data)
        {
            File.WriteAllBytes(filePath, data);
        }

        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public static void PlayerPrefsCreateKey(string key)
        {
            PlayerPrefs.SetString(key, "string");
            PlayerPrefs.Save();
        }
    }
}