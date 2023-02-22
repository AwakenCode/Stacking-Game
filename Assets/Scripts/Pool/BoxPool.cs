using UnityEngine;

public class BoxPool : IObjectPool<Box>
{
    private readonly Transform _container;
    private readonly IObjectFactory<ITransformable> _factory;
    private readonly IObjectPool<Box> _pool;

    public BoxPool(IObjectFactory<ITransformable> factory, uint initialCount, Transform container)
    {
        _factory = factory;
        _pool = new ObjectPool<Box>(Create, OnDestroyed, OnGot, OnRelaesed);
        _container = container;

        for (int i = 0; i < initialCount; i++)
            _pool.Release(Create());
    }

    public Box Get() => _pool.Get();

    public void Release(Box box) => _pool.Release(box);

    private Box Create() => (Box) _factory.CreateBox();

    private void OnDestroyed(Box box)
    {
        Object.Destroy(box);
    }

    private void OnRelaesed(Box box)
    {
        box.transform.SetParent(_container);
        box.gameObject.SetActive(false);
    }

    private void OnGot(Box box)
    {

    }
}