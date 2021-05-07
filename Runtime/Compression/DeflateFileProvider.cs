using System.IO.Compression;
using System.Threading.Tasks;

namespace LocalStorage.Compression
{
    public class DeflateFileProvider : CompressedFileProvider
    {
        public DeflateFileProvider(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        protected override byte[] Write(byte[] uncompressedData)
        {
            return Compress.WriteDeflate(uncompressedData);
        }

        protected override Task<byte[]> WriteAsync(byte[] uncompressedData)
        {
            return Compress.WriteDeflateAsync(uncompressedData);
        }

        protected override byte[] Read(byte[] compressedData)
        {
            return Compress.ReadDeflate(compressedData);
        }

        protected override Task<byte[]> ReadAsync(byte[] compressedData)
        {
            return Compress.ReadDeflateAsync(compressedData);
        }
    }
}