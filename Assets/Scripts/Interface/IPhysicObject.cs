using UnityEngine;

public interface IPhysicObject : IHasCollider, IRigidbody
{
    void Enable()
    {
        Collider.enabled = true;
        Rigidbody.isKinematic = false;
    }

    void Disable()
    {
        Collider.enabled = false;
        Rigidbody.isKinematic = true;
    }
}

public interface IRigidbody
{
    Rigidbody Rigidbody { get; }
}

public interface IHasCollider
{
    Collider Collider { get; }
}