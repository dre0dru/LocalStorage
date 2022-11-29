using System;

namespace Dre0Dru.LocalStorage
{
    public class FileStorageSync : IFileStorageSync
    {
        private readonly ISerializationProviderSync _serializationProvider;
        private readonly IFileProviderSync _fileProvider;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public FileStorageSync(ISerializationProviderSync serializationProvider,
            IFileProviderSync fileProvider)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
        }

        public void Save<TData>(TData data, string fileName) =>
            _fileProvider.Write(_serializationProvider.Serialize(data),
                GetFilePath(fileName));

        public TData Load<TData>(string fileName) =>
            _serializationProvider
                .Deserialize<TData>(_fileProvider.Read(GetFilePath(fileName)));

        public bool Delete(string fileName) =>
            _fileProvider.Delete(fileName);

        public string GetFilePath(string fileName) =>
            _fileProvider.GetFilePath(fileName);

        public bool FileExists(string fileName) =>
            _fileProvider.FileExists(fileName);
    }
}