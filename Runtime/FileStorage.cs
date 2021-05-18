using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage
{
    public class FileStorage : IFileStorage
    {
        private readonly ISerializationProvider _serializationProvider;
        private readonly IFileProvider _fileProvider;

        [RequiredMember]
        public FileStorage(ISerializationProvider serializationProvider,
            IFileProvider fileProvider)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
        }

        public void Save<TData>(TData data, string fileName) =>
            _fileProvider.Write(_serializationProvider.Serialize(data), 
                GetFilePath(fileName));

        public Task SaveAsync<TData>(TData data, string fileName) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(task => 
                    _fileProvider.WriteAsync(task.Result, GetFilePath(fileName)));

        public TData Load<TData>(string fileName) =>
            _serializationProvider
                .Deserialize<TData>(_fileProvider.Read(GetFilePath(fileName)));

        public Task<TData> LoadAsync<TData>(string fileName)
        {
            return _fileProvider.ReadAsync(GetFilePath(fileName))
                .ContinueWith(task => _serializationProvider.Deserialize<TData>(task.Result));
        }

        public bool Delete(string fileName) =>
            _fileProvider.Delete(fileName);

        public string GetFilePath(string fileName) =>
            _fileProvider.GetFilePath(fileName);

        public bool FileExists(string fileName) =>
            _fileProvider.FileExists(fileName);
    }

    public class FileStorage<TSerialization, TFile> : FileStorage, IFileStorage<TSerialization, TFile>
        where TSerialization : ISerializationProvider where TFile : IFileProvider
    {
        [RequiredMember]
        public FileStorage(TSerialization serializationProvider, TFile fileProvider) : base(
            serializationProvider, fileProvider)
        {
        }
    }
}