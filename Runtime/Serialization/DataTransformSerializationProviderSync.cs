using System;

namespace LocalStorage.Serialization
{
    public class DataTransformSerializationProviderSync : ISerializationProviderSync
    {
        private readonly ISerializationProviderSync _baseProvider;

        private readonly IDataTransformSync _dataTransform;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public DataTransformSerializationProviderSync(ISerializationProviderSync baseProvider,
            IDataTransformSync dataTransform)
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
    }
}