using Character;
using Common;
using Service.Progress;
using Infrastructure.Boot;
using Infrastructure.Factory;
using Infrastructure.States.StateMachine;
using UI;
using UnityEngine;
using Service;
using Pool;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateMachine _stateMachine;
        private readonly BoxPool _boxPool;

        public LoadLevelState(IGameStateMachine stateMachine, ILoadingCurtain loadingCurtain, SceneLoader sceneLoader,
            Services services)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _playerProgressService = services.Resolve<IPlayerProgressService>();
            _gameFactory = services.Resolve<IGameFactory>();
            _boxPool = services.Resolve<BoxPool>();
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(_playerProgressService.PlayerProgress.Level, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            InitLevel();
            _stateMachine.Enter<GameLoopState>();
        }

        public void InitLevel()
        {
            InitPlayer();
            InitPool();
        }

        private void InitPlayer()
        {
            Player player = _gameFactory.CreatePlayer();
            CollectablesReceiver collectablesReceiver = _gameFactory.CreateCollectablesReceiver();

            player.Inventory.Init(collectablesReceiver);
            SetCameraFollower(player.transform);
        }

        private void InitPool()
        {
            PoolContainers poolContainers = _gameFactory.CreatePoolContainers();
            _boxPool.Init(poolContainers.BoxContainer, 5);
        }

        private void SetCameraFollower(Transform target)
        {
            Camera.main.GetComponent<TargetFollower>().Init(target);
        }
    }
}
