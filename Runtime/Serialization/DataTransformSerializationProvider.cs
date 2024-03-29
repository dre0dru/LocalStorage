using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Dre0Dru.LocalStorage.Serialization
{
    public class DataTransformSerializationProvider : ISerializationProvider
    {
        private readonly ISerializationProvider _baseProvider;

        private readonly IDataTransform _dataTransform;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
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
        
        public T Deserialize<T>(byte[] data) =>
            _baseProvider.Deserialize<T>(_dataTransform.Reverse(data));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> SerializeAsync<T>(T data) =>
            _baseProvider.SerializeAsync(data)
                .ContinueWith(bytes => _dataTransform.ApplyAsync(bytes));
        #else
        public Task<byte[]> SerializeAsync<T>(T data) =>
            _baseProvider.SerializeAsync(data)
                .ContinueWith(task => _dataTransform.ApplyAsync(task.Result))
                .Unwrap();
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<T> DeserializeAsync<T>(byte[] data) =>
            _dataTransform.ReverseAsync(data)
                .ContinueWith(bytes => _baseProvider.DeserializeAsync<T>(bytes));
        #else
        public Task<T> DeserializeAsync<T>(byte[] data) =>
            _dataTransform.ReverseAsync(data)
                .ContinueWith(task => _baseProvider.DeserializeAsync<T>(task.Result))
                .Unwrap();
        #endif
    }
}