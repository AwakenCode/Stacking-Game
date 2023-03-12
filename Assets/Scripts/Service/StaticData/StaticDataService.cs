using StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Service.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public const string PlayerDataPath = "StaticData/Player/PlayerData";
        public const string FactoryDataPath = "StaticData/Factory/FactoryData";
        public const string PoolDataPath = "StaticData/Pool/PoolData";
        public const string StackDataPath = "StaticData/Stack/StackData";

        private Dictionary<string, ScriptableObject> _data;

        public void Load()
        {
            _data = new Dictionary<string, ScriptableObject>() {
                { PlayerDataPath, Resources.Load<PlayerData>(PlayerDataPath) },
                { FactoryDataPath, Resources.Load<FactoryData>(FactoryDataPath) },
                { PoolDataPath, Resources.Load<PoolData>(PoolDataPath)},
                { StackDataPath, Resources.Load<StackData>(StackDataPath) }
            };
        }

        public PlayerData GetPlayerData() => (PlayerData)_data[PlayerDataPath];
        public FactoryData GetFactoryData() => (FactoryData)_data[FactoryDataPath];
        public PoolData GetPoolData() => (PoolData)_data[PoolDataPath];
        public StackData GetStackData() => (StackData)_data[StackDataPath];
    }
}