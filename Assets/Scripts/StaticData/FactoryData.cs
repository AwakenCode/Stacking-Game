using UnityEngine;
using Character;
using GameplayEntities.Box;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Factory/Data", order = 61)]
    public class FactoryData : ScriptableObject
    {
        [field: SerializeField] public Box BoxTemplate { get; private set; }
        [field: SerializeField] public Player PlayerTemplate { get; private set; }
    }
}