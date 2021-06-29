using System;
using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using LocalStorage.Serialization;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class FileStorageTests
    {
        private static readonly object[] ArgumentNullExceptionCases =
        {
            new object[] {null, null},
            new object[] {UnityJsonSP, null},
            new object[] {null, FP},
        };

        private static readonly object[] StorageInstances =
        {
            new object[] {new FileStorage(UnityJsonSP, FP)},
            new object[] {new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, AesDT), FP)},
            new object[] {new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, DeflateDT), FP)},
            new object[] {new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, GZipDT), FP)},
        };

        [Test]
        [TestCaseSource(nameof(ArgumentNullExceptionCases))]
        public void Storage_ThrowsArgumentNullException(
            ISerializationProvider serializationProvider, IFileProvider fileProvider)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new FileStorage(serializationProvider, fileProvider);
            });
        }

        [Test]
        [TestCaseSource(nameof(StorageInstances))]
        public void Storage_SaveLoad(IFileStorage storage)
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                Setup.DeleteFile(FilePath);

                Assert.IsFalse(Setup.FileExists(FilePath));

                storage.Save(data, FileName);

                Assert.IsTrue(Setup.FileExists(FilePath));

                Assert.AreEqual(data, storage.Load<T>(FileName));
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);

            Setup.DeleteFile(FilePath);
        }

        [UnityTest]
        public IEnumerator Storage_SaveLoadAsync()
            => UniTask.ToCoroutine(async () =>
            {
                //Can't use [TestCase] or [TestCaseSource]
                //because of IL2CPP AOT compilation
                async UniTask Test<T>(T data, IFileStorage storage)
                {
                    Setup.DeleteFile(FilePath);

                    Assert.IsFalse(Setup.FileExists(FilePath));

                    await storage.SaveAsync(data, FileName);

                    Assert.IsTrue(Setup.FileExists(FilePath));

                    var result = await storage.LoadAsync<T>(FileName);

                    Assert.AreEqual(data, result);

                    Setup.DeleteFile(FilePath);
                }

                foreach (var storage in StorageInstances
                    .Select(o => (object[]) o)
                    .Select(objects => (IFileStorage) objects[0]))
                {
                    await Test(GenericDataVector, storage);
                    await Test(GenericDataStruct, storage);
                }
            });
    }
}