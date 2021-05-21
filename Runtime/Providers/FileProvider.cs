using System.IO;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif
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

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask WriteAsync(byte[] output, string fileName)
        {
            var fileStream = CreateFileStream(fileName, FileMode.Create, 
                FileAccess.Write, FileShare.None);

            return fileStream.WriteAsync(output, 0, output.Length)
                .ContinueWith(task => fileStream.Dispose()).AsUniTask();
        }
        #else
        public Task WriteAsync(byte[] output, string fileName)
        {
            var fileStream = CreateFileStream(fileName, FileMode.Create, 
                FileAccess.Write, FileShare.None);

            return fileStream.WriteAsync(output, 0, output.Length)
                .ContinueWith(task => fileStream.Dispose());
        }
        #endif

        public byte[] Read(string fileName) =>
            File.ReadAllBytes(GetFilePath(fileName));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ReadAsync(string fileName)
        {
            var fileStream = CreateFileStream(fileName, FileMode.Open, 
                FileAccess.Read, FileShare.Read);

            var buffer = new byte[fileStream.Length];
            //Assuming data length is < int.MaxValue
            return fileStream.ReadAsync(buffer, 0, buffer.Length)
                .ContinueWith(task =>
                {
                    fileStream.Dispose();
                    return buffer;
                }).AsUniTask();
        }
        #else
        public Task<byte[]> ReadAsync(string fileName)
        {
            var fileStream = CreateFileStream(fileName, FileMode.Open, 
                FileAccess.Read, FileShare.Read);

            var buffer = new byte[fileStream.Length];
            //Assuming data length is < int.MaxValue
            return fileStream.ReadAsync(buffer, 0, buffer.Length)
                .ContinueWith(task =>
                {
                    fileStream.Dispose();
                    return buffer;
                });
        }
        #endif

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

        private FileStream CreateFileStream(string fileName, FileMode fileMode,
            FileAccess fileAccess, FileShare fileShare)
        {
            return new FileStream(
                GetFilePath(fileName),
                fileMode, fileAccess, fileShare,
                4096, true);
        }
    }
}