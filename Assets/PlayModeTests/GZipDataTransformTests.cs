using System.Threading.Tasks;
using LocalStorage.Compression;
using NUnit.Framework;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class GZipDataTransformTests
    {
        private static readonly IDataTransform DataTransform =
            new GZipDataTransform();

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = DataTransform.Apply(data);

            Assert.AreEqual(data, result.ReadGzip());
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await DataTransform.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.ReadGzip());
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteGZip();

            var result = DataTransform.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants), nameof(Constants.TestsData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var compressed = data.WriteGZip();

            var result = Task.Run(async () => await DataTransform.ReverseAsync(compressed))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}