using Service.UnityContext;
using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure.Boot
{
    public class GameBootsrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private UnityUpdater _unityUpdater;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Instantiate(_loadingCurtain), Instantiate(_unityUpdater));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}