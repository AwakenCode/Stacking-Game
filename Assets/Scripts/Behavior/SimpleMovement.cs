using System;
using UnityEngine;

public class SimpleMovement : IMoveable, IUpdateable, IDisposable
{
    private readonly PlayerInputRouter _input;
    private readonly float _speed;
    private float _deltaTime;
    private Vector3 _motion;
    private CharacterController _characterController;
    private UpdateProvider _updateProvider;
    
    public SimpleMovement(PlayerInputRouter input, float speed, CharacterController characterController, UpdateProvider updateProvider)
    {
        _input = input;
        _speed = speed;
        _characterController = characterController;
        _updateProvider = updateProvider;

        _updateProvider.AddUpdateables(this);
    }

    public Vector3 Direction { get; set; }

    public void Move()
    {
        Direction = new Vector3(_input.Value.x, Direction.y, _input.Value.y);
        _motion = _speed * _deltaTime * Direction;
        _characterController.Move(_motion);
    }

    public void Update(float deltaTime)
    {
        _deltaTime = deltaTime;
    }

    public void Dispose()
    {
        _updateProvider.RemoveUpdateable(this);
    }
}