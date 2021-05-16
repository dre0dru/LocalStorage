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
            
            if (Directory.Exists(_path) == false)
            {
                Directory.CreateDirectory(_path);
            }
        }

        public void Write(byte[] output, string fileName) =>
            File.WriteAllBytes(GetFilePath(fileName), output);

        public Task WriteAsync(byte[] output, string fileName)
        {
            var fileStream =
                new FileStream(
                    GetFilePath(fileName),
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    4096, true);

            return fileStream.WriteAsync(output, 0, output.Length)
                .ContinueWith(task => fileStream.Dispose());
        }

        public byte[] Read(string fileName) =>
            File.ReadAllBytes(GetFilePath(fileName));

        public Task<byte[]> ReadAsync(string fileName)
        {
            var fileStream =
                new FileStream(
                    GetFilePath(fileName),
                    FileMode.Open, FileAccess.Read, FileShare.Read,
                    4096, true);

            var buffer = new byte[fileStream.Length];
            //Assuming data length is < int.MaxValue
            return fileStream.ReadAsync(buffer, 0, buffer.Length)
                .ContinueWith(task =>
                {
                    fileStream.Dispose();
                    return buffer;
                });
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

        public string GetFilePath(string fileName) =>
            Path.Combine(_path, fileName);

        public bool FileExists(string fileName) =>
            File.Exists(GetFilePath(fileName));
    }
}