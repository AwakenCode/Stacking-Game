using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Box : MonoBehaviour, ICollectableTransform, IObjectOfSpawn, IPhysicObject
{
    private Collider _collider;
    private Rigidbody _rigidbody;
    private IPhysicObject _physicObject => this;

    public bool IsCollected { get; private set; } = false;
    public Collider Collider => _collider;
    public Transform Transform => transform;
    public Rigidbody Rigidbody => _rigidbody;
    

    public void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void BeSpawned()
    {
        _physicObject.Enable();
        gameObject.SetActive(true);
    }
    
    public void BeCollected()
    {
        IsCollected = true;
    }
}