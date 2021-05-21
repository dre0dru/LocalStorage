using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class DeflateDataTransformTests
    {
        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestsByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = DeflateDT.Apply(data);

            Assert.AreEqual(data, result.ReadDeflate());
        }

        [UnityTest]
        public IEnumerator DataTransform_ApplyAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var result = await DeflateDT.ApplyAsync(TestByteData);

                Assert.AreEqual(TestByteData, result.ReadDeflate());
            });

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestsByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteDeflate();

            var result = DeflateDT.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [UnityTest]
        public IEnumerator DataTransform_ReverseAsync()
            => UniTask.ToCoroutine(async () =>
            {
                var compressed = TestByteData.WriteDeflate();
                
                var result = await DeflateDT.ReverseAsync(compressed);

                Assert.AreEqual(TestByteData, result);
            });
    }
}