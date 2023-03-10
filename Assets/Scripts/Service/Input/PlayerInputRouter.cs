using Service.UnityContext;
using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Service.Input
{
    public class PlayerInputRouter : IInputService, IUpdateable
    {
        private const int MaxDeltaMagnitude = 1;
        private const int TouchScreenSize = 150;

        private readonly PlayerInput _input;

        private bool _isTouchScreen = false;
        private Vector2 _startPosition = Vector2.zero;

        public PlayerInputRouter(UnityUpdater unityUpdater)
        {
            _input = new PlayerInput();
            unityUpdater.AddUpdateables(this);
            Enable();
        }

        public PlayerInput Input => _input;
        public Vector2 Movement { get; private set; }

        public void Enable()
        {
            _input.Enable();
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerDown += OnTouchFingerDown;
            ETouch.Touch.onFingerMove += OnTouchFingerMove;
            ETouch.Touch.onFingerUp += OnTouchFingerUp;
        }

        public void Disable()
        {
            if (EnhancedTouchSupport.enabled)
            {
                ETouch.Touch.onFingerDown -= OnTouchFingerDown;
                ETouch.Touch.onFingerMove -= OnTouchFingerMove;
                ETouch.Touch.onFingerUp -= OnTouchFingerUp;
                EnhancedTouchSupport.Disable();
            }

            _input.Disable();
            Movement = Vector2.zero;
        }

        public void Update(float deltaTime)
        {
            if (_isTouchScreen == false)
                Movement = _input.Player.Move.ReadValue<Vector2>();
        }

        public void SubscribeForJump(Action jumpAction)
        {
            Input.Player.Jump.started += context => jumpAction?.Invoke();
        }

        private void OnTouchFingerDown(Finger finger)
        {
            if (_isTouchScreen)
                return;

            _isTouchScreen = true;
            int x = (int)Math.Clamp(finger.screenPosition.x, TouchScreenSize, Screen.width - TouchScreenSize);
            int y = (int)Math.Clamp(finger.screenPosition.y, TouchScreenSize, Screen.height - TouchScreenSize);
            _startPosition = new Vector2(x, y);
        }

        private void OnTouchFingerMove(Finger finger)
        {
            Vector2 delta = finger.screenPosition - _startPosition;
            delta /= TouchScreenSize;

            if (delta.magnitude >= MaxDeltaMagnitude)
                delta.Normalize();

            Movement = delta;
        }

        private void OnTouchFingerUp(Finger finger)
        {
            if (_isTouchScreen == false)
                return;

            Movement = Vector2.zero;
            _isTouchScreen = false;
        }
    }
}