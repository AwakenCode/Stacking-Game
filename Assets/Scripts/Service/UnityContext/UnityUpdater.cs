using System.Collections.Generic;
using UnityEngine;

namespace Service.UnityContext
{
    public class UnityUpdater : MonoBehaviour, IService
    {
        private List<IUpdateable> _updateables = new List<IUpdateable>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            foreach (IUpdateable updateable in _updateables)
                updateable.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {

        }

        private void LateUpdate()
        {

        }

        public void AddUpdateables(params IUpdateable[] updateables)
        {
            foreach (IUpdateable updateable in updateables)
                _updateables.Add(updateable);
        }

        public void RemoveUpdateable(IUpdateable updateable)
        {
            _updateables.Remove(updateable);
        }
    }
}