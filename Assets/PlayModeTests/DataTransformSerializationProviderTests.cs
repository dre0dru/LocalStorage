using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using LocalStorage.Serialization;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

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
            void Test<T>(T data)
            {
                var serialized = serializationProvider.Serialize(data);
                var deserialized = serializationProvider.Deserialize<T>(serialized);

                Assert.AreEqual(data, deserialized);
            }
            
            Test(GenericDataVector);
            Test(GenericDataStruct);
        }

        [UnityTest]
        public IEnumerator SerializationProvider_SerializeDeserializeAsync()
            => UniTask.ToCoroutine(async () =>
            {
                async UniTask Test<T>(T data, ISerializationProvider serializationProvider)
                {
                    var serialized =await serializationProvider.SerializeAsync(data);
                    var deserialized = await serializationProvider.DeserializeAsync<T>(serialized);

                    Assert.AreEqual(data, deserialized);
                }

                foreach (var serializationProvider in _serializationProviders
                    .Select(o => (object[]) o)
                    .Select(objects => (ISerializationProvider) objects[0]))
                {
                    await Test(GenericDataVector, serializationProvider);
                    await Test(GenericDataStruct, serializationProvider);
                }
            });
    }
}