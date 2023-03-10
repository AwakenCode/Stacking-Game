using Service;
using Service.UnityContext;
using Infrastructure.States.StateMachine;
using UI;

namespace Infrastructure.Boot
{
    public class Game
    {
        private readonly GameStateMachine _stateMachine;

        public GameStateMachine StateMachine => _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, ILoadingCurtain loadingCurtain, UnityUpdater unityUpdater)
        {
            _stateMachine = new GameStateMachine(
                new SceneLoader(coroutineRunner), 
                loadingCurtain, unityUpdater, 
                coroutineRunner,
                Services.Container
            );
        }
    }
}