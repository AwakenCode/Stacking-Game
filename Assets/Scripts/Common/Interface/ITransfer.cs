using GameplayEntities.Interface;
using System;

namespace Common.Interface
{
    public interface ITransfer
    {
        void Transfer(ITransformable collectableTransform, Action onComplete = null);
    }
}