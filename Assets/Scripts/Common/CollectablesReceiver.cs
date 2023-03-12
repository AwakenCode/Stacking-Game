using Common.Interface;
using GameplayEntities.Box;
using GameplayEntities.Interface;
using Pool;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class CollectablesReceiver : MonoBehaviour, ICollectablesReceiver
    {
        [SerializeField] private Transform _stackPoint;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _receiveDuration;
        [SerializeField] private float _delay;

        private IObjectPool<Box> _boxPool;
        private ITransfer _transfer;

        public event Action CollectorTriggered;

        public bool IsCollectorTriggered { get; private set; } = false;

        private void Awake()
        {
            _transfer = TransferFactory.CreateJumpTransfer(_stackPoint, _jumpPower, _receiveDuration);
            _boxPool = Services.Container.Resolve<BoxPool>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICollectorTransform collector))
            {
                IsCollectorTriggered = true;
                CollectorTriggered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ICollectorTransform collector))
                IsCollectorTriggered = false;
        }

        public void ReceiveCollectables(Stack<ICollectableTransform> collectableTransforms)
        {
            StartCoroutine(Receive(collectableTransforms));
        }

        private IEnumerator Receive(Stack<ICollectableTransform> collectableTransforms)
        {
            WaitForSeconds seconds = new WaitForSeconds(_delay);

            while (IsCollectorTriggered)
            {
                if (collectableTransforms.Count <= 0)
                    yield break;

                ICollectableTransform collectableTransform = collectableTransforms.Pop();

                _transfer.Transfer(collectableTransform, () =>
                {
                    if (collectableTransform is Box box)
                        _boxPool.Release(box);
                });

                yield return seconds;
            }
        }
    }
}