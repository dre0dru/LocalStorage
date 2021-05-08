using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IStorage
    {
        void Save<TData>(TData data, string fileName);

        Task SaveAsync<TData>(TData data, string fileName);

        TData Load<TData>(string fileName);

        Task<TData> LoadAsync<TData>(string fileName);
        
        bool Delete(string fileName);
        
        string GetFilePath(string fileName);
        
        bool FileExists(string fileName);
    }

    public interface IStorage<TSerialization, TFile> : IStorage
        where TSerialization : ISerializationProvider where TFile : IFileProvider
    {
        
    }
}