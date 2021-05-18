using System;
using System.Numerics;
using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class DataTransformSerializationProviderTests
    {
        private static readonly ISerializationProvider JsonSP = new UnityJsonSerializationProvider();

        private static object[] SerializationProviders =
        {
            new object[] {new DataTransformSerializationProvider(JsonSP, Constants.AesDT)},
            new object[] {new DataTransformSerializationProvider(JsonSP, Constants.DeflateDT)},
            new object[] {new DataTransformSerializationProvider(JsonSP, Constants.GZipDT)},
        };

        private static object[] _argumentNullExceptionCases =
        {
            new object[] {null, null},
            new object[] {JsonSP, null},
            new object[] {null, Constants.AesDT},
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
        [TestCaseSource(nameof(SerializationProviders))]
        public void SerializationProvider_SerializeDeserialize(ISerializationProvider serializationProvider)
        {
            var serialized = serializationProvider.Serialize(Data);
            var deserialized = serializationProvider.Deserialize<Vector2>(serialized);

            Assert.AreEqual(Data, deserialized);
        }

        [Test]
        [TestCaseSource(nameof(SerializationProviders))]
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