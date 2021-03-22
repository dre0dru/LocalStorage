using System;
using System.Collections.Generic;
using LocalStorage.Encryption;
using LocalStorage.Providers;
using NSubstitute;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class EncryptedFileProviderTests : FileProviderTestsBase
    {
        private static readonly IFileProvider _encryptedFileProvider =
            new EncryptedFileProvider(new FileProvider(), Helpers.Es);

        protected override IFileProvider FileProvider => _encryptedFileProvider;

        private static IEnumerable<object[]> _argumentNullExceptionCases()
        {
            yield return new object[] {null, null};
            yield return new object[] {Substitute.For<IFileProvider>(), null};
            yield return new object[] {null, Substitute.For<IEncryptionSettings>()};
        }

        protected override byte[] GetExpectedResultFor(byte[] data)
        {
            return data.Encrypt();
        }

        protected override byte[] PrepareDataForRead(byte[] data)
        {
            return data.Encrypt();
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
    }
}