namespace LocalStorage
{
    public interface IDataTransform : IDataTransformSync, IDataTransformAsync
    {
    }

    public interface IDataTransformSync
    {
        byte[] Apply(byte[] data);

        byte[] Reverse(byte[] data);
    }

    public interface IDataTransformAsync
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<byte[]> ApplyAsync(byte[] data);
        #else
        System.Threading.Tasks.Task<byte[]> ApplyAsync(byte[] data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<byte[]> ReverseAsync(byte[] data);
        #else
        System.Threading.Tasks.Task<byte[]> ReverseAsync(byte[] data);
        #endif
    }
}