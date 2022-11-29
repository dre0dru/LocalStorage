using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Dre0Dru.LocalStorage.Serialization
{
    public class DataTransformSerializationProviderAsync : ISerializationProviderAsync
    {
        private readonly ISerializationProviderAsync _baseProvider;

        private readonly IDataTransformAsync _dataTransform;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public DataTransformSerializationProviderAsync(ISerializationProviderAsync baseProvider,
            IDataTransformAsync dataTransform)
        {
            _baseProvider = baseProvider ??
                            throw new ArgumentNullException(nameof(baseProvider));
            _dataTransform = dataTransform ??
                             throw new ArgumentNullException(nameof(dataTransform));
        }

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