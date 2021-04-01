using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStorage.Encryption;
using LocalStorage.Providers;
using NSubstitute;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class EncryptedFileProviderTests
    {
        private static readonly IFileProvider FileProvider =
            new EncryptedFileProvider(new FileProvider(), Constants.Es);

        private static IEnumerable<object[]> _argumentNullExceptionCases()
        {
            yield return new object[] {null, null};
            yield return new object[] {Substitute.For<IFileProvider>(), null};
            yield return new object[] {null, Substitute.For<IEncryptionSettings>()};
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

        [TestCaseSource(nameof(_argumentNullExceptionCases))]
        public void EncryptedFileProvider_ThrowsArgumentNullException(IFileProvider fileProvider,
            IEncryptionSettings encryptionSettings)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var provider = new EncryptedFileProvider(fileProvider, encryptionSettings);
            });
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void EncryptedFileProvider_Write(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.FilePath));

            FileProvider.Write(data, Constants.FileName);

            Assert.IsTrue(Setup.FileExists(Constants.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.FilePath).Decrypt());
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void EncryptedFileProvider_WriteAsync(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.FilePath));

            Task.Run(async () => await FileProvider.WriteAsync(data, Constants.FileName))
                .GetAwaiter().GetResult();

            Assert.IsTrue(Setup.FileExists(Constants.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.FilePath).Decrypt());
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void EncryptedFileProvider_Read(byte[] data)
        {
            Setup.WriteToFile(Constants.FilePath, data.Encrypt());

            Assert.AreEqual(data, FileProvider.Read(Constants.FileName));
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.FileProviderTestData))]
        public void EncryptedFileProvider_ReadAsync(byte[] data)
        {
            Setup.WriteToFile(Constants.FilePath, data.Encrypt());

            var result = Task.Run(async () => await FileProvider.ReadAsync(Constants.FileName))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}