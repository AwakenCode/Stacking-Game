using UnityEngine;

public interface IMoveable
{
    Vector3 Direction { get; set; }
    void Move();
}
