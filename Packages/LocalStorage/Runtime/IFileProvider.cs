namespace LocalStorage
{
    public interface IFileProvider
    {
        void Write(byte[] output, string filePath);

        byte[] Read(string filePath);
    }
}