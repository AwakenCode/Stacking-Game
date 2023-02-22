using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputRouter
{
    private const int MaxDeltaMagnitude = 1;
    private const int TouchScreenSize = 150;

    private readonly PlayerInput _input;

    private bool _isTouchScreen = false;
    private Vector2 _startPosition = Vector2.zero;


    public PlayerInputRouter()
    {
        _input = new PlayerInput();
    }

    public PlayerInput Input => _input;
    public Vector2 Value { get; private set; }

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
        Value = Vector2.zero;
    }

    public void Update()
    {
        if (_isTouchScreen == false)
            Value = _input.Player.Move.ReadValue<Vector2>();
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

        Value = delta;
    }

    private void OnTouchFingerUp(Finger finger)
    {
        if (_isTouchScreen == false)
            return;

        Value = Vector2.zero;
        _isTouchScreen = false;
    }
}