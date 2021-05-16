using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace LocalStorage.Compression
{
    public static class Compress
    {
        public static byte[] WriteGZip(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using var dataStream = new MemoryStream();
            using (var compressionStream = new GZipStream(dataStream, CompressionMode.Compress))
            {
                compressionStream.Write(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }

        public static Task<byte[]> WriteGZipAsync(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataStream = new MemoryStream();
            var compressionStream = new GZipStream(dataStream, CompressionMode.Compress);

            return compressionStream.WriteAsync(data, 0, data.Length)
                .ContinueWith(task =>
                {
                    using (dataStream)
                    {
                        //Must manually dispose before returning
                        //https://github.com/dotnet/runtime/issues/15371
                        compressionStream.Dispose();
                        return dataStream.ToArray();
                    }
                });
        }

        public static byte[] ReadGZip(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using var dataStream = new MemoryStream(data);
            using var compressionStream = new GZipStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            compressionStream.CopyTo(decompressedStream);

            return decompressedStream.ToArray();
        }

        public static Task<byte[]> ReadGZipAsync(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataStream = new MemoryStream(data);
            var compressionStream = new GZipStream(dataStream, CompressionMode.Decompress);
            var decompressedStream = new MemoryStream();

            return compressionStream.CopyToAsync(decompressedStream)
                .ContinueWith(task =>
                {
                    using (dataStream)
                    using (compressionStream)
                    using (decompressedStream)
                    {
                        return decompressedStream.ToArray();
                    }
                });
        }

        public static byte[] WriteDeflate(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            using var dataStream = new MemoryStream();
            using (var compressionStream = new DeflateStream(dataStream, CompressionMode.Compress))
            {
                compressionStream.Write(data, 0, data.Length);
            }

            return dataStream.ToArray();
        }

        public static Task<byte[]> WriteDeflateAsync(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataStream = new MemoryStream();
            var compressionStream = new DeflateStream(dataStream, CompressionMode.Compress);

            return compressionStream.WriteAsync(data, 0, data.Length)
                .ContinueWith(task =>
                {
                    using (dataStream)
                    {
                        //Must manually dispose before returning
                        //https://github.com/dotnet/runtime/issues/15371
                        compressionStream.Dispose();
                        return dataStream.ToArray();
                    }
                });
        }

        public static byte[] ReadDeflate(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            using var dataStream = new MemoryStream(data);
            using var compressionStream = new DeflateStream(dataStream, CompressionMode.Decompress);
            using var decompressedStream = new MemoryStream();
            compressionStream.CopyTo(decompressedStream);

            return decompressedStream.ToArray();
        }

        public static Task<byte[]> ReadDeflateAsync(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var dataStream = new MemoryStream(data);
            var compressionStream = new DeflateStream(dataStream, CompressionMode.Decompress);
            var decompressedStream = new MemoryStream();

            return compressionStream.CopyToAsync(decompressedStream)
                .ContinueWith(task =>
                {
                    using (dataStream)
                    using (compressionStream)
                    using (decompressedStream)
                    {
                        return decompressedStream.ToArray();
                    }
                });
        }
    }
}