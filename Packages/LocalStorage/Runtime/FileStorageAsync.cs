using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage
{
    public class FileStorageAsync : IFileStorageAsync
    {
        private readonly ISerializationProviderAsync _serializationProvider;
        private readonly IFileProviderAsync _fileProvider;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public FileStorageAsync(ISerializationProviderAsync serializationProvider,
            IFileProviderAsync fileProvider)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask SaveAsync<TData>(TData data, string fileName) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(bytes =>
                    _fileProvider.WriteAsync(bytes, GetFilePath(fileName)));
        #else
        public Task SaveAsync<TData>(TData data, string fileName) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(task =>
                    _fileProvider.WriteAsync(task.Result, GetFilePath(fileName)));
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<TData> LoadAsync<TData>(string fileName)
        {
            return _fileProvider.ReadAsync(GetFilePath(fileName))
                .ContinueWith(bytes => _serializationProvider.DeserializeAsync<TData>(bytes));
        }
        #else
        public Task<TData> LoadAsync<TData>(string fileName)
        {
            return _fileProvider.ReadAsync(GetFilePath(fileName))
                .ContinueWith(task => _serializationProvider.DeserializeAsync<TData>(task.Result)).Unwrap();
        }
        #endif

        public bool Delete(string fileName) =>
            _fileProvider.Delete(fileName);

        public string GetFilePath(string fileName) =>
            _fileProvider.GetFilePath(fileName);

        public bool FileExists(string fileName) =>
            _fileProvider.FileExists(fileName);
    }
}