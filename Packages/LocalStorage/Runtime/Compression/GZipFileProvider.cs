using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Compression
{
    public class GZipFileProvider : CompressedFileProvider
    {
        [RequiredMember]
        public GZipFileProvider(IFileProvider fileProvider) : base(fileProvider)
        {
        }

        protected override byte[] Write(byte[] uncompressedData)
        {
            return Compress.WriteGZip(uncompressedData);
        }

        protected override Task<byte[]> WriteAsync(byte[] uncompressedData)
        {
            return Compress.WriteGZipAsync(uncompressedData);
        }

        protected override byte[] Read(byte[] compressedData)
        {
            return Compress.ReadGZip(compressedData);
        }

        protected override Task<byte[]> ReadAsync(byte[] compressedData)
        {
            return Compress.ReadGZipAsync(compressedData);
        }
    }
}