using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class CombinedDataTransformTests
    {
        private static object[] DataTransforms =
        {
            new object[] {new CombinedDataTransform(Constants.AesDT, Constants.DeflateDT)},
            new object[] {new CombinedDataTransform(Constants.DeflateDT, Constants.AesDT)},
            new object[] {new CombinedDataTransform(Constants.AesDT, Constants.GZipDT)},
            new object[] {new CombinedDataTransform(Constants.GZipDT, Constants.AesDT)},
        };

        [Test]
        [TestCaseSource(nameof(DataTransforms))]
        public void DataTransform_ApplyReverse(IDataTransform dataTransform)
        {
            var data = Constants.TestData.ToBytes();
            var applied = dataTransform.Apply(data);
            var result = dataTransform.Reverse(applied);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(nameof(DataTransforms))]
        public void DataTransform_ApplyReverseAsync(IDataTransform dataTransform)
        {
            var data = Constants.TestData.ToBytes();
            var applied = Task.Run(async () => await dataTransform.ApplyAsync(data))
                .GetAwaiter().GetResult();
            var result = Task.Run(async () => await dataTransform.ReverseAsync(applied))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}