using System;
using System.Threading.Tasks;
using LocalStorage.Encryption;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class AesEncryptionDataTransformTests
    {
        private static readonly IDataTransform DataTransform =
            new AesEncryptionDataTransform(Constants.Es);

        [Test]
        public void DataTransform_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var transform = new AesEncryptionDataTransform(null);
            });
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = DataTransform.Apply(data);

            Assert.AreEqual(data, result.Decrypt());
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await DataTransform.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.Decrypt());
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var encrypted = data.Encrypt();

            var result = DataTransform.Reverse(encrypted);

            Assert.AreEqual(data, result);
        }

        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var encrypted = data.Encrypt();

            var result = Task.Run(async () => await DataTransform.ReverseAsync(encrypted))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}