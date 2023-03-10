using UnityEngine;
using DG.Tweening;
using System;
using GameplayEntities.Interface;

namespace Behavior
{
    public class JumpingToTarget : ITransfer
    {
        private readonly Transform _holder;
        private readonly float _jumpPower;
        private readonly int _jumpCount;
        private readonly float _transferDuration;

        public JumpingToTarget(Transform parent = null, float jumpPower = 2, float transferDuration = 0.5f, uint jumpCount = 1)
        {
            _holder = parent;
            _jumpPower = jumpPower;
            _jumpCount = (int)jumpCount;
            _transferDuration = transferDuration;
        }

        public void Transfer(ITransformable transformable, Action onComplete = null)
        {
            Vector3 targetPosition = new Vector3(0, _holder.transform.childCount / 2.5f, 0);

            if (transformable is IPhysicObject physicObject)
            {
                physicObject.Rigidbody.useGravity = false;
                physicObject.Disable();
            }

            transformable.Transform.SetParent(_holder.transform, true);
            transformable.Transform.DOJump(_holder.position, _jumpPower, _jumpCount, _transferDuration).OnComplete(() =>
            {
                transformable.Transform.localPosition = targetPosition;
                transformable.Transform.rotation = default;

                if (transformable is IPhysicObject physicObject)
                    physicObject.Collider.enabled = true;

                onComplete?.Invoke();
            });
        }
    }
}