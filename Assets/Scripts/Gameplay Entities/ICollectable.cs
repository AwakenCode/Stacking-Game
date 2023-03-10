namespace GameplayEntities.Interface
{
    public interface ICollectable
    {
        bool IsCollected { get; }

        void BeCollected();
    }

    public interface ICollectableTransform : ICollectable, ITransformable
    {

    }
}