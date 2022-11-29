using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class FileProviderTests
    {
        [SetUp]
        public virtual void SetUp()
        {
            Setup.DeleteFile(FilePath);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Setup.DeleteFile(FilePath);
        }
        
        [Test]
        public void FileProvider_Delete()
        {
            Setup.CreateEmptyFile(FilePath);

            FP.Delete(FileName);

            Assert.IsFalse(Setup.FileExists(FilePath));
        }

        [Test]
        public void FileProvider_GetFilePath()
        {
            Assert.AreEqual(FilePath, FP.GetFilePath(FileName));
        }

        [Test]
        public void FileProvider_FileExists()
        {
            Assert.IsFalse(FP.FileExists(FileName));
            
            Setup.CreateEmptyFile(FilePath);
            
            Assert.IsTrue(FP.FileExists(FileName));
        }

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(TestsByteData))]
        public void FileProvider_Write(byte[] data)
        {
            Assert.IsFalse(Setup.FileExists(FilePath));

            FP.Write(data, FileName);

            Assert.IsTrue(Setup.FileExists(FilePath));
            Assert.AreEqual(data, Setup.ReadFromFile(FilePath));
        }

        [UnityTest]
        public IEnumerator FileProvider_WriteAsync()
            => UniTask.ToCoroutine(async () =>
            {
                Assert.IsFalse(Setup.FileExists(FilePath));

                await FP.WriteAsync(TestByteData, FileName);

                Assert.IsTrue(Setup.FileExists(FilePath));
                Assert.AreEqual(TestByteData, Setup.ReadFromFile(FilePath));
            });

        [Test]
        [TestCaseSource(typeof(Constants.Data), nameof(TestsByteData))]
        public void FileProvider_Read(byte[] data)
        {
            Setup.WriteToFile(FilePath, data);

            Assert.AreEqual(data, FP.Read(FileName));
        }

        [UnityTest]
        public IEnumerator FileProvider_ReadAsync()
            => UniTask.ToCoroutine(async () =>
            {
                Setup.WriteToFile(FilePath, TestByteData);

                var result = await FP.ReadAsync(FileName);

                Assert.AreEqual(TestByteData, result);
            });
    }
}