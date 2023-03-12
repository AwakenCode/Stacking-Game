using Character;
using Common;
using Common.Interface;
using GameplayEntities.Box;
using Pool;
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
            Player player = _assetProvider.Instantiate(AssetPath.Player, _staticDataService.GetPlayerData().Position)
                .GetComponent<Player>();

            Inventory inventory = CreateInventory(player.CollectorTransform);
            player.Inventory = inventory;

            _behaviorFactory.Init(player);

            player.PlayerMovement = new PlayerMovement(_unityUpdater, _inputService);
            player.PlayerMovement.SetMovable(_behaviorFactory.CreateSimpleMovement());
            player.PlayerMovement.SetRotator(_behaviorFactory.CreateSimpleRotator());
            player.PlayerMovement.SetGravity(_behaviorFactory.CreateSimpleGravity());
            player.PlayerMovement.SetJump(_behaviorFactory.CreateSimpleJump());

            return player;
        }

        public Box CreateBox() => _assetProvider.Instantiate(AssetPath.Box).GetComponent<Box>();

        public PoolContainers CreatePoolContainers() 
            => _assetProvider.Instantiate(AssetPath.PoolContainers).GetComponent<PoolContainers>();

        public CollectablesReceiver CreateCollectablesReceiver() => 
            _assetProvider.Instantiate(
                AssetPath.CollectablesReceiver, 
                _staticDataService.GetStackData().Position
            ).GetComponent<CollectablesReceiver>();

        public Inventory CreateInventory(ICollectorTransform collector) 
            => new Inventory(collector);
    }
}