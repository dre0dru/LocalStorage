using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public abstract class FileProviderTestsBase : TestsBase
    {
        protected static IEnumerable<byte[]> FileProviderTestData()
        {
            yield return new byte[] {1, 2, 3};
            yield return "string".ToBytes();
        }

        protected abstract IFileProvider FileProvider { get; }

        [TestCaseSource(nameof(FileProviderTestData))]
        public void FileProvider_Write(byte[] data)
        {
            Assert.IsFalse(Helpers.FileExist);

            FileProvider.Write(data, Helpers.FilePath);

            Assert.IsTrue(Helpers.FileExist);
            Assert.AreEqual(GetExpectedResultFor(data), Helpers.ReadFromFile());
        }

        [TestCaseSource(nameof(FileProviderTestData))]
        public void FileProvider_WriteAsync(byte[] data)
        {
            Assert.IsFalse(Helpers.FileExist);

            var filePath = Helpers.FilePath;
            
            Task.Run(async () => await FileProvider.WriteAsync(data, filePath))
                .GetAwaiter().GetResult();

            Assert.IsTrue(Helpers.FileExist);
            Assert.AreEqual(GetExpectedResultFor(data), Helpers.ReadFromFile());
        }

        protected abstract byte[] GetExpectedResultFor(byte[] data);

        [TestCaseSource(nameof(FileProviderTestData))]
        public void FileProvider_Read(byte[] data)
        {
            Helpers.WriteToFile(PrepareDataForRead(data));

            Assert.AreEqual(data, FileProvider.Read(Helpers.FilePath));
        }

        [TestCaseSource(nameof(FileProviderTestData))]
        public void FileProvider_ReadAsync(byte[] data)
        {
            Helpers.WriteToFile(PrepareDataForRead(data));

            var filePath = Helpers.FilePath;
            var result = Task.Run(async () => await FileProvider.ReadAsync(filePath))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }

        protected abstract byte[] PrepareDataForRead(byte[] data);
    }
}