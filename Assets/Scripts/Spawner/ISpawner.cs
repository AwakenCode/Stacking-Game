using UnityEngine;

namespace Spawner
{
    public interface ISpawner
    {
        void Spawn(Vector3 position, Quaternion rotation, Transform parent);
    }

    public interface ISpawn
    {
        void Spawn();
    }
}