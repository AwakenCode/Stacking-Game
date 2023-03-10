using GameplayEntities.Interface;
using System;
using UnityEngine;

namespace Spawner
{
    public class Spawner : ISpawner
    {
        private Func<IObjectOfSpawn> _getObjectOfSpawn;

        public Spawner(Func<IObjectOfSpawn> get)
        {
            _getObjectOfSpawn = get;
        }

        public void Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            IObjectOfSpawn objectOfSpawn = _getObjectOfSpawn();

            if (objectOfSpawn is ITransformable transformable)
            {
                transformable.Transform.SetPositionAndRotation(position, rotation);
                transformable.Transform.SetParent(parent);
            }

            objectOfSpawn.BeSpawned();
        }
    }
}