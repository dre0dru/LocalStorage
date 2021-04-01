using System.IO;

namespace LocalStorage.Tests
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
    }
}