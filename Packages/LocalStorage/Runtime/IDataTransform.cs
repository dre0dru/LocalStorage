using System.Threading.Tasks;

namespace LocalStorage
{
    public interface IDataTransform
    {
        byte[] Apply(byte[] data);
        
        Task<byte[]> ApplyAsync(byte[] data);

        byte[] Reverse(byte[] data);
        
        Task<byte[]> ReverseAsync(byte[] data);
    }
}