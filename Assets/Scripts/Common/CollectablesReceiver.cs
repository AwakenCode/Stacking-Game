using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesReceiver : MonoBehaviour, ICollectablesReceiver, IComposer
{
    [SerializeField] private Collider _collider;
    [SerializeField] private Transform _stackPoint;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _receiveDuration;
    [SerializeField] private float _delay;
    [SerializeField] private Level _level;

    private IObjectPool<Box> _boxPool;
    private ITransfer _transfer;

    public bool IsCollectorTriggered { get; private set; } = false;

    public event Action CollectorTriggered;

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

    public void Compose()
    {
        _transfer = new JumpingToTarget(_stackPoint, _jumpPower, _receiveDuration);
        _boxPool = _level.BoxPool;
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
            if(collectableTransforms.Count <= 0)
                yield break;

            ICollectableTransform collectableTransform = collectableTransforms.Pop();

            _transfer.Transfer(collectableTransform, () =>
            {
                if(collectableTransform is Box box)
                    _boxPool.Release(box);
            });

            yield return seconds;
        }
    }
}