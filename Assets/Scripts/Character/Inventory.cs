using Common.Interface;
using GameplayEntities.Interface;
using System.Collections.Generic;

namespace Character
{
    public class Inventory
    {
        private ICollectorTransform _collector;
        private ICollectablesReceiver _collectablesReceiver;

        private Stack<ICollectableTransform> _collectables = new Stack<ICollectableTransform>();

        public Inventory(ICollectorTransform collector)
        {
            _collector = collector;
        }

        public void Init(ICollectablesReceiver collectablesReceiver)
        {
            _collectablesReceiver = collectablesReceiver;
            Enable();
        }

        public void Enable()
        {
            _collectablesReceiver.CollectorTriggered += OnCollectorTriggered;
            _collector.CollectableCollected += OnCollectableCollected;
        }

        public void Disable()
        {
            _collectablesReceiver.CollectorTriggered -= OnCollectorTriggered;
            _collector.CollectableCollected -= OnCollectableCollected;
        }

        private void OnCollectableCollected(ICollectableTransform collectable)
        {
            _collectables.Push(collectable);
        }

        private void OnCollectorTriggered()
        {
            _collectablesReceiver.ReceiveCollectables(_collectables);
        }
    }
}