using System;

public interface ICollector
{
    void Collect(ICollectable collectable) => collectable.BeCollected();
}

public interface ICollectorTransform : ICollector
{
    event Action<ICollectableTransform> CollectableCollected;
}