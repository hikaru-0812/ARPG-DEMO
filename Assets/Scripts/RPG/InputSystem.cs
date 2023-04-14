using UnityEngine.InputSystem;

namespace RPG
{
    public class InputSystem
    {
        private static InputSystem _instance;
        public static InputSystem Instance
        {
            get { return _instance ??= new InputSystem(); }
        }

        public InputAction MoveAction;
        public InputAction AttackAction;

        public bool isGround;
        public float TargetSpeed;
        public float VerticalVelocity;

        public void EnableInput()
        {
            MoveAction.Enable();
        }
        
        public void DisableInput()
        {
            MoveAction.Disable();
            TargetSpeed = 0;
        }
    }
}