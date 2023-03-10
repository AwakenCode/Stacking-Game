using Character;
using Common;
using Service.Progress;
using Infrastructure.Boot;
using Infrastructure.Factory;
using Infrastructure.States.StateMachine;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(IGameStateMachine stateMachine, ILoadingCurtain loadingCurtain, SceneLoader sceneLoader,
            IPlayerProgressService playerProgressService, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
            _gameFactory = gameFactory;
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
            InitPlayer();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitPlayer()
        {
            Player player = _gameFactory.CreatePlayer();
            SetCameraFollower(player.transform);
        }

        private void SetCameraFollower(Transform target)
        {
            Camera.main.GetComponent<TargetFollower>().Init(target);
        }
    }
}
