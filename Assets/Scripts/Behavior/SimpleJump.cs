using UnityEngine;

namespace Behavior
{
    public class SimpleJump : IJump
    {
        private readonly CharacterController _characterController;
        private readonly float _jumpForce;
        private readonly PlayerMovement _playerMovement;

        private IGravity _gravity;

        public SimpleJump(float jumpForce, CharacterController characterController, PlayerMovement playerMovement)
        {
            _jumpForce = jumpForce;
            _characterController = characterController;
            _playerMovement = playerMovement;
            _gravity = _playerMovement.Gravity;

            _playerMovement.GravityChanged += OnGravityChanged;
            _playerMovement.JumpChanged += Dispose;
        }

        public void Jump()
        {
            if (_characterController.isGrounded == false) return;

            _gravity.Value = _jumpForce;
        }

        private void OnGravityChanged(IGravity gravity) => _gravity = gravity;

        private void Dispose(IJump jump)
        {
            _playerMovement.GravityChanged -= OnGravityChanged;
            _playerMovement.JumpChanged -= Dispose;
        }
    }
}