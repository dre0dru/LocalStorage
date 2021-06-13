using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using static LocalStorage.PlayModeTests.Constants.Instances;
using static LocalStorage.PlayModeTests.Constants.Data;

namespace LocalStorage.PlayModeTests
{
    [TestFixture]
    public class PlayerPrefsStorageTests
    {
        private static readonly IPlayerPrefsStorageAsync Storage =
            new PlayerPrefsStorage(UnityJsonSP, true);

        [SetUp]
        public virtual void SetUp()
        {
            Setup.ClearPlayerPrefs();
        }

        [TearDown]
        public virtual void TearDown()
        {
            Setup.ClearPlayerPrefs();
        }

        [Test]
        public void Storage_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var storage = new PlayerPrefsStorage(null);
            });
        }

        [Test]
        public void Storage_GetSetData()
        {
            //Can't use [TestCase] or [TestCaseSource]
            //because of IL2CPP AOT compilation
            void Test<T>(T data)
            {
                Setup.ClearPlayerPrefs();
                
                Assert.IsFalse(Storage.HasKey(DataKey));

                Storage.SetData(DataKey, data);

                Assert.IsTrue(Storage.HasKey(DataKey));
                Assert.AreEqual(data, Storage.GetData<T>(DataKey));
                
                Setup.ClearPlayerPrefs();
            }

            Test(GenericDataVector);
            Test(GenericDataStruct);
        }

        [UnityTest]
        public IEnumerator Storage_GetSetDataAsync() =>
            UniTask.ToCoroutine(async () =>
            {
                //Can't use [ValueSource]
                async UniTask Test<T>(T data)
                {
                    Setup.ClearPlayerPrefs();
                    
                    Assert.IsFalse(Storage.HasKey(DataKey));

                    await Storage.SetDataAsync(DataKey, data);
                    var result = await Storage.GetDataAsync<T>(DataKey);

                    Assert.IsTrue(Storage.HasKey(DataKey));
                    Assert.AreEqual(data, result);
                    
                    Setup.ClearPlayerPrefs();
                }

                await Test(GenericDataVector);
                await Test(GenericDataStruct);
            });

        [Test]
        [TestCase(1.0f)]
        public void Storage_GetSetFloat(float data)
        {
            Assert.IsFalse(Storage.HasKey(DataKey));

            Storage.SetFloat(DataKey, data);

            Assert.IsTrue(Storage.HasKey(DataKey));
            Assert.AreEqual(data, Storage.GetFloat(DataKey));
        }

        [Test]
        [TestCase(1)]
        public void Storage_GetSetInt(int data)
        {
            Assert.IsFalse(Storage.HasKey(DataKey));

            Storage.SetInt(DataKey, data);

            Assert.IsTrue(Storage.HasKey(DataKey));
            Assert.AreEqual(data, Storage.GetInt(DataKey));
        }

        [Test]
        [TestCase("string")]
        public void Storage_GetSetString(string data)
        {
            Assert.IsFalse(Storage.HasKey(DataKey));

            Storage.SetString(DataKey, data);

            Assert.IsTrue(Storage.HasKey(DataKey));
            Assert.AreEqual(data, Storage.GetString(DataKey));
        }

        [Test]
        public void Storage_DeleteKey()
        {
            Setup.PlayerPrefsCreateKey(DataKey);
            Assert.IsTrue(Storage.HasKey(DataKey));

            Storage.DeleteKey(DataKey);

            Assert.IsFalse(Storage.HasKey(DataKey));
        }

        [Test]
        public void Storage_DeleteAll()
        {
            Setup.PlayerPrefsCreateKey(DataKey);
            Assert.IsTrue(Storage.HasKey(DataKey));

            Storage.DeleteAll();

            Assert.IsFalse(Storage.HasKey(DataKey));
        }
    }
}