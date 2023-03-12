using Character;
using Common;
using Common.Interface;
using GameplayEntities.Box;
using Pool;
using Service;

namespace Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Player CreatePlayer();
        Box CreateBox();
        PoolContainers CreatePoolContainers();

        CollectablesReceiver CreateCollectablesReceiver();
        Inventory CreateInventory(ICollectorTransform collector);
    }
}