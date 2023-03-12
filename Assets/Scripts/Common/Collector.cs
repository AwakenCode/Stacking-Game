using Common.Interface;
using GameplayEntities.Interface;
using System;
using UnityEngine;

namespace Common
{
    public class Collector : MonoBehaviour, ICollectorTransform
    {
        [SerializeField] private Transform _holder;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _collectingDuration;
        [SerializeField] private int _transformsCount;

        private ITransfer _transfer;

        public event Action<ICollectableTransform> CollectableCollected;

        private void Awake()
        {
            _transfer = TransferFactory.CreateJumpTransfer(_holder, _jumpPower, _collectingDuration);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICollectableTransform collectableTransform))
                Collect(collectableTransform);
        }

        private void Collect(ICollectableTransform collectable)
        {
            if (collectable.IsCollected) return;

            _transformsCount++;

            _transfer.Transfer(collectable);

            collectable.BeCollected();
            CollectableCollected?.Invoke(collectable);
        }
    }
}