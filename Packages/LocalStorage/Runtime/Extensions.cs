namespace LocalStorage
{
    public static class Extensions
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask<byte[]> SerializeFakeAsync<T>(
            this ISerializationProviderSync provider, T data) =>
            Cysharp.Threading.Tasks.UniTask.FromResult<byte[]>(provider.Serialize(data));
        #else
        public static System.Threading.Tasks.Task<byte[]> SerializeFakeAsync<T>(
            this ISerializationProviderSync provider, T data) =>
            System.Threading.Tasks.Task.FromResult<byte[]>(provider.Serialize(data));
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask<T> DeserializeFakeAsync<T>(
            this ISerializationProviderSync provider, byte[] data) =>
            Cysharp.Threading.Tasks.UniTask.FromResult<T>(provider.Deserialize<T>(data));
        #else
        public static System.Threading.Tasks.Task<T> DeserializeFakeAsync<T>(this ISerializationProviderSync provider,
            byte[] data) =>
            System.Threading.Tasks.Task.FromResult<T>(provider.Deserialize<T>(data));
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask WriteFakeAsync(this IFileProviderSync provider, byte[] output,
            string fileName)
        {
            provider.Write(output, fileName);
            return Cysharp.Threading.Tasks.UniTask.CompletedTask;
        }
        #else
        public static System.Threading.Tasks.Task WriteFakeAsync(this IFileProviderSync provider, byte[] output,
            string fileName)
        {
            provider.Write(output, fileName);
            return System.Threading.Tasks.Task.CompletedTask;
        }
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask<byte[]> ReadFakeAsync(this IFileProviderSync provider,
            string fileName) =>
            Cysharp.Threading.Tasks.UniTask.FromResult<byte[]>(provider.Read(fileName));
        #else
        public static System.Threading.Tasks.Task<byte[]> ReadFakeAsync(this IFileProviderSync provider,
            string fileName) =>
            System.Threading.Tasks.Task.FromResult<byte[]>(provider.Read(fileName));
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask<byte[]> ApplyFakeAsync(this IDataTransformSync dataTransform,
            byte[] data) =>
            Cysharp.Threading.Tasks.UniTask.FromResult<byte[]>(dataTransform.Apply(data));
        #else
        public static System.Threading.Tasks.Task<byte[]> ApplyFakeAsync(this IDataTransformSync dataTransform,
            byte[] data) =>
            System.Threading.Tasks.Task.FromResult<byte[]>(dataTransform.Apply(data));
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public static Cysharp.Threading.Tasks.UniTask<byte[]> ReverseFakeAsync(this IDataTransformSync dataTransform,
            byte[] data) =>
            Cysharp.Threading.Tasks.UniTask.FromResult<byte[]>(dataTransform.Reverse(data));
        #else
        public static System.Threading.Tasks.Task<byte[]> ReverseFakeAsync(this IDataTransformSync dataTransform,
            byte[] data) =>
            System.Threading.Tasks.Task.FromResult<byte[]>(dataTransform.Reverse(data));
        #endif
    }
}