using GameplayEntities.Box;
using Pool;
using Service;
using UnityEngine;

namespace Common
{
    public class SpawnersRoot : MonoBehaviour
    {
        [SerializeField] private int _boxCount;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private Transform _spawnedBoxContainer;
        [SerializeField] private Transform[] _spawnPoints;

        private float _elapsedTime = 0;
        private int _spawnedBoxCount;
        private int _nextPointIndex = 0;

        BoxPool _boxPool;

        private void Awake()
        {
            _boxPool = Services.Container.Resolve<BoxPool>();
        }

        private void FixedUpdate()
        {
            _elapsedTime += Time.deltaTime;

            if (_spawnedBoxCount < _boxCount)
                if (_elapsedTime >= _spawnDelay)
                    SpawnBox();
        }

        private void SpawnBox()
        {
            _elapsedTime = 0;
            Box box = _boxPool.Get();

            box.transform.SetParent(_spawnedBoxContainer);
            box.transform.SetPositionAndRotation(_spawnPoints[_nextPointIndex].transform.position, Quaternion.identity);
            _spawnedBoxCount++;

            if (_nextPointIndex == _spawnPoints.Length - 1)
                _nextPointIndex = 0;
            else
                _nextPointIndex++;
        }
    }
}