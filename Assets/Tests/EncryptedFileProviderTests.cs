using LocalStorage.Encryption;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class EncryptedFileProviderTests : FileProviderTestsBase
    {
        private static readonly IFileProvider _encryptedFileProvider =
            new EncryptedFileProvider(new FileProvider(), Helpers.Es);

        protected override IFileProvider FileProvider => _encryptedFileProvider;

        protected override byte[] GetExpectedResultFor(byte[] data)
        {
            return data.Encrypt();
        }

        protected override byte[] PrepareDataForRead(byte[] data)
        {
            return data.Encrypt();
        }
    }
}