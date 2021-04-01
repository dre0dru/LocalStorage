using System;
using NSubstitute;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class StorageTests
    {
        private static ISerializationProvider SerializationProvider
        {
            get
            {
                var sub = Substitute.For<ISerializationProvider>();
                return sub;
            }
        }

        private static IFileProvider FileProvider
            => Substitute.For<IFileProvider>();

        private static object[] _argumentNullExceptionCases =
        {
            new object[] {null, null},
            new object[] {SerializationProvider, null},
            new object[] {null, FileProvider},
        };

        [TestCaseSource(nameof(_argumentNullExceptionCases))]
        public void Storage_ThrowsArgumentNullException(
            ISerializationProvider serializationProvider, IFileProvider fileProvider)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new Storage(serializationProvider, fileProvider);
            });
        }
    }
}