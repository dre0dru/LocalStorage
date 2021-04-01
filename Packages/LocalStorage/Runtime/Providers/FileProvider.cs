using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class FileProvider : IFileProvider
    {
        private readonly string _filePath;

        [RequiredMember]
        public FileProvider() : this(Application.persistentDataPath)
        {
        }

        public FileProvider(string filePath)
        {
            _filePath = string.IsNullOrEmpty(filePath)
                ? throw new ArgumentNullException(nameof(filePath))
                : filePath;
        }

        public void Write(byte[] output, string fileName)
        {
            File.WriteAllBytes(GetFilePath(fileName), output);
        }

        public async Task WriteAsync(byte[] output, string fileName)
        {
            using var fileStream =
                new FileStream(
                    GetFilePath(fileName),
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    4096, true);

            await fileStream.WriteAsync(output, 0, output.Length);
        }

        public byte[] Read(string fileName)
        {
            return File.ReadAllBytes(GetFilePath(fileName));
        }

        public async Task<byte[]> ReadAsync(string fileName)
        {
            using var fileStream =
                new FileStream(
                    GetFilePath(fileName),
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    4096, true);


            var buffer = new byte[fileStream.Length];
            //Assuming data length is < int.MaxValue
            await fileStream.ReadAsync(buffer, 0, buffer.Length);

            return buffer;
        }

        public bool Delete(string fileName)
        {
            if (FileExists(fileName))
            {
                File.Delete(GetFilePath(fileName));
                return true;
            }

            return false;
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_filePath, fileName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }
    }
}