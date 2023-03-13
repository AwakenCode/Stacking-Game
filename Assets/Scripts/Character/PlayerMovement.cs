using Behavior.Interface;
using Service.Input;
using Service.UnityContext;
using System;

namespace Character
{
    public class PlayerMovement : IUpdateable
    {
        private readonly UnityUpdater _unityUpdater;
        private readonly IInputService _input;

        public Action<IGravity> GravityChanged;
        public Action<IJump> JumpChanged;
        public Action<IRotator> RotatorChanged;
        public Action<IMoveable> MoveableChanged;

        public IMoveable Moveable { get; private set; }
        public IGravity Gravity { get; private set; }
        public IRotator Rotator { get; private set; }
        public IJump Jump { get; private set; }

        public PlayerMovement(UnityUpdater unityUpdater, IInputService input)
        {
            _unityUpdater = unityUpdater;
            _input = input;

            _unityUpdater.AddUpdateables(this);
        }

        public void Update(float deltaTime)
        {
            Moveable.Move();
            Rotator.Rotate();
            Gravity.ApplyGravity();
        }

        public void SetGravity(IGravity gravity)
        {
            Gravity = gravity;
            GravityChanged?.Invoke(Gravity);
        }

        public void SetMovable(IMoveable moveable)
        {
            Moveable = moveable;
            MoveableChanged?.Invoke(Moveable);
        }

        public void SetRotator(IRotator rotator)
        {
            Rotator = rotator;
            RotatorChanged?.Invoke(Rotator);
        }

        public void SetJump(IJump jump)
        {
            Jump = jump;

            _input.SubscribeForJump(Jump.Jump);
            JumpChanged?.Invoke(Jump);
        }
    }
}