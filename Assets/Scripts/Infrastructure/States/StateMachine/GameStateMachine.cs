using Service;
using Service.Progress;
using Service.UnityContext;
using Infrastructure.Boot;
using System;
using System.Collections.Generic;
using UI;

namespace Infrastructure.States.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        private IState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, ILoadingCurtain loadingCurtain, UnityUpdater unityUpdater, 
            ICoroutineRunner coroutineRunner, Services services)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, unityUpdater, 
                    coroutineRunner, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader, 
                    services.Resolve<IPlayerProgressService>()
                ),
                [typeof(LoadLevelState)] = new LoadLevelState(this, loadingCurtain, sceneLoader, services), 
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : IState
        {
            _activeState?.Exit();

            _activeState = GetState<TState>();
            return (TState)_activeState;
        }

        private IState GetState<TState>() where TState : IState
        {
            return _states[typeof(TState)];
        }
    }
}
