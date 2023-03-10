using GameplayEntities.Interface;
using System;

public interface ITransfer
{
    void Transfer(ITransformable collectableTransform, Action onComplete = null);
}