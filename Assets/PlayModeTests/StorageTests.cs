using System;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class StorageTests
    {
        private static ISerializationProvider SerializationProvider =>
            new UnityJsonSerializationProvider();

        private static IFileProvider FileProvider =>
            new FileProvider();

        private static object[] _argumentNullExceptionCases =
        {
            new object[] {null, null},
            new object[] {SerializationProvider, null},
            new object[] {null, FileProvider},
        };

        [Test]
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