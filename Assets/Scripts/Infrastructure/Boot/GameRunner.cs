using UnityEngine;

namespace Infrastructure.Boot
{
    public class GameRunner : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameBootsrapper _gameBootsrapperTemplate;

        private void Awake()
        {
            if (FindObjectOfType<GameBootsrapper>() != null) return;

            Instantiate(_gameBootsrapperTemplate);
        }
    }
}

