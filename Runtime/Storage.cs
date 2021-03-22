using System;
using System.IO;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage
{
    public class Storage
    {
        private readonly ISerializationProvider _serializationProvider;
        private readonly IFileProvider _fileProvider;

        [RequiredMember]
        public Storage(ISerializationProvider serializationProvider,
            IFileProvider fileProvider)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _fileProvider = fileProvider ??
                            throw new ArgumentNullException(nameof(fileProvider));
        }

        public void Save<TData>(TData data, string fileName)
        {
            var output = _serializationProvider.Serialize(data);
            _fileProvider.Write(output, GetFilePath(fileName));
        }

        public TData Load<TData>(string fileName)
        {
            var output = _fileProvider.Read(GetFilePath(fileName));
            return _serializationProvider.Deserialize<TData>(output);
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
            return Path.Combine(Application.persistentDataPath, fileName);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(GetFilePath(fileName));
        }
    }
}