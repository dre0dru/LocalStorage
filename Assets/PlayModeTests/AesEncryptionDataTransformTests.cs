using System;
using System.Threading.Tasks;
using LocalStorage.Encryption;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class AesEncryptionDataTransformTests
    {
       

        [Test]
        public void DataTransform_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var transform = new AesEncryptionDataTransform(null);
            });
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = AesDT.Apply(data);

            Assert.AreEqual(data, result.Decrypt());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await AesDT.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.Decrypt());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var encrypted = data.Encrypt();

            var result = AesDT.Reverse(encrypted);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var encrypted = data.Encrypt();

            var result = Task.Run(async () => await AesDT.ReverseAsync(encrypted))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}