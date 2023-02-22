using System.Collections.Generic;
using UnityEngine;

public class UpdateProvider : MonoBehaviour
{
    private List<IUpdateable> _updateables = new List<IUpdateable>();

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