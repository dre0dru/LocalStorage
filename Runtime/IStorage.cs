namespace LocalStorage
{
    public interface IStorage
    {
        void Save<TData>(TData data, string fileName);

        TData Load<TData>(string fileName);

        bool Delete(string fileName);

        string GetFilePath(string fileName);

        bool FileExists(string fileName);
    }
}
