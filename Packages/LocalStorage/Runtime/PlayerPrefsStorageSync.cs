using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace LocalStorage
{
    public class PlayerPrefsStorageSync : IPlayerPrefsStorageSync
    {
        private readonly ISerializationProviderSync _serializationProvider;
        private readonly bool _autoSaveEnabled;

        [RequiredMember]
        public PlayerPrefsStorageSync(ISerializationProviderSync serializationProvider)
            : this(serializationProvider, false)
        {
        }

        public PlayerPrefsStorageSync(ISerializationProviderSync serializationProvider, bool autoSaveEnabled)
        {
            _serializationProvider = serializationProvider ??
                                     throw new ArgumentNullException(nameof(serializationProvider));
            _autoSaveEnabled = autoSaveEnabled;
        }

        public void SetData<T>(string key, T data) =>
            SetBytes(key, _serializationProvider.Serialize(data));

        public T GetData<T>(string key) =>
            _serializationProvider.Deserialize<T>(GetBytes(key));

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