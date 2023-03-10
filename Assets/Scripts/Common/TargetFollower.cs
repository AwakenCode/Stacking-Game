using UnityEngine;

namespace Common
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private float _smoothTime;

        private Transform _target;
        private Vector3 _targetPosition;
        private Vector3 _offset;
        private Vector3 _currentVelocity;

        private void LateUpdate()
        {
            if (_target == null) return;

            _targetPosition = _target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _currentVelocity, _smoothTime);
        }

        public void Init(Transform target)
        {
            _target = target;
            _offset = transform.position - _target.position;
        }
    }
}