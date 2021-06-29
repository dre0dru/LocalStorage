using System;
using UnityEngine.Scripting;

namespace LocalStorage.DataTransform
{
    public class CombinedDataTransformSync : IDataTransformSync
    {
        private readonly IDataTransformSync _firstTransform;
        private readonly IDataTransformSync _secondTransform;

        [RequiredMember]
        public CombinedDataTransformSync(IDataTransformSync firstTransform, IDataTransformSync secondTransform)
        {
            _firstTransform = firstTransform ??
                              throw new ArgumentNullException(nameof(firstTransform));
            _secondTransform = secondTransform ??
                               throw new ArgumentNullException(nameof(secondTransform));
        }

        public byte[] Apply(byte[] data) =>
            _secondTransform.Apply(_firstTransform.Apply(data));

        public byte[] Reverse(byte[] data) =>
            _firstTransform.Reverse(_secondTransform.Reverse(data));
    }
}