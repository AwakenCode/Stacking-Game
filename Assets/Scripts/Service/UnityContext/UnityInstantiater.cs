using UnityEngine;

namespace Service.UnityContext
{
    public class UnityInstantiater : IService
    {
        public T Instantiate<T>(T template) where T : Object
        {
            return Object.Instantiate(template);
        }

        public T Instantiate<T>(T template, Vector3 position) where T : Object
        {
            return Object.Instantiate(template, position, Quaternion.identity);
        }

        public T Instantiate<T>(T template, Vector3 position, Transform parent) where T : Object
        {
            return Object.Instantiate(template, position, Quaternion.identity, parent);
        }
    }
}