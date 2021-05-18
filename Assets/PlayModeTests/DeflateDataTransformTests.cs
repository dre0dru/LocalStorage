using System.Threading.Tasks;
using LocalStorage.Compression;
using NUnit.Framework;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class DeflateDataTransformTests
    {
        private static readonly IDataTransform DataTransform =
            new DeflateDataTransform();

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = DataTransform.Apply(data);

            Assert.AreEqual(data, result.ReadDeflate());
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await DataTransform.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.ReadDeflate());
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteDeflate();

            var result = DataTransform.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var compressed = data.WriteDeflate();

            var result = Task.Run(async () => await DataTransform.ReverseAsync(compressed))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}