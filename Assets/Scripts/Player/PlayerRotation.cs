using UnityEngine;

public class PlayerRotation : IRotator
{
    private readonly PlayerInputRouter _input;
    private readonly IMoveable _moveable;
    private readonly Transform _target;
    private readonly float _rotationSmoothTime;

    private float _currentVelocity;

    public PlayerRotation(PlayerInputRouter input, IMoveable moveable, Transform target, float rotationSmoothTime)
    {
        _input = input;
        _moveable = moveable;
        _target = target;
        _rotationSmoothTime = rotationSmoothTime;
    }

    public void Rotate()
    {
        if (_input.Value.sqrMagnitude == 0) return;

        float targetAngle = Mathf.Atan2(_moveable.Direction.x, _moveable.Direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(_target.eulerAngles.y, targetAngle, ref _currentVelocity, _rotationSmoothTime);
        _target.rotation = Quaternion.Euler(0, angle, 0);
    }
}