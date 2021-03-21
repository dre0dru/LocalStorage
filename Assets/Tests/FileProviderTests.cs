using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class FileProviderTests : FileProviderTestsBase
    {
        private static readonly IFileProvider _fileProvider = new FileProvider();
        protected override IFileProvider FileProvider => _fileProvider;
        
        protected override byte[] GetExpectedResultFor(byte[] data)
        {
            return data;
        }

        protected override byte[] PrepareDataForRead(byte[] data)
        {
            return data;
        }
    }
}