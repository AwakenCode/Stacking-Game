using Pool;
using UnityEngine;

namespace Spawner
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

        private ISpawner _boxSpawner;

        private void FixedUpdate()
        {
            //_elapsedTime += Time.deltaTime;

            //if (_spawnedBoxCount < _boxCount)
            //    if (_elapsedTime >= _spawnDelay)
            //SpawnBox();
        }

        public void Init(BoxPool boxPool)
        {
            _boxSpawner = new Spawner(() => boxPool.Get());
        }

        private void SpawnBox()
        {
            _elapsedTime = 0;
            _boxSpawner.Spawn(_spawnPoints[_nextPointIndex].transform.position, Quaternion.identity, _spawnedBoxContainer);
            _spawnedBoxCount++;

            if (_nextPointIndex == _spawnPoints.Length - 1)
                _nextPointIndex = 0;
            else
                _nextPointIndex++;
        }
    }
}