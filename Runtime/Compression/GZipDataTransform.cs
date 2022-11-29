#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Dre0Dru.LocalStorage.Compression
{
    public class GZipDataTransform : IDataTransform
    {
        public byte[] Apply(byte[] data)
        {
            return Compress.WriteGZip(data);
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteGZipAsync(data).AsUniTask();
        }
        #else
        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteGZipAsync(data);
        }
        #endif

        public byte[] Reverse(byte[] data)
        {
            return Compress.ReadGZip(data);
        }

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadGZipAsync(data).AsUniTask();
        }
        #else
        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadGZipAsync(data);
        }
        #endif
    }
}