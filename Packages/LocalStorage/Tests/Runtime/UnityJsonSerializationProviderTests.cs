using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
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

        [UnityTest]
        public IEnumerator SerializationProvider_SerializeAsync()
            => UniTask.ToCoroutine(async () =>
            {
                //Can't use [TestCase] or [TestCaseSource]
                //because of IL2CPP AOT compilation
                async Task Test<T>(T data)
                {
                    var result = await UnityJsonSP.SerializeAsync(data);

                    Assert.AreEqual(data.ToJson().ToBytes(), result);

                    var resultJson = result.FromBytes();

                    Assert.AreEqual(data.ToJson(), resultJson);
                }

                await Test(GenericDataVector);
                await Test(GenericDataStruct);
            });

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

        [UnityTest]
        public IEnumerator SerializationProvider_DeserializeAsync()
            => UniTask.ToCoroutine(async () =>
            {
                //Can't use [TestCase] or [TestCaseSource]
                //because of IL2CPP AOT compilation
                async Task Test<T>(T data)
                {
                    var json = data.ToJson();
                    var bytes = json.ToBytes();

                    var result = await UnityJsonSP.DeserializeAsync<T>(bytes);

                    Assert.AreEqual(data, result);
                }

                await Test(GenericDataVector);
                await Test(GenericDataStruct);
            });
    }
}