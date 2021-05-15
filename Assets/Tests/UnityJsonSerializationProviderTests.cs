using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;
using UnityEngine;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class UnityJsonSerializationProviderTests
    {
        private readonly ISerializationProvider _serializationProvider
            = new UnityJsonSerializationProvider();

        private static Vector2 Data => new Vector2();

        [Test]
        public void SerializationProvider_Serialize()
        {
            var result = _serializationProvider.Serialize(Data);

            Assert.AreEqual(Data.ToJson().ToBytes(), result);

            var resultJson = result.FromBytes();

            Assert.AreEqual(Data.ToJson(), resultJson);
        }

        [Test]
        public void SerializationProvider_SerializeAsync()
        {
            var result = Task.Run(async () => await _serializationProvider.SerializeAsync(Data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(Data.ToJson().ToBytes(), result);

            var resultJson = result.FromBytes();

            Assert.AreEqual(Data.ToJson(), resultJson);
        }

        [Test]
        public void SerializationProvider_Deserialize()
        {
            var json = Data.ToJson();
            var bytes = json.ToBytes();

            var result = _serializationProvider.Deserialize<Vector2>(bytes);

            Assert.AreEqual(Data, result);
        }

        [Test]
        public void SerializationProvider_DeserializeAsync()
        {
            var json = Data.ToJson();
            var bytes = json.ToBytes();

            var result = Task.Run(async () => await _serializationProvider.DeserializeAsync<Vector2>(bytes))
                .GetAwaiter().GetResult();

            Assert.AreEqual(Data, result);
        }
    }
}