using System;
using UnityEngine;

public class SimpleJump : IJump, IDisposable
{
    private readonly CharacterController _characterController;
    private readonly float _jumpForce;
    private readonly PlayerMovement _playerMovement;

    private IGravity _gravity;

    public SimpleJump(float jumpForce, CharacterController characterController, IGravity gravity, PlayerMovement playerMovement)
    {
        _jumpForce = jumpForce;
        _characterController = characterController;
        _gravity = gravity;
        _playerMovement = playerMovement;
        _playerMovement.GravityChanged += OnGravityChanged;
    }

    public void Jump()
    {
        if (_characterController.isGrounded == false) return;

        _gravity.Value = _jumpForce;
    }

    public void Dispose() => _playerMovement.GravityChanged -= OnGravityChanged;

    private void OnGravityChanged(IGravity gravity) => _gravity = gravity;
}