using Common.Interface;
using UnityEngine;

namespace Common
{
    public class RootCompose : MonoBehaviour, IComposer
    {
        [SerializeField, RequireInterface(typeof(IComposer))] Object[] _composersOrder;

        private void Awake()
        {
            Compose();
        }

        public void Compose()
        {
            foreach (IComposer composer in _composersOrder)
                composer.Compose();
        }
    }
}