using GameplayEntities.Interface;
using System;
using System.Collections.Generic;

namespace Common.Interface
{
    public interface ICollectablesReceiver
    {
        event Action CollectorTriggered;
        void ReceiveCollectables(Stack<ICollectableTransform> collectables);
    }
}