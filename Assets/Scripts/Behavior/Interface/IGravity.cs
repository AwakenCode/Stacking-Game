namespace Behavior.Interface
{
    public interface IGravity
    {
        float Value { get; set; }
        void ApplyGravity();
    }
}