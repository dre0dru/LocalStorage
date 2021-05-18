using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IPlayerPrefsStorage
    {
        void SetData<T>(string key, T data);
        T GetData<T>(string key);
        Task SetDataAsync<T>(string key, T data);
        Task<T> GetDataAsync<T>(string key);
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
    
    public interface IPlayerPrefsStorage<TSerialization> : IPlayerPrefsStorage
        where TSerialization : ISerializationProvider
    {
    }
}