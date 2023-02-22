using UnityEngine;

public class TransformablesFactory : MonoBehaviour, IObjectFactory<ITransformable>
{
    [SerializeField] private Box _boxTemplate;

    public ITransformable CreateBox()
    {
        return Instantiate(_boxTemplate);
    }
}