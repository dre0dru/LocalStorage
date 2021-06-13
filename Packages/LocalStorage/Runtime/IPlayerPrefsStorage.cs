#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace LocalStorage
{
    public interface IPlayerPrefsStorage
    {
        void SetData<T>(string key, T data);

        T GetData<T>(string key);

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

    public interface IPlayerPrefsStorageAsync : IPlayerPrefsStorage
    {
        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask SetDataAsync<T>(string key, T data);
        #else
        Task SetDataAsync<T>(string key, T data);
        #endif

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        UniTask<T> GetDataAsync<T>(string key);
        #else
        Task<T> GetDataAsync<T>(string key);
        #endif
    }
}