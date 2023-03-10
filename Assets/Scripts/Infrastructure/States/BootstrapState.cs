using Service;
using Infrastructure.Factory;
using Service.Input;
using Service.Progress;
using Infrastructure.States.StateMachine;
using Service.StaticData;
using Service.Asset;
using Infrastructure.Boot;
using Service.UnityContext;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string BootScene = "Boot";

        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly UnityUpdater _unityUpdater;
        private readonly Services _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, UnityUpdater unityUpdater, 
            ICoroutineRunner coroutineRunner, Services services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _unityUpdater = unityUpdater;
            _coroutineRunner = coroutineRunner;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(BootScene, OnLoaded);
        }

        public void Exit() { }

        private void RegisterServices()
        {
            _services.Register<UnityUpdater>(_unityUpdater);
            _services.Register<UnityInstantiater>(new UnityInstantiater());
            _services.Register<IGameStateMachine>(_gameStateMachine);
            _services.Register<ICoroutineRunner>(_coroutineRunner);
            _services.Register<IPlayerProgressService>(new PlayerProgressService());
            _services.Register<IInputService>(new PlayerInputRouter(_services.Resolve<UnityUpdater>()));

            RegisterAssetProvider();
            RegisterStaticData();
            RegisterPlayerBehaviorFactory();
            RegisterGameFactory();
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new AssetProvider(_services.Resolve<UnityInstantiater>());
            _services.Register<IAssetProvider>(assetProvider);
            assetProvider.Load();
        }

        private void RegisterStaticData()
        {
            StaticDataService staticData = new StaticDataService();
            _services.Register<IStaticDataService>(staticData);
            staticData.Load();
        }

        private void RegisterGameFactory()
        {
            _services.Register<IGameFactory>(new GameFactory(
                _services.Resolve<IAssetProvider>(),
                _services.Resolve<IStaticDataService>(),
                _services.Resolve<UnityUpdater>(),
                _services.Resolve<IInputService>(),
                _services.Resolve<IBehaviorFactory>()
            ));
        }

        private void RegisterPlayerBehaviorFactory()
        {
            _services.Register<IBehaviorFactory>(new PlayerBehaviorFactory(
                _services.Resolve<UnityUpdater>(),
                _services.Resolve<ICoroutineRunner>(),
                _services.Resolve<IInputService>(),
                _services.Resolve<IStaticDataService>()
            ));
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}
