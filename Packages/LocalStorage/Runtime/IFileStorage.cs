using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IFileStorage
    {
        void Save<TData>(TData data, string fileName);

        Task SaveAsync<TData>(TData data, string fileName);

        TData Load<TData>(string fileName);

        Task<TData> LoadAsync<TData>(string fileName);
        
        bool Delete(string fileName);
        
        string GetFilePath(string fileName);
        
        bool FileExists(string fileName);
    }

    public interface IFileStorage<TSerialization, TFile> : IFileStorage
        where TSerialization : ISerializationProvider where TFile : IFileProvider
    {
        
    }
}