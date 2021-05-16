using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class DataTransformSerializationProvider : ISerializationProvider
    {
        private readonly ISerializationProvider _baseProvider;

        private readonly IDataTransform _dataTransform;

        [RequiredMember]
        public DataTransformSerializationProvider(ISerializationProvider baseProvider,
            IDataTransform dataTransform)
        {
            _baseProvider = baseProvider ??
                            throw new ArgumentNullException(nameof(baseProvider));
            _dataTransform = dataTransform ??
                             throw new ArgumentNullException(nameof(dataTransform));
        }

        public byte[] Serialize<T>(T data) =>
            _dataTransform.Apply(_baseProvider.Serialize(data));

        public Task<byte[]> SerializeAsync<T>(T data) =>
            _baseProvider.SerializeAsync(data)
                .ContinueWith(task => _dataTransform.ApplyAsync(task.Result))
                .Unwrap();

        public T Deserialize<T>(byte[] data) =>
            _baseProvider.Deserialize<T>(_dataTransform.Reverse(data));

        public Task<T> DeserializeAsync<T>(byte[] data) =>
            _dataTransform.ReverseAsync(data)
                .ContinueWith(task => _baseProvider.DeserializeAsync<T>(task.Result))
                .Unwrap();
    }
}