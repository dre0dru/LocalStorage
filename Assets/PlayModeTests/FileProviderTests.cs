using System.Threading.Tasks;
using NUnit.Framework;
using static LocalStorage.PlayModeTests.Constants.Instances;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class FileProviderTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            Setup.DeleteFile(Constants.Data.FilePath);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Setup.DeleteFile(Constants.Data.FilePath);
        }
        
        [Test]
        public void FileProvider_Delete()
        {
            Setup.CreateEmptyFile(Constants.Data.FilePath);

            FP.Delete(Constants.Data.FileName);

            Assert.IsFalse(Setup.FileExists(Constants.Data.FilePath));
        }

        [Test]
        public void FileProvider_GetFilePath()
        {
            Assert.AreEqual(Constants.Data.FilePath, FP.GetFilePath(Constants.Data.FileName));
        }

        [Test]
        public void FileProvider_FileExists()
        {
            Assert.IsFalse(FP.FileExists(Constants.Data.FileName));
            
            Setup.CreateEmptyFile(Constants.Data.FilePath);
            
            Assert.IsTrue(FP.FileExists(Constants.Data.FileName));
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void FileProvider_Write(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.Data.FilePath));

            FP.Write(data, Constants.Data.FileName);

            Assert.IsTrue(Setup.FileExists(Constants.Data.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.Data.FilePath));
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void FileProvider_WriteAsync(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(Constants.Data.FilePath));

            Task.Run(async () => await FP.WriteAsync(data, Constants.Data.FileName))
                .GetAwaiter().GetResult();

            Assert.IsTrue(Setup.FileExists(Constants.Data.FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(Constants.Data.FilePath));
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void FileProvider_Read(byte[] data)
        {
            Setup.WriteToFile(Constants.Data.FilePath, data);

            Assert.AreEqual(data, FP.Read(Constants.Data.FileName));
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(Constants.Data.TestByteData))]
        public void FileProvider_ReadAsync(byte[] data)
        {
            Setup.WriteToFile(Constants.Data.FilePath, data);

            var result = Task.Run(async () => await FP.ReadAsync(Constants.Data.FileName))
                .GetAwaiter().GetResult();

            Assert.AreEqual(data, result);
        }
    }
}