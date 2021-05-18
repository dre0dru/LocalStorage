using System.Threading.Tasks;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class DeflateDataTransformTests
    {
        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Apply(byte[] data)
        {
            var result = DeflateDT.Apply(data);

            Assert.AreEqual(data, result.ReadDeflate());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ApplyAsync(byte[] data)
        {
            var result = Task.Run(async () => await DeflateDT.ApplyAsync(data))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result.ReadDeflate());
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_Reverse(byte[] data)
        {
            var compressed = data.WriteDeflate();

            var result = DeflateDT.Reverse(compressed);

            Assert.AreEqual(data, result);
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void DataTransform_ReverseAsync(byte[] data)
        {
            var compressed = data.WriteDeflate();

            var result = Task.Run(async () => await DeflateDT.ReverseAsync(compressed))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}