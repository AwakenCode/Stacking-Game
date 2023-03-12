using GameplayEntities.Box;
using Infrastructure.Factory;
using Service;
using UnityEngine;

namespace Pool
{
    public class BoxPool : IObjectPool<Box>, IService
    {
        private readonly IGameFactory _factory;
        private readonly IObjectPool<Box> _pool;

        private Transform _container;

        public BoxPool(IGameFactory factory)
        {
            _factory = factory;
            _pool = new ObjectPool<Box>(Create, OnDestroyed, OnGot, OnRelaesed);
        }

        public void Init(Transform container, uint initialCount) 
        { 
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

        private void OnGot(Box box) { }
    }
}