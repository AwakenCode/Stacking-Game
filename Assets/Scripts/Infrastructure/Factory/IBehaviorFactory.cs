using Character;
using Service;

namespace Infrastructure.Factory
{
    public interface IBehaviorFactory : IService
    {
        void Init(Player target);
        IJump CreateSimpleJump();
        IJump CreateMultipleJump();
        IGravity CreateSimpleGravity();
        IGravity CreateZeroGravity();
        IRotator CreateSimpleRotator();
        IMoveable CreateSimpleMovement();
    }
}