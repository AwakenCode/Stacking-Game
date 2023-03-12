using GameplayEntities.Interface;
using UnityEngine;

namespace GameplayEntities.Box
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Box : MonoBehaviour, ICollectableTransform, IPhysicObject
    {
        private Collider _collider;
        private Rigidbody _rigidbody;

        public bool IsCollected { get; private set; } = false;
        public Collider Collider => _collider;
        public Transform Transform => transform;
        public Rigidbody Rigidbody => _rigidbody;

        public void Awake()
        {
            _collider = GetComponent<Collider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void BeCollected()
        {
            IsCollected = true;
        }
    }
}