using System.Threading.Tasks;

namespace LocalStorage.Compression
{
    public class GZipDataTransform : IDataTransform
    {
        public byte[] Apply(byte[] data)
        {
            return Compress.WriteGZip(data);
        }

        public Task<byte[]> ApplyAsync(byte[] data)
        {
            return Compress.WriteGZipAsync(data);
        }

        public byte[] Reverse(byte[] data)
        {
            return Compress.ReadGZip(data);
        }

        public Task<byte[]> ReverseAsync(byte[] data)
        {
            return Compress.ReadGZipAsync(data);
        }
    }
}