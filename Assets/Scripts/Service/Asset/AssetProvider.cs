using Service.UnityContext;
using System.Collections.Generic;
using UnityEngine;

namespace Service.Asset
{
    public class AssetProvider : IAssetProvider
    {
        private UnityInstantiater _unityInstantiater;
        private Dictionary<string, GameObject> _source = new Dictionary<string, GameObject>();

        public AssetProvider(UnityInstantiater unityInstantiater)
        {
            _unityInstantiater = unityInstantiater;
        }

        public void Load()
        {
            GameObject player = Resources.Load<GameObject>(AssetPath.Player);
            _source.Add(AssetPath.Player, player);
        }

        public GameObject Instantiate(string path)
        {
            return _unityInstantiater.Instantiate(_source[path]);
        }

        public GameObject Instantiate(string path, Vector3 position)
        {
            return _unityInstantiater.Instantiate(_source[path], position);
        }

        public GameObject Instantiate(string path, Vector3 position, Transform parent)
        {
            return _unityInstantiater.Instantiate(_source[path], position, parent);
        }
    }
}