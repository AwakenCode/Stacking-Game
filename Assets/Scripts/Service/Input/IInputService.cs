using System;
using UnityEngine;

namespace Service.Input
{
    public interface IInputService : IService
    {
        public Vector2 Movement { get; }
        public void SubscribeForJump(Action jumpAction);
    }
}