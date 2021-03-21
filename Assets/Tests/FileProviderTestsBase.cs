using System.Collections.Generic;
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

        protected abstract byte[] GetExpectedResultFor(byte[] data);

        [TestCaseSource(nameof(FileProviderTestData))]
        public void FileProvider_Read(byte[] data)
        {
            Helpers.WriteToFile(PrepareDataForRead(data));

            Assert.AreEqual(data, FileProvider.Read(Helpers.FilePath));
        }

        protected abstract byte[] PrepareDataForRead(byte[] data);
    }
}