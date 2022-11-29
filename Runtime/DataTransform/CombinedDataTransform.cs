using System;
#if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
using Cysharp.Threading.Tasks;
#else
using System.Threading.Tasks;
#endif

namespace Dre0Dru.LocalStorage.DataTransform
{
    public class CombinedDataTransform : IDataTransform
    {
        private readonly IDataTransform _firstTransform;
        private readonly IDataTransform _secondTransform;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public CombinedDataTransform(IDataTransform firstTransform, IDataTransform secondTransform)
        {
            _firstTransform = firstTransform ??
                              throw new ArgumentNullException(nameof(firstTransform));
            _secondTransform = secondTransform ??
                               throw new ArgumentNullException(nameof(secondTransform));
        }

        public byte[] Apply(byte[] data) =>
            _secondTransform.Apply(_firstTransform.Apply(data));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ApplyAsync(byte[] data) =>
            _firstTransform.ApplyAsync(data)
                .ContinueWith(bytes => _secondTransform.ApplyAsync(bytes));
        #else
        public Task<byte[]> ApplyAsync(byte[] data) =>
            _firstTransform.ApplyAsync(data)
                .ContinueWith(task => _secondTransform.ApplyAsync(task.Result))
                .Unwrap();
        #endif

        public byte[] Reverse(byte[] data) =>
            _firstTransform.Reverse(_secondTransform.Reverse(data));

        #if !DISABLE_UNITASK_SUPPORT && UNITASK_SUPPORT
        public UniTask<byte[]> ReverseAsync(byte[] data) =>
            _secondTransform.ReverseAsync(data)
                .ContinueWith(bytes => _firstTransform.ReverseAsync(bytes));
        #else
        public Task<byte[]> ReverseAsync(byte[] data) =>
            _secondTransform.ReverseAsync(data)
                .ContinueWith(task => _firstTransform.ReverseAsync(task.Result))
                .Unwrap();
        #endif
    }
}