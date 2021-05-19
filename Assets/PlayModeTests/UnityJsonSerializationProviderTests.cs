using System.Threading.Tasks;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class UnityJsonSerializationProviderTests
    {
        [Test]
        public void SerializationProvider_Serialize()
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                var result = UnityJsonSP.Serialize(data);

                Assert.AreEqual(data.ToJson().ToBytes(), result);

                var resultJson = result.FromBytes();

                Assert.AreEqual(data.ToJson(), resultJson);
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);
        }

        [Test]
        public void SerializationProvider_SerializeAsync()
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                var result = Task.Run(async () => await UnityJsonSP.SerializeAsync(data))
                    .GetAwaiter().GetResult();

                Assert.AreEqual(data.ToJson().ToBytes(), result);

                var resultJson = result.FromBytes();

                Assert.AreEqual(data.ToJson(), resultJson);
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);
        }

        [Test]
        public void SerializationProvider_Deserialize()
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                var json = data.ToJson();
                var bytes = json.ToBytes();

                var result = UnityJsonSP.Deserialize<T>(bytes);

                Assert.AreEqual(data, result);
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);
        }

        [Test]
        public void SerializationProvider_DeserializeAsync()
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                var json = data.ToJson();
                var bytes = json.ToBytes();

                var result = Task.Run(async () => await UnityJsonSP.DeserializeAsync<T>(bytes))
                    .GetAwaiter().GetResult();

                Assert.AreEqual(data, result);
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);
        }
    }
}