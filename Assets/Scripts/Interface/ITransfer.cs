using System;

public interface ITransfer
{
    public void Transfer(ITransformable transform, Action onComplete = null);
}