#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage
{
    public interface IDataTransform
    {
        byte[] Apply(byte[] data);

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<byte[]> ApplyAsync(byte[] data);
        #else
        Task<byte[]> ApplyAsync(byte[] data);
        #endif

        byte[] Reverse(byte[] data);

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<byte[]> ReverseAsync(byte[] data);
        #else
        Task<byte[]> ReverseAsync(byte[] data);
        #endif
    }
}