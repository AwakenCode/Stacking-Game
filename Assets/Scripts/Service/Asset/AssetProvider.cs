using System.Collections.Generic;
using UnityEngine;

namespace Service.Asset
{
    public class AssetProvider : IAssetProvider
    {
        private Dictionary<string, GameObject> _source;

        public void Load()
        {
            _source = new Dictionary<string, GameObject>()
            {
                { AssetPath.Player, Resources.Load<GameObject>(AssetPath.Player) },
                { AssetPath.Box,  Resources.Load<GameObject>(AssetPath.Box) },
                { AssetPath.PoolContainers, Resources.Load<GameObject>(AssetPath.PoolContainers) },
                { AssetPath.CollectablesReceiver, Resources.Load<GameObject>(AssetPath.CollectablesReceiver) }
            };
        }

        public GameObject Instantiate(string path) => Object.Instantiate(_source[path]);

        public GameObject Instantiate(string path, Vector3 position) =>
            Object.Instantiate(_source[path], position, Quaternion.identity);

        public GameObject Instantiate(string path, Vector3 position, Transform parent) => 
            Object.Instantiate(_source[path], position, Quaternion.identity, parent);
    }
}