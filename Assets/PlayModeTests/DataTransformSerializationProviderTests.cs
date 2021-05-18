using System;
using System.Numerics;
using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class DataTransformSerializationProviderTests
    {

        private static object[] _serializationProviders =
        {
            new object[] {new DataTransformSerializationProvider(UnityJsonSP, AesDT)},
            new object[] {new DataTransformSerializationProvider(UnityJsonSP, DeflateDT)},
            new object[] {new DataTransformSerializationProvider(UnityJsonSP, GZipDT)},
        };

        private static object[] _argumentNullExceptionCases =
        {
            new object[] {null, null},
            new object[] {UnityJsonSP, null},
            new object[] {null, AesDT},
        };

        private static Vector2 Data => new Vector2();

        [Test]
        [TestCaseSource(nameof(_argumentNullExceptionCases))]
        public void SerializationProvider_ThrowsArgumentNullException(ISerializationProvider serializationProvider,
            IDataTransform dataTransform)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = new DataTransformSerializationProvider(serializationProvider, dataTransform);
            });
        }

        [Test]
        [TestCaseSource(nameof(_serializationProviders))]
        public void SerializationProvider_SerializeDeserialize(ISerializationProvider serializationProvider)
        {
            var serialized = serializationProvider.Serialize(Data);
            var deserialized = serializationProvider.Deserialize<Vector2>(serialized);

            Assert.AreEqual(Data, deserialized);
        }

        [Test]
        [TestCaseSource(nameof(_serializationProviders))]
        public void SerializationProvider_SerializeDeserializeAsync(ISerializationProvider serializationProvider)
        {
            var serialized = Task.Run(async () => await serializationProvider.SerializeAsync(Data))
                .GetAwaiter().GetResult();
            var deserialized = Task.Run(async () => await serializationProvider.DeserializeAsync<Vector2>(serialized))
                .GetAwaiter().GetResult();

            Assert.AreEqual(Data, deserialized);
        }
    }
}