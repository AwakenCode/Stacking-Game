using Character;
using GameplayEntities.Box;
using Pool;
using Spawner;
using Infrastructure.Factory;
using UnityEngine;

namespace Common
{
    public class Level : MonoBehaviour, IComposer
    {
        [SerializeField, RequireInterface(typeof(ICollectorTransform))] private Object _collector;
        [SerializeField, RequireInterface(typeof(ICollectablesReceiver))] private Object _collectorReceiver;
        [SerializeField] private Transform _container;
        [SerializeField] private SpawnersRoot _spawnersRoot;

        private Inventory _inventory;
        private BoxPool _boxPool;

        private IObjectFactory<Box> _boxFactory;
        public ICollectorTransform CollectorTransform => _collector as ICollectorTransform;
        public ICollectablesReceiver CollectablesReceiver => _collectorReceiver as ICollectablesReceiver;
        public IObjectPool<Box> BoxPool => _boxPool;

        private void OnEnable()
        {
            //_inventory.Enable();
        }

        private void OnDisable()
        {
            //_inventory.Disable();
        }

        public void Compose()
        {
            //_boxFactory = new BoxFactory(_factoryConfig, _unityInstantiater);
            //_boxPool = new BoxPool(_boxFactory, 5, _container);
            //_inventory = new Inventory(CollectorTransform, CollectablesReceiver);

            _spawnersRoot.Init(_boxPool);
        }
    }
}