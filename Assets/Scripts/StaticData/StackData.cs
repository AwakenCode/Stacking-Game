using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "Stack/Data", order = 61)]
    public class StackData : ScriptableObject
    {
        [field: SerializeField] public Vector3 Position; 
    }
}