using UnityEngine;
using Common.Interface;

namespace Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(ICollectorTransform))] private Object _collectorTransform;
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        private PlayerMovement _playerMovement;
        private Inventory _inventory;

        public ICollectorTransform CollectorTransform => _collectorTransform as ICollectorTransform;
        public PlayerMovement PlayerMovement { get => _playerMovement; set => _playerMovement ??= value; }
        public Inventory Inventory { get => _inventory; set => _inventory ??= value; }
    }
}