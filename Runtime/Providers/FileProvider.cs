using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class FileProvider : IFileProvider
    {
        private readonly string _path;

        [RequiredMember]
        public FileProvider() : this(null)
        {
        }

        public FileProvider(string path)
        {
            _path = string.IsNullOrEmpty(path)
                ? Application.persistentDataPath
                : Path.Combine(Application.persistentDataPath, path);
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
            return Path.Combine(_path, fileName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }
    }
}