using UnityEngine;
using System.Collections;
using Infrastructure.Boot;
using Character;
using Behavior.Interface;

namespace Behavior
{
    public class MultipleJump : IJump
    {
        private readonly uint _maxNumberOfJumps;
        private readonly float _jumpForce;
        private readonly CharacterController _characterController;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly PlayerMovement _playerMovement;

        private IGravity _gravity;
        private uint _numberOfJumps = 0;

        public MultipleJump(float jumpForce, uint maxNumberOfJumps, CharacterController characterController,
            ICoroutineRunner coroutineRunner, IGravity gravity, PlayerMovement playerMovement)
        {
            _jumpForce = jumpForce;
            _maxNumberOfJumps = maxNumberOfJumps;
            _characterController = characterController;
            _coroutineRunner = coroutineRunner;
            _gravity = gravity;
            _playerMovement = playerMovement;

            _playerMovement.GravityChanged += OnGravityChanged;
            _playerMovement.JumpChanged += Dispose;
        }

        public void Jump()
        {
            if (IsGrounded() == false && (_numberOfJumps >= _maxNumberOfJumps || _numberOfJumps == 0)) return;

            if (_numberOfJumps == 0)
                _coroutineRunner.StartCoroutine(WaitForLanding());

            _numberOfJumps++;
            _gravity.Value = _jumpForce;
        }

        private IEnumerator WaitForLanding()
        {
            yield return new WaitUntil(() => IsGrounded() == false);
            yield return new WaitUntil(IsGrounded);

            _numberOfJumps = 0;
        }

        private void OnGravityChanged(IGravity gravity) => _gravity = gravity;

        private bool IsGrounded() => _characterController.isGrounded;

        private void Dispose(IJump jump)
        {
            _playerMovement.GravityChanged -= OnGravityChanged;
            _playerMovement.JumpChanged -= Dispose;
        }
    }
}