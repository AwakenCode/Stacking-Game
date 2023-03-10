using Service.Input;
using Service.UnityContext;
using System;
using UnityEngine;

namespace Behavior
{
    public class SimpleMovement : IMoveable, IUpdateable, IDisposable
    {
        private readonly IInputService _input;
        private readonly float _speed;
        private float _deltaTime;
        private Vector3 _motion;
        private CharacterController _characterController;
        private UnityUpdater _unityUpdater;

        public SimpleMovement(IInputService input, float speed, CharacterController characterController, UnityUpdater unityUpdater)
        {
            _input = input;
            _speed = speed;
            _characterController = characterController;
            _unityUpdater = unityUpdater;

            _unityUpdater.AddUpdateables(this);
        }

        public Vector3 Direction { get; set; }

        public void Move()
        {
            Direction = new Vector3(_input.Movement.x, Direction.y, _input.Movement.y);
            _motion = _speed * _deltaTime * Direction;
            _characterController.Move(_motion);
        }

        public void Update(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        public void Dispose()
        {
            _unityUpdater.RemoveUpdateable(this);
        }
    }
}