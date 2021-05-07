using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace LocalStorage.Compression
{
    public static class Compress
    {
        public static byte[] WriteGZip(byte[] data)
        {
            using var dataStream = new MemoryStream();
            using (var compressionStream = new GZipStream(dataStream, CompressionMode.Compress))
            {
                compressionStream.Write(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }

        public static byte[] ReadGZip(byte[] data)
        {
            using var dataStream = new MemoryStream(data);
            using var compressionStream = new GZipStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            compressionStream.CopyTo(decompressedStream);

            return decompressedStream.ToArray();
        }

        public static async Task<byte[]> WriteGZipAsync(byte[] data)
        {
            using var dataStream = new MemoryStream();
            using (var compressionStream = new GZipStream(dataStream, CompressionMode.Compress))
            {
                await compressionStream.WriteAsync(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }
        
        public static async Task<byte[]> ReadGZipAsync(byte[] data)
        {
            using var dataStream = new MemoryStream(data);
            using var compressionStream = new GZipStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            await compressionStream.CopyToAsync(decompressedStream);

            return decompressedStream.ToArray();
        }
        
        public static byte[] WriteDeflate(byte[] data)
        {
            using var dataStream = new MemoryStream();
            using (var compressionStream = new DeflateStream(dataStream, CompressionMode.Compress))
            {
                compressionStream.Write(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }

        public static byte[] ReadDeflate(byte[] data)
        {
            using var dataStream = new MemoryStream(data);
            using var compressionStream = new DeflateStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            compressionStream.CopyTo(decompressedStream);

            return decompressedStream.ToArray();
        }

        public static async Task<byte[]> WriteDeflateAsync(byte[] data)
        {
            using var dataStream = new MemoryStream();
            using (var compressionStream = new DeflateStream(dataStream, CompressionMode.Compress))
            {
                await compressionStream.WriteAsync(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }
        
        public static async Task<byte[]> ReadDeflateAsync(byte[] data)
        {
            using var dataStream = new MemoryStream(data);
            using var compressionStream = new DeflateStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            await compressionStream.CopyToAsync(decompressedStream);

            return decompressedStream.ToArray();
        }
        
        
    }
}