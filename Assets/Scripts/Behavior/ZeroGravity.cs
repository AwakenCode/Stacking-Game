using Behavior.Interface;

namespace Behavior
{
    public class ZeroGravity : IGravity
    {
        public float Value { get; set; } = 0;

        public void ApplyGravity() { }
    }
}