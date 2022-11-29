namespace Dre0Dru.LocalStorage
{
    public interface IPlayerPrefsStorage : IPlayerPrefsStorageSync, IPlayerPrefsStorageAsync
    {
    }

    public interface IPlayerPrefsStorageSync : IPlayerPrefsStorageCommon
    {
        void SetData<T>(string key, T data);

        T GetData<T>(string key);
    }

    public interface IPlayerPrefsStorageAsync : IPlayerPrefsStorageCommon
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask SetDataAsync<T>(string key, T data);
        #else
        System.Threading.Tasks.Task SetDataAsync<T>(string key, T data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        Cysharp.Threading.Tasks.UniTask<T> GetDataAsync<T>(string key);
        #else
        System.Threading.Tasks.Task<T> GetDataAsync<T>(string key);
        #endif
    }

    public interface IPlayerPrefsStorageCommon
    {
        void SetFloat(string key, float value);

        float GetFloat(string key);

        void SetInt(string key, int value);

        int GetInt(string key);

        void SetString(string key, string value);

        string GetString(string key);

        bool HasKey(string key);

        void Save();

        void DeleteKey(string key);

        void DeleteAll();
    }
}