using System.Threading.Tasks;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class UnityJsonSerializationProviderTests
    {
        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestGenericData))]
        public void SerializationProvider_Serialize<T>(T data)
        {
            var result = UnityJsonSP.Serialize(data);

            Assert.AreEqual(data.ToJson().ToBytes(), result);

            var resultJson = result.FromBytes();

            Assert.AreEqual(data.ToJson(), resultJson);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestGenericData))]
        public void SerializationProvider_SerializeAsync<T>(T data)
        {
            var result = Task.Run(async () => await UnityJsonSP.SerializeAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data.ToJson().ToBytes(), result);

            var resultJson = result.FromBytes();

            Assert.AreEqual(data.ToJson(), resultJson);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestGenericData))]
        public void SerializationProvider_Deserialize<T>(T data)
        {
            var json = data.ToJson();
            var bytes = json.ToBytes();

            var result = UnityJsonSP.Deserialize<T>(bytes);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestGenericData))]
        public void SerializationProvider_DeserializeAsync<T>(T data)
        {
            var json = data.ToJson();
            var bytes = json.ToBytes();

            var result = Task.Run(async () => await UnityJsonSP.DeserializeAsync<T>(bytes))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}