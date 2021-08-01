using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif
using UnityEngine;

namespace LocalStorage
{
    public class PlayerPrefsStorage : IPlayerPrefsStorage
    {
        private readonly ISerializationProvider _serializationProvider;
        private readonly bool _autoSaveEnabled;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public PlayerPrefsStorage(ISerializationProvider serializationProvider)
            : this(serializationProvider, false)
        {
        }

        public PlayerPrefsStorage(ISerializationProvider serializationProvider, bool autoSaveEnabled)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _autoSaveEnabled = autoSaveEnabled;
        }

        public void SetData<T>(string key, T data) =>
            SetBytes(key, _serializationProvider.Serialize(data));

        public T GetData<T>(string key) =>
            _serializationProvider.Deserialize<T>(GetBytes(key));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask SetDataAsync<T>(string key, T data) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(bytes => SetBytes(key, bytes));
        #else
        public Task SetDataAsync<T>(string key, T data) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(task => SetBytes(key, task.Result), TaskScheduler.FromCurrentSynchronizationContext());
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<T> GetDataAsync<T>(string key) =>
            _serializationProvider.DeserializeAsync<T>(GetBytes(key));
        #else
        public Task<T> GetDataAsync<T>(string key) =>
            _serializationProvider.DeserializeAsync<T>(GetBytes(key));
        #endif

        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            AttemptAutoSave();
        }

        public float GetFloat(string key) =>
            PlayerPrefs.GetFloat(key);

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            AttemptAutoSave();
        }

        public int GetInt(string key) =>
            PlayerPrefs.GetInt(key);

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            AttemptAutoSave();
        }

        public string GetString(string key) =>
            PlayerPrefs.GetString(key);

        public bool HasKey(string key) =>
            PlayerPrefs.HasKey(key);

        public void Save() =>
            PlayerPrefs.Save();

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
            AttemptAutoSave();
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            AttemptAutoSave();
        }

        private void AttemptAutoSave()
        {
            if (_autoSaveEnabled)
            {
                Save();
            }
        }

        private void SetBytes(string key, byte[] data) =>
            SetString(key, Convert.ToBase64String(data));

        private byte[] GetBytes(string key) =>
            Convert.FromBase64String(GetString(key));
    }
}