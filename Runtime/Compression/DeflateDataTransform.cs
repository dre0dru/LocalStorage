#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Dre0Dru.LocalStorage.Compression
{
    public class DeflateDataTransform : IDataTransform
    {
        public byte[] Apply(byte[] data)
        {
            return Compress.WriteDeflate(data);
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteDeflateAsync(data).AsUniTask();
        }
        #else
        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteDeflateAsync(data);
        }
        #endif

        public byte[] Reverse(byte[] data)
        {
            return Compress.ReadDeflate(data);
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadDeflateAsync(data).AsUniTask();
        }
        #else
        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadDeflateAsync(data);
        }
        #endif
    }
}