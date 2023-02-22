using UnityEngine;
using System.Collections;
using System;

public class MultipleJump : IJump, IDisposable
{
    private readonly uint _maxNumberOfJumps;
    private readonly float _jumpForce;
    private readonly CharacterController _characterController;
    private readonly CoroutineExecutionProvider _coroutineExecutionProvider;
    private readonly PlayerMovement _playerMovement;

    private IGravity _gravity;
    private uint _numberOfJumps = 0;

    public MultipleJump(float jumpForce, uint maxNumberOfJumps, CharacterController characterController, 
        CoroutineExecutionProvider coroutineExecutionProvider, IGravity gravity, PlayerMovement playerMovement)
    {
        _jumpForce = jumpForce;
        _maxNumberOfJumps = maxNumberOfJumps;
        _characterController = characterController;
        _coroutineExecutionProvider = coroutineExecutionProvider;
        _gravity = gravity;
        _playerMovement = playerMovement;

        _playerMovement.GravityChanged += OnGravityChanged;
    }

    public void Jump()
    {
        if (IsGrounded() == false && (_numberOfJumps >= _maxNumberOfJumps || _numberOfJumps == 0)) return;

        if (_numberOfJumps == 0)
            _coroutineExecutionProvider.StartCoroutine(WaitForLanding());

        _numberOfJumps++;
        _gravity.Value = _jumpForce;
    }

    public void Dispose() => _playerMovement.GravityChanged -= OnGravityChanged;

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => IsGrounded() == false);
        yield return new WaitUntil(IsGrounded);

        _numberOfJumps = 0;
    }

    private void OnGravityChanged(IGravity gravity) => _gravity = gravity;

    private bool IsGrounded() => _characterController.isGrounded;
}