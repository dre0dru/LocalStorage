using System.Collections.Generic;
using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;

namespace LocalStorage.Tests
{
    [TestFixture]
    public class CombinedDataTransformTests
    {
        private static IEnumerable<IDataTransform> DataTransforms()
        {
            yield return new CombinedDataTransform(Constants.AesDT, Constants.DeflateDT);
            yield return new CombinedDataTransform(Constants.DeflateDT, Constants.AesDT);
            yield return new CombinedDataTransform(Constants.AesDT, Constants.GZipDT);
            yield return new CombinedDataTransform(Constants.GZipDT, Constants.AesDT);
        }

        [TestCaseSource(nameof(DataTransforms))]
        public void DataTransform_ApplyReverse(IDataTransform dataTransform)
        {
            var data = Constants.TestData.ToBytes();
            var applied = dataTransform.Apply(data);
            var result = dataTransform.Reverse(applied);

            Assert.AreEqual(data, result);
        }

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