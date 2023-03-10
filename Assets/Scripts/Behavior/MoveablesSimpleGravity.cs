using Service.UnityContext;
using UnityEngine;

namespace Behavior
{
    public class MoveablesSimpleGravity : IGravity, IUpdateable
    {
        private readonly CharacterController _characterController;
        private readonly IMoveable _moveable;
        private readonly float _gravityMultiplier;
        private readonly UnityUpdater _unityUpdater;
        private readonly PlayerMovement _playerMovement;

        private float _deltaTime;

        public MoveablesSimpleGravity(CharacterController characterController, float gravityMultiplier,
            UnityUpdater unityUpdater, PlayerMovement playerMovement)
        {
            _characterController = characterController;
            _gravityMultiplier = gravityMultiplier;
            _unityUpdater = unityUpdater;
            _playerMovement = playerMovement;
            _moveable = _playerMovement.Moveable;

            _unityUpdater.AddUpdateables(this);
            playerMovement.GravityChanged += Dispose;
        }

        public float Value { get; set; } = 0;

        public void ApplyGravity()
        {
            if (_characterController.isGrounded && Value < 0)
                Value = -1f;
            else
                Value += Physics.gravity.y * _gravityMultiplier * _deltaTime;

            _moveable.Direction = new Vector3(_moveable.Direction.x, Value, _moveable.Direction.z);
        }

        public void Update(float deltaTime) => _deltaTime = deltaTime;

        private void Dispose(IGravity gravity)
        {
            if (gravity == this) return;

            _unityUpdater.RemoveUpdateable(this);
            _playerMovement.GravityChanged -= Dispose;
        }
    }
}