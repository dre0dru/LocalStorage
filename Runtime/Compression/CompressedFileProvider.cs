using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Compression
{
    public abstract class CompressedFileProvider: IFileProvider
    {
        private readonly IFileProvider _fileProvider;

        [RequiredMember]
        protected CompressedFileProvider(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
            
            _fileProvider = fileProvider;
        }

        public void Write(byte[] output, string fileName)
        {
            var bytes = Write(output);
            _fileProvider.Write(bytes, fileName);
        }

        public async Task WriteAsync(byte[] output, string fileName)
        {
            var bytes = await WriteAsync(output);
            await _fileProvider.WriteAsync(bytes, fileName);
        }

        public byte[] Read(string fileName)
        {
            var bytes = _fileProvider.Read(fileName);
            return Read(bytes);
        }

        public async Task<byte[]> ReadAsync(string fileName)
        {
            var bytes = await _fileProvider.ReadAsync(fileName);
            return await ReadAsync(bytes);
        }

        public bool Delete(string fileName)
        {
            return _fileProvider.Delete(fileName);
        }

        public string GetFilePath(string fileName)
        {
            return _fileProvider.GetFilePath(fileName);
        }

        public bool FileExists(string fileName)
        {
            return _fileProvider.FileExists(fileName);
        }

        protected abstract byte[] Write(byte[] uncompressedData);
        protected abstract Task<byte[]> WriteAsync(byte[] uncompressedData);
        protected abstract byte[] Read(byte[] compressedData);
        protected abstract Task<byte[]> ReadAsync(byte[] compressedData);
    }
}