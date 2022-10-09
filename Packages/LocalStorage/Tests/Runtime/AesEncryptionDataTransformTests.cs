using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using LocalStorage.Encryption;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

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
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestsByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = AesDT.Apply(data);

            Assert.AreEqual(data, result.Decrypt());
        }

        [UnityTest]
        public IEnumerator DataTransform_ApplyAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var result = await AesDT.ApplyAsync(TestByteData);

                Assert.AreEqual(TestByteData, result.Decrypt());
            });

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestsByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var encrypted = data.Encrypt();

            var result = AesDT.Reverse(encrypted);

            Assert.AreEqual(data, result);
        }

        [UnityTest]
        public IEnumerator DataTransform_ReverseAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var encrypted = TestByteData.Encrypt();

                var result = await AesDT.ReverseAsync(encrypted);

                Assert.AreEqual(TestByteData, result);
            });
    }
}