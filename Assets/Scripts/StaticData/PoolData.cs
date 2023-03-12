using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Pool/Data", order = 61)]
    public class PoolData : ScriptableObject
    {
        [field: SerializeField] public int InitialObjectCount { get; private set; }
    }
}