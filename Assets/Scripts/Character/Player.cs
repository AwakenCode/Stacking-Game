using Service;
using Infrastructure.Factory;
using UnityEngine;

namespace Character
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }

        private IBehaviorFactory _behaviorFactory;

        public PlayerMovement PlayerMovement { get; private set; }

        private void Awake()
        {
            _behaviorFactory = Services.Container.Resolve<IBehaviorFactory>();
        }

        public void SetMovement(PlayerMovement playerMovement)
        {
            PlayerMovement = playerMovement;
        }
    }
}