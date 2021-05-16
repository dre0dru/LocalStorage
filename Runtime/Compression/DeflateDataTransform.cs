using System.Threading.Tasks;

namespace LocalStorage.Compression
{
    public class DeflateDataTransform : IDataTransform
    {
        public byte[] Apply(byte[] data)
        {
            return Compress.WriteDeflate(data);
        }

        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteDeflateAsync(data);
        }

        public byte[] Reverse(byte[] data)
        {
            return Compress.ReadDeflate(data);
        }

        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadDeflateAsync(data);
        }
    }
}