using UnityEngine;

namespace RPG
{
    public static class AnimatorStringToHash
    {
        public static readonly int PlayerAttitude = Animator.StringToHash("PlayerAttitude");
        public static readonly int HorizontalVelocity = Animator.StringToHash("HorizontalVelocity");
        public static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    }
}