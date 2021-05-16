using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using LocalStorage.Compression;
using LocalStorage.Encryption;
using LocalStorage.Providers;
using NSubstitute;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class DataTransformSerializationProviderTests
    {
        private static readonly ISerializationProvider JsonSP = new UnityJsonSerializationProvider();

        private static IEnumerable<ISerializationProvider> SerializationProviders()
        {
            yield return new DataTransformSerializationProvider(JsonSP, Constants.AesDT);
            yield return new DataTransformSerializationProvider(JsonSP, Constants.DeflateDT);
            yield return new DataTransformSerializationProvider(JsonSP, Constants.GZipDT);
        }
        
        private static IEnumerable<object[]> _argumentNullExceptionCases()
        {
            yield return new object[] {null, null};
            yield return new object[] {Substitute.For<ISerializationProvider>(), null};
            yield return new object[] {null, Substitute.For<IDataTransform>()};
        }
        
        private static Vector2 Data => new Vector2();

        [TestCaseSource(nameof(_argumentNullExceptionCases))]
        public void SerializationProvider_ThrowsArgumentNullException(ISerializationProvider serializationProvider, 
            IDataTransform dataTransform)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = new DataTransformSerializationProvider(serializationProvider, dataTransform);
            });
        }
        
        [TestCaseSource(nameof(SerializationProviders))]
        public void SerializationProvider_SerializeDeserialize(ISerializationProvider serializationProvider)
        {
            var serialized = serializationProvider.Serialize(Data);
            var deserialized = serializationProvider.Deserialize<Vector2>(serialized);

            Assert.AreEqual(Data, deserialized);
        }
        
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