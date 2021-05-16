using System;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace LocalStorage.Providers
{
    public class CombinedDataTransform : IDataTransform
    {
        private readonly IDataTransform _firstTransform;
        private readonly IDataTransform _secondTransform;

        [RequiredMember]
        public CombinedDataTransform(IDataTransform firstTransform, IDataTransform secondTransform)
        {
            _firstTransform = firstTransform ??
                              throw new ArgumentNullException(nameof(firstTransform));
            _secondTransform = secondTransform ??
                               throw new ArgumentNullException(nameof(secondTransform));
        }

        public byte[] Apply(byte[] data) =>
            _secondTransform.Apply(_firstTransform.Apply(data));

        public Task<byte[]> ApplyAsync(byte[] data) =>
            _firstTransform.ApplyAsync(data)
                .ContinueWith(task => _secondTransform.ApplyAsync(task.Result))
                .Unwrap();

        public byte[] Reverse(byte[] data) =>
            _firstTransform.Reverse(_secondTransform.Reverse(data));

        public Task<byte[]> ReverseAsync(byte[] data) =>
            _secondTransform.ReverseAsync(data)
                .ContinueWith(task => _firstTransform.ReverseAsync(task.Result))
                .Unwrap();
    }
}