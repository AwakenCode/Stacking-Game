using UnityEngine;

public class Level : MonoBehaviour, IComposer
{
    [SerializeField, RequireInterface(typeof(IObjectFactory<ITransformable>))] private Object _factory;
    [SerializeField, RequireInterface(typeof(ICollectorTransform))] private Object _collector;
    [SerializeField, RequireInterface(typeof(ICollectablesReceiver))] private Object _collectorReceiver;
    [SerializeField] private Transform _container;
    [SerializeField] private SpawnersRoot _spawnersRoot;

    private Inventory _inventory;
    private BoxPool _boxPool;

    public IObjectFactory<ITransformable> Factory => _factory as IObjectFactory<ITransformable>;
    public ICollectorTransform CollectorTransform => _collector as ICollectorTransform;
    public ICollectablesReceiver CollectablesReceiver => _collectorReceiver as ICollectablesReceiver;
    public IObjectPool<Box> BoxPool => _boxPool;

    private void OnEnable()
    {
        _inventory.Enable();
    }

    private void OnDisable()
    {
        _inventory.Disable();
    }

    public void Compose()
    {
        _boxPool = new BoxPool(Factory, 5, _container);
        _inventory = new Inventory(CollectorTransform, CollectablesReceiver);

        _spawnersRoot.Init(_boxPool);
    }
}