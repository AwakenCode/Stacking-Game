using StaticData;
using UnityEngine;

namespace Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public const string PlayerDataPath = "StaticData/Player/PlayerData";
        public const string FactoryDataPath = "StaticData/Factory/FactoryData";

        private PlayerData _playerData;
        private FactoryData _factoryData;

        public void Load()
        {
            _playerData = Resources.Load<PlayerData>(PlayerDataPath);
            _factoryData = Resources.Load<FactoryData>(FactoryDataPath);
        }

        public PlayerData GetPlayerData() => _playerData;

        public FactoryData GetFactoryData() => _factoryData;
    }
}