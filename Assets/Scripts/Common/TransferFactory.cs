using Behavior;
using UnityEngine;

namespace Common
{
    public static class TransferFactory
    {
        public static JumpingToTarget CreateJumpTransfer(Transform targetParent, float jumpPower, float receiveDuration)
        {
            return new JumpingToTarget(targetParent, jumpPower, receiveDuration);
        }
    }
}