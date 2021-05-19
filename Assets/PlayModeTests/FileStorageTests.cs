using System;
using System.Threading.Tasks;
using LocalStorage.Providers;
using NUnit.Framework;
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
            new object[] { new FileStorage(UnityJsonSP, FP)},
            new object[] { new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, AesDT), FP)},
            new object[] { new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, DeflateDT), FP)},
            new object[] { new FileStorage(new DataTransformSerializationProvider(UnityJsonSP, GZipDT), FP)},
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
        
        [Test]
        [TestCaseSource(nameof(StorageInstances))]
        public void Storage_SaveLoadAsync(IFileStorage storage)
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                Setup.DeleteFile(FilePath);
                
                Assert.IsFalse(Setup.FileExists(FilePath));

                Task.Run(async () => await storage.SaveAsync(data, FileName))
                    .GetAwaiter().GetResult();
                
                Assert.IsTrue(Setup.FileExists(FilePath));

                var result =  Task.Run(async () => await storage.LoadAsync<T>(FileName))
                    .GetAwaiter().GetResult();

                Assert.AreEqual(data, result);
            }
            
            Test(GenericDataVector);
            Test(GenericDataStruct);
            
            Setup.DeleteFile(FilePath);
        }
    }
}