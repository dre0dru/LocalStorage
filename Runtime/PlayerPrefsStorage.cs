using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage
{
    public class PlayerPrefsStorage : IPlayerPrefsStorage
    {
        private readonly ISerializationProvider _serializationProvider;
        private readonly bool _autoSaveEnabled;

        [RequiredMember]
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

        public Task SetDataAsync<T>(string key, T data) =>
            _serializationProvider.SerializeAsync(data)
                .ContinueWith(task => SetBytes(key, task.Result));

        public Task<T> GetDataAsync<T>(string key) =>
            _serializationProvider.DeserializeAsync<T>(GetBytes(key));

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

    public class PlayerPrefsStorage<T> : PlayerPrefsStorage, IPlayerPrefsStorage<T>
        where T : ISerializationProvider
    {
        [RequiredMember]
        public PlayerPrefsStorage(ISerializationProvider serializationProvider) : base(serializationProvider)
        {
        }

        public PlayerPrefsStorage(ISerializationProvider serializationProvider, bool autoSaveEnabled) : base(serializationProvider, autoSaveEnabled)
        {
        }
    }
}