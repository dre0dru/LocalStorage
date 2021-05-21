using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class CombinedDataTransformTests
    {
        private static object[] _dataTransforms =
        {
            new object[] {new CombinedDataTransform(AesDT, DeflateDT)},
            new object[] {new CombinedDataTransform(DeflateDT, AesDT)},
            new object[] {new CombinedDataTransform(AesDT, GZipDT)},
            new object[] {new CombinedDataTransform(GZipDT, AesDT)},
        };

        [Test]
        [TestCaseSource(nameof(_dataTransforms))]
        public void DataTransform_ApplyReverse(IDataTransform dataTransform)
        {
            var applied = dataTransform.Apply(TestByteData);
            var result = dataTransform.Reverse(applied);

            Assert.AreEqual(TestByteData, result);
        }

        [UnityTest]
        public IEnumerator DataTransform_ApplyReverseAsync()
            => UniTask.ToCoroutine(async () =>
            {
                async UniTask Test(byte[] data, IDataTransform dataTransform)
                {
                    var applied = await dataTransform.ApplyAsync(data);
                    var result = await dataTransform.ReverseAsync(applied);

                    Assert.AreEqual(data, result);
                }

                foreach (var serializationProvider in _dataTransforms
                    .Select(o => (object[]) o)
                    .Select(objects => (IDataTransform) objects[0]))
                {
                    await Test(TestByteData, serializationProvider);
                }
            });
    }
}