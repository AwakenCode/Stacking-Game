using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    [SerializeField] private float _rotationSmoothTime;
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _target;
    [SerializeField] private UpdateProvider _updateProvider;
    [SerializeField] private CoroutineExecutionProvider _coroutineExecutionProvider;
    [SerializeField] private uint _maxNumberOfJumps;

    private CharacterController _characterController;
    private PlayerInputRouter _inputRouter;

    private IMoveable _moveable;
    private IRotator _rotator;
    private IJump _jump;
    private IGravity _gravity;

    public Action<IGravity> GravityChanged;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _inputRouter = new PlayerInputRouter();
        _moveable = new SimpleMovement(_inputRouter, _speed, _characterController, _updateProvider);
        _gravity = new MoveablesSimpleGravity(_characterController, _moveable, _gravityMultiplier, _updateProvider);
        _rotator = new PlayerRotation(_inputRouter, _moveable, _target, _rotationSmoothTime);
        _jump = new SimpleJump(_jumpForce, _characterController, _gravity, this);
    }

    private void OnEnable()
    {
        _inputRouter.Enable();
        _inputRouter.Input.Player.Jump.started += context => _jump.Jump();
    }

    private void Update()
    {
        _inputRouter.Update();
        _moveable.Move();
        _rotator.Rotate();
        _gravity.ApplyGravity();
    }

    [ContextMenu("Multiple Jump")]
    private void SetMultipleJump()
    {
        Dispose(_jump as IDisposable);

        _jump = new MultipleJump(_jumpForce, _maxNumberOfJumps, _characterController, _coroutineExecutionProvider, _gravity, this);
    }

    [ContextMenu("Simple Jump")]
    private void SetSimpleJump()
    {
        Dispose(_jump as IDisposable);

        _jump = new SimpleJump(_jumpForce, _characterController, _gravity, this);
    }

    [ContextMenu("Simple Gravity")]
    private void SetSimpleGravity()
    {
        Dispose(_gravity as IDisposable);

        _gravity = new MoveablesSimpleGravity(_characterController, _moveable, _gravityMultiplier, _updateProvider);
        GravityChanged?.Invoke(_gravity);
    }

    [ContextMenu("No Gravity")]
    private void SetNoGravity()
    {
        Dispose(_gravity as IDisposable);

        _gravity = new ZeroGravity();
        GravityChanged?.Invoke(_gravity);
    }

    private void Dispose(IDisposable disposable) => disposable?.Dispose();
}