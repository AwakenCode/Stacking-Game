using Behavior;
using Character;
using Service.Input;
using Service.StaticData;
using Service.UnityContext;
using StaticData;
using Infrastructure.Boot;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class PlayerBehaviorFactory : IBehaviorFactory
    {
        private readonly UnityUpdater _unityUpdater;
        private readonly PlayerData _playerData;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInputService _inputService;

        private Player _player;

        private PlayerMovement _playerMovement => _player.PlayerMovement;
        private CharacterController _characterController => _player.CharacterController;

        public PlayerBehaviorFactory(UnityUpdater unityUpdater, ICoroutineRunner coroutineRunner, IInputService inputServie,
            IStaticDataService staticDataService)
        {
            _unityUpdater = unityUpdater;
            _coroutineRunner = coroutineRunner;
            _inputService = inputServie;
            _playerData = staticDataService.GetPlayerData();
        }

        public void Init(Player player)
        {
            _player = player;
        }

        public IMoveable CreateSimpleMovement() =>
            new SimpleMovement(_inputService, _playerData.Speed, _characterController, _unityUpdater);

        public IRotator CreateSimpleRotator() =>
            new SimpleRotator(_inputService, _playerMovement.Moveable, _player.transform,
                _playerData.RotatioinSmoothTime);

        public IGravity CreateSimpleGravity() =>
            new MoveablesSimpleGravity(_characterController, _playerData.GravityMultiplier, _unityUpdater, _playerMovement);

        public IGravity CreateZeroGravity() => new ZeroGravity();

        public IJump CreateSimpleJump() =>
            new SimpleJump(_playerData.JumpForce, _characterController, _playerMovement);

        public IJump CreateMultipleJump() =>
            new MultipleJump(_playerData.JumpForce, _playerData.MaxNumberOfJumps, _characterController,
                _coroutineRunner, _playerMovement.Gravity, _playerMovement);
    }
}