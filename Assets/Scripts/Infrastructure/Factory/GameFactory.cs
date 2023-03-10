using Character;
using Service.Asset;
using Service.Input;
using Service.StaticData;
using Service.UnityContext;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly UnityUpdater _unityUpdater;
        private readonly IInputService _inputService;
        private readonly IBehaviorFactory _behaviorFactory;

        private Player _player;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService, UnityUpdater unityUpdater,
            IInputService input, IBehaviorFactory behaviorFactory)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _unityUpdater = unityUpdater; 
            _inputService = input;
            _behaviorFactory = behaviorFactory;
        }

        public Player CreatePlayer()
        {
            _player = _assetProvider.Instantiate(AssetPath.Player, _staticDataService.GetPlayerData().Position)
                .GetComponent<Player>();

            _behaviorFactory.Init(_player);

            _player.SetMovement(new PlayerMovement(_unityUpdater, _inputService));
            _player.PlayerMovement.SetMovable(_behaviorFactory.CreateSimpleMovement());
            _player.PlayerMovement.SetRotator(_behaviorFactory.CreateSimpleRotator());
            _player.PlayerMovement.SetGravity(_behaviorFactory.CreateSimpleGravity());
            _player.PlayerMovement.SetJump(_behaviorFactory.CreateSimpleJump());

            return _player;
        }
    }
}