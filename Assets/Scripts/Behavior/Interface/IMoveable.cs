using UnityEngine;

namespace Behavior.Interface
{
    public interface IMoveable
    {
        Vector3 Direction { get; set; }
        void Move();
    }
}