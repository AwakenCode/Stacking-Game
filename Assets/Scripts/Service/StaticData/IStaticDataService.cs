using StaticData;

namespace Service.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        PlayerData GetPlayerData();
        FactoryData GetFactoryData();
        PoolData GetPoolData();
        StackData GetStackData();
    }
}