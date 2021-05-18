using System.Threading.Tasks;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class GZipDataTransformTests
    {
        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = GZipDT.Apply(data);

            Assert.AreEqual(data, result.ReadGzip());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await GZipDT.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.ReadGzip());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteGZip();

            var result = GZipDT.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var compressed = data.WriteGZip();

            var result = Task.Run(async () => await GZipDT.ReverseAsync(compressed))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}