using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class GZipDataTransformTests
    {
        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(TestsByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = GZipDT.Apply(data);

            Assert.AreEqual(data, result.ReadGzip());
        }

        [UnityTest]
        public IEnumerator DataTransform_ApplyAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var result =  await GZipDT.ApplyAsync(TestByteData);

                Assert.AreEqual(TestByteData, result.ReadGzip());
            });

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(TestsByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteGZip();

            var result = GZipDT.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [UnityTest]
        public IEnumerator DataTransform_ReverseAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var compressed = TestByteData.WriteGZip();

                var result = await GZipDT.ReverseAsync(compressed);

                Assert.AreEqual(TestByteData, result);
            });
    }
}