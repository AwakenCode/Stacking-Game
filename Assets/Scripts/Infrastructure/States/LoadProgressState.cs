using Service.Progress;
using Infrastructure.Boot;
using Infrastructure.States.StateMachine;
using UI;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPlayerProgressService _playerProgressService;

        public LoadProgressState(GameStateMachine stateMachine, SceneLoader sceneLoader, IPlayerProgressService playerProgressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _playerProgressService = playerProgressService;
        }

        public void Enter()
        {
            _stateMachine.Enter<LoadLevelState>();
        }

        public void Exit() { }
    }
}
