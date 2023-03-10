using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Player/Data", order = 61)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public uint MaxNumberOfJumps { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float RotatioinSmoothTime { get; private set; }
        [field: SerializeField] public float GravityMultiplier { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
    }
}