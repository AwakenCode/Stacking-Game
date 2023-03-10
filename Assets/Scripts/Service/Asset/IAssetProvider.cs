using UnityEngine;

namespace Service.Asset
{
    public interface IAssetProvider : IService
    {
        void Load();
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
        GameObject Instantiate(string path, Vector3 position, Transform parent);
    }
}