using System;
using UnityEngine;

public class MoveablesSimpleGravity : IGravity, IUpdateable, IDisposable
{
    private readonly CharacterController _characterController;
    private readonly IMoveable _moveable;
    private readonly float _gravityMultiplier;
    private readonly UpdateProvider _updateProvider;

    private float _deltaTime;

    public MoveablesSimpleGravity(CharacterController characterController, IMoveable moveable, float gravityMultiplier, UpdateProvider updateProvider)
    {
        _characterController = characterController;
        _gravityMultiplier = gravityMultiplier;
        _moveable = moveable;
        _updateProvider = updateProvider;

        _updateProvider.AddUpdateables(this);
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

    public void Dispose() => _updateProvider.RemoveUpdateable(this);
}