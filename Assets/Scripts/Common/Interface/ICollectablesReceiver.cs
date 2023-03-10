using GameplayEntities.Interface;
using System;
using System.Collections.Generic;

public interface ICollectablesReceiver
{
    event Action CollectorTriggered;
    void ReceiveCollectables(Stack<ICollectableTransform> collectables);
}
