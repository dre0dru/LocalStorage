using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class FileProviderTests
    {
        private static readonly IFileProvider FileProvider = new FileProvider();

        private static IEnumerable<string> _argumentNullExceptionCases()
        {
            yield return null;
            yield return string.Empty;
        }

        [SetUp]
        public virtual void SetUp()
        {
            Setup.DeleteFile(Constants.FilePath);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Setup.DeleteFile(Constants.FilePath);
        }
        
        [Test]
        public void FileProvider_Delete()
        {
            Setup.CreateEmptyFile(Constants.FilePath);

            FileProvider.Delete(Constants.FileName);

            Assert.IsFalse(Setup.FileExists(Constants.FilePath));
        }

        [Test]
        public void FileProvider_GetFilePath()
        {
            Assert.AreEqual(Constants.FilePath, FileProvider.GetFilePath(Constants.FileName));
        }

        [Test]
        public void FileProvider_FileExists()
        {
            Assert.IsFalse(FileProvider.FileExists(Constants.FileName));
            
            Setup.CreateEmptyFile(Constants.FilePath);
            
            Assert.IsTrue(FileProvider.FileExists(Constants.FileName));
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void FileProvider_Write(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.FilePath));

            FileProvider.Write(data, Constants.FileName);

            Assert.IsTrue(Setup.FileExists(Constants.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.FilePath));
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void FileProvider_WriteAsync(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.FilePath));

            Task.Run(async () => await FileProvider.WriteAsync(data, Constants.FileName))
                .GetAwaiter().GetResult();

            Assert.IsTrue(Setup.FileExists(Constants.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.FilePath));
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void FileProvider_Read(byte[] data)
        {
            Setup.WriteToFile(Constants.FilePath, data);

            Assert.AreEqual(data, FileProvider.Read(Constants.FileName));
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void FileProvider_ReadAsync(byte[] data)
        {
            Setup.WriteToFile(Constants.FilePath, data);

            var result = Task.Run(async () => await FileProvider.ReadAsync(Constants.FileName))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }

        [TestCaseSource(nameof(_argumentNullExceptionCases))]
        public void FileProvider_ThrowsArgumentNullException(string filePath)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var fileProvider = new FileProvider(filePath);
            });
        }
    }
}