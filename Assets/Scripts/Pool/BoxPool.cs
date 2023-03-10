using GameplayEntities.Box;
using Infrastructure.Factory;
using UnityEngine;

namespace Pool
{
    public class BoxPool : IObjectPool<Box>
    {
        private readonly Transform _container;
        private readonly IObjectFactory<Box> _factory;
        private readonly IObjectPool<Box> _pool;

        public BoxPool(IObjectFactory<Box> factory, uint initialCount, Transform container)
        {
            _factory = factory;
            _pool = new ObjectPool<Box>(Create, OnDestroyed, OnGot, OnRelaesed);
            _container = container;

            for (int i = 0; i < initialCount; i++)
                _pool.Release(Create());
        }

        public Box Get() => _pool.Get();

        public void Release(Box box) => _pool.Release(box);

        private Box Create() => _factory.CreateBox();

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
}