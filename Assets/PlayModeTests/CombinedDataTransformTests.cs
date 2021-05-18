using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

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
            var data = Constants.Data.LoremIpsum.ToBytes();
            var applied = dataTransform.Apply(data);
            var result = dataTransform.Reverse(applied);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(nameof(_dataTransforms))]
        public void DataTransform_ApplyReverseAsync(IDataTransform dataTransform)
        {
            var data = Constants.Data.LoremIpsum.ToBytes();
            var applied = Task.Run(async () => await dataTransform.ApplyAsync(data))
                .GetAwaiter().GetResult();
            var result = Task.Run(async () => await dataTransform.ReverseAsync(applied))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}